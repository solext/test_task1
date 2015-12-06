using System;
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
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace TestTask.ViewModel
{
    public class ViewModelMain : ViewModelBase
    {
        ObservableCollection<file> _Files;
        ICommand _command;
        public Queue<file> FilesQueue;

        //Thread filefinder;
        //Thread hashcalc;
        //Thread icongeter;
        //Thread lastModified;

        private Task filefinder;
        private Task hashcalc;
        private Task icongeter;
        //Task getLastModified;
        //Task getDateCreated;
        private Task signaturegeter;
        private Task checkchanger;
        //private CancellationToken ct;
        //private CancellationTokenSource ts;

        public ViewModelMain()
        {
            //ts = new CancellationTokenSource();
            //ct = ts.Token;
            Files = new ObservableCollection<file>();
            FilesQueue = new Queue<file>();

            filefinder = new Task(TraverseTree);
            hashcalc = new Task(SetMD5HashFromFile);
            icongeter = new Task(SetIcon);
            //getLastModified = new Task(SetModifiedDate);
            //getDateCreated = new Task(SetCreatedDate);
            signaturegeter = new Task(SetDigitalSignature);
            checkchanger = new Task(checkchange);

            //filefinder.Name = "FileFinder";
            //filefinder.IsBackground = true;
            //filefinder.Priority = ThreadPriority.Highest;
            ////hashcalc.Name = "hashcalc";
            ////hashcalc.IsBackground = true;
            //icongeter.Name = "icongeter";
            //icongeter.IsBackground = true;
            //lastModified.Name = "lastModified";
            //lastModified.IsBackground = true;

            filefinder.Start();
            hashcalc.Start();
            //getDateCreated.Start();
            //getLastModified.Start();
            icongeter.Start();
            checkchanger.Start();
            signaturegeter.Start();
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
                            FilesQueue.Enqueue(new file(dirinfo[i].Name, dirinfo[i].FullName, dirinfo[i].LastWriteTime.ToString(), dirinfo[i].CreationTime.ToString(), dirinfo[i].Extension, allCheck));
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

        //public void SetModifiedDate()
        //{
        //    //Thread.Sleep(2000);
        //    int i = 0;
        //    while (i < Files.Count)
        //    {
        //        lock (Files)
        //        {
        //            var dtmod = System.IO.File.GetLastWriteTime(Files[i].Path).ToString("dd/MM/yy HH:mm:ss");
        //            Files[i].DateModified = dtmod;
        //            ++i;

        //        }
        //    }
        //}

        //public void SetCreatedDate()
        //{
        //    //Thread.Sleep(2000);
        //    int i = 0;
        //    while (i < Files.Count)
        //    {
        //        lock (Files)
        //        {
        //            var dtcr = System.IO.File.GetCreationTime(Files[i].Path).ToString("dd/MM/yy HH:mm:ss");
        //            Files[i].DateCreated = dtcr;
        //            ++i;
        //        }
        //    }
        //}

        //public void SetMD5HashFromFile()
        //{
        //    Thread.Sleep(2000);
        //    int i = 0;
        //    while (FilesQueue.Count > 0)
        //    {

        //        try
        //        {
        //            lock (FilesQueue)
        //            {
        //                file temp = FilesQueue.Dequeue();
        //                var md5 = MD5.Create();
        //                var stream = File.OpenRead(temp.Path);
        //                Files[i].Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
        //                ++i;
        //            }
        //        }
        //        catch (System.IO.IOException e)
        //        {
        //            Files[i].Hash = "has not access";
        //            ++i;
        //            continue;
        //        }
        //        catch (System.UnauthorizedAccessException e)
        //        {
        //            Files[i].Hash = "Access deny";
        //            ++i;
        //            continue;
        //        }
        //        catch (Exception)
        //        {
        //            Files[i].Hash = "Access deny";
        //            ++i;
        //            continue;
        //        }


        //    }
        //}
        public void SetIcon()
        {
            int i = 0;
            //Thread.Sleep(2000);
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
                        lock (FilesQueue)
                        {
                            file temp = FilesQueue.Dequeue();
                            var md5 = MD5.Create();
                            var stream = File.OpenRead(temp.Path);
                            Files[i].Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                            ++i;
                        }
                    }
                    catch (System.IO.IOException e)
                    {
                        Files[i].Hash = "has not access";
                        ++i;
                        continue;
                    }
                    catch (System.UnauthorizedAccessException e)
                    {
                        Files[i].Hash = "Access deny";
                        ++i;
                        continue;
                    }
                    catch (Exception)
                    {
                        Files[i].Hash = "Access deny";
                        ++i;
                        continue;
                    }
                }
            }
        }
        public void SetDigitalSignature()
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
                            Assembly assembly = Assembly.LoadFrom(Files[i].Path);
                            Module module = assembly.GetModules().First();
                            X509Certificate certificate = module.GetSignerCertificate();
                            if (certificate == null)
                            {
                                Files[i].Signature = "NO";
                                ++i;
                            }
                            else
                            {
                                Files[i].Signature = "YES";
                                ++i;
                            }
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
