using FileWatcherEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Forms=System.Windows.Forms;
using System.Windows.Input;
using TestFileWatcher.Core;
using System.Windows;
using TestFileWatcher.Models;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Data;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Interop;
using System.Windows.Threading;

namespace TestFileWatcher.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private string _folderPath;
        private readonly FileWatcherMan _fileWatcherMan;
        private DataModel _selectedItem;
        
        public DataModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string FolderPath
        {
            get => _folderPath;
            set
            {
                if (_folderPath != value)
                {
                    _folderPath = value;
                    RaisePropertyChanged();
                }
            }
        }
        public ObservableCollection<DataModel> FileList { get; }

        #region commands

        public ICommand SelectFolderCommand { get; }
        public ICommand RunAsAdminCommand { get; }
        public ICommand ListSelectedCommand { get; }

        private bool CanExecuteRunAsAdminCommand(object arg)
        {
            return IsRunningAsAdmin() == false;
        }

        private void ExecuteRunAsAdminCommand(object obj)
        {
            var appPath = Assembly.GetExecutingAssembly().Location;
            _fileWatcherMan.RunWithAdmin(appPath);
            Application.Current.Shutdown();
        }

        private void ExecuteListSelectedCommand(object obj)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.IsDir)
                {
                    FolderPath = SelectedItem.Path;
                    PopulateFileList(SelectedItem.Path);
                }
                else
                    OpenSelectedFile(SelectedItem);
            }
        }

        private void ExecuteSelectFolderCommand(object obj)
        {
            var dlg = new Forms.FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            if (dlg.ShowDialog() == Forms.DialogResult.OK)
            {
                FolderPath = dlg.SelectedPath;
                PopulateFileList(FolderPath);
            }
        }

        #endregion

        #region private methods

        private List<DataModel> GetDirectories(string path)
        {
            var list = new List<DataModel>();
            var dirs=Directory.GetDirectories(path);

            var parent=Directory.GetParent(path);
            if(parent!=null)
            {
                var info = new DirectoryInfo(path);
                list.Add(new DataModel
                {
                    Name = "..",
                    Path = parent.FullName,
                    IsDir = true,
                    Size = 0,
                    Date = info.CreationTime
                });
            }

            foreach (var dirName in dirs)
            {
                var info = new DirectoryInfo(dirName);

                list.Add(new DataModel
                {
                    Name = info.Name,
                    Path = info.FullName,
                    IsDir = true,
                    Size = 0,
                    Date = info.CreationTime
                });
            }

            return list;
        }

        private IEnumerable<DataModel> GetFiles(string path)
        {
            var names = Directory.GetFiles(path);
            
            foreach (var name in names)
            {
                var info=new FileInfo(name);
                yield return new DataModel
                {
                    Name = info.Name,
                    Path = info.FullName,
                    IsDir = false,
                    Size = info.Length,
                    Date = info.CreationTime
                };
            }
        }

       private void OpenSelectedFile(DataModel selectedItem)
        {
            Process.Start(selectedItem.Path);
        }

        private async void PopulateFileList(string folderPath)
        {
            FileList.Clear();

            var items =await Task.Run(() =>
            {
                var list=new List<Models.DataModel>();

                list.AddRange(GetDirectories(folderPath));
                list.AddRange(GetFiles(folderPath));

                return list;
            });

            foreach (var item in items)
                FileList.Add(item);
        }

        private bool IsRunningAsAdmin()
        {
            return _fileWatcherMan.HasAdminPrivileges();
        }

        #endregion

        public MainViewModel()
        {
            SelectFolderCommand = new RelayCommand(ExecuteSelectFolderCommand);
            RunAsAdminCommand = new RelayCommand(ExecuteRunAsAdminCommand, CanExecuteRunAsAdminCommand);
            ListSelectedCommand = new RelayCommand(ExecuteListSelectedCommand);
            FileList = new ObservableCollection<DataModel>();

            _fileWatcherMan = new FileWatcherMan();
        }
    }
}
