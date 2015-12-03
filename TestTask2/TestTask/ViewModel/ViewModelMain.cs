﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using TestTask.Model;
using System.Threading;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace TestTask.ViewModel
{
    public class ViewModelMain : ViewModelBase
    {
        ObservableCollection<file> _Files;
        ICommand _command;

        private Task filefinder;
        private Task hashcalc;
        private Task icongeter;
        private Task checkchanger;

        public ViewModelMain()
        {
            Files = new ObservableCollection<file>();

            filefinder = new Task(TraverseTree);
            hashcalc = new Task(SetMD5HashFromFile);
            icongeter = new Task(SetIcon);
            checkchanger = new Task(checkchange);

            filefinder.Start();
            hashcalc.Start();
            icongeter.Start();
            checkchanger.Start();

        }

        

        public ObservableCollection<file> Files
        {
            get
            {
                return _Files;
            }
            set
            {
                _Files = value;
                OnPropertyChanged("Files");
            }
        }

        public void TraverseTree()
        {
            const string root = @"C:\";
            Stack<string> dirs = new Stack<string>();
            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                try
                {
                    FileInfo[] dirinfo = new DirectoryInfo(currentDir).GetFiles();
                    int i = 0;
                    while (i < dirinfo.Length)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                        {
                            Files.Add(new file(dirinfo[i].Name, dirinfo[i].FullName, dirinfo[i].LastWriteTime.ToString(), dirinfo[i].CreationTime.ToString(), dirinfo[i].Extension, allCheck)); // Add row on UI thread 
                        }));
                        //Files.Add(new file(dirinfo[i].Name, string.Empty, null, dirinfo[i].FullName));
                        ++i;
                    }
                }

                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (Exception)
                {
                    continue;
                }
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
        }

        public void SetMD5HashFromFile()
        {
            int i = 0;
            while (true) 
            {
                if (Files.Count == 0)
                    continue;
                while (i < Files.Count)
                {
                    try
                    {
                        lock (Files)
                        {
                            var md5 = MD5.Create();
                            var stream = File.OpenRead(Files[i].Path);
                            Files[i].Signature = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                            ++i;
                        }
                    }
                    catch (System.IO.IOException e)
                    {
                        Files[i].Signature = "has not access";
                        ++i;
                        continue;
                    }
                    catch (System.UnauthorizedAccessException e)
                    {
                        Files[i].Signature = "Access deny";
                        ++i;
                        continue;
                    }
                    catch (Exception)
                    {
                        Files[i].Signature = "Access deny";
                        ++i;
                        continue;
                    }
                }
            }
        }

        public void SetIcon()
        {
            int i = 0;
            while (true)
            {
                if(Files.Count == 0)
                    continue;
                while (i < Files.Count)
                {
                    lock (Files)
                    {
                        var sysicon = System.Drawing.Icon.ExtractAssociatedIcon(Files[i].Path);
                        var bmpSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                            sysicon.Handle,
                            System.Windows.Int32Rect.Empty,
                            System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                        bmpSrc.Freeze();
                        Files[i].Icon = bmpSrc;
                        Files[i].Icon.Freeze();
                        ++i;

                    }
                }
            }
        }

        public void checkchange()
        {
            int i = 0;
            bool check = allCheck;
            while (true)
            {
                if (Files.Count == 0)
                    continue;
                while (i < Files.Count)
                {
                    lock (Files)
                    {
                        
                        if (!Equals(check,allCheck))
                        {
                            i = 0;
                            check = allCheck;
                        }
                        Files[i].ischeck = check;
                        ++i;
                    }
                }
            }
        }

        //public ICommand StopScan
        //{
        //    get
        //    {
        //        if (_command == null)
        //        {
        //            _command = new file.DelegateCommand(CanExecute, Execute);
        //        }
        //        return _command;
        //    }
        //}
        //bool isStop = false;
        //private void Execute(object parameter)
        //{
        //    if (!isStop)
        //    {
        //        ts.Cancel();
        //        isStop = true;
        //    }
        //    else
        //    {
        //        filefinder.Start();
        //        //hashcalc.Start();
        //        icongeter.Start();
        //        //getLastModified.Start();
        //        //getDateCreated.Start();
        //    }
        //}
        //private bool CanExecute(object parameter)
        //{
        //    return true;
        //}

        bool allCheck = false;
        string checkbuttonContent = "Check All";
        public ICommand chekall
        {
            get
            {
                if (_command == null)
                {
                    _command = new file.DelegateCommand(CanExecute, Execute);
                }
                return _command;
            }
        }
        private void Execute(object parameter)
        {
            if (!allCheck)
            {
                checkbuttonContent = "Uncheck All";
                allCheck = true;
            }
            else
            {
                checkbuttonContent = "Check All";
                allCheck = false;
            }
        }
        private bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
