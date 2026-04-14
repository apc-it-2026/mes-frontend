using System;

using System.Collections.Generic;
using System.Text;

using System.IO;

namespace SJeMES_Framework.Common
{
    public class IOHelper
    {
        //文件夹
        public class Folder
        {
            int _Level;//层次，0为首层
            string _Name;
            string _Path;

            public Folder(string Name, int Level,string Path)
            {
                _Name = Name;
                _Level = Level;
                _Path = Path;
            }

            public string Name
            { get { return _Name; } set { _Name = value; } }

            public int Level
            { get { return _Level; } set { _Level = value; } }

            public string Path
            { get { return _Path; } set { _Path = value; } }
        }

        public class FileSystem
        {
            int _LevelNum;
            List<IOHelper.Folder> _Folders;
            List<IOHelper.File> _Files;

            public FileSystem()
            {
                _Folders = new List<Folder>();
                _Files = new List<File>();
                _LevelNum = 0;
            }

            public void AddFolder(Folder f)
            {
                _Folders.Add(f);
                if (f.Level > _LevelNum)
                    _LevelNum = f.Level;
            }

            public void AddFile(File f)
            {
                _Files.Add(f);
                if (f.Level > _LevelNum)
                    _LevelNum = f.Level;
            }

            public List<IOHelper.Folder> Folders
            { get { return _Folders; } set { _Folders = value; } }

            public List<IOHelper.File> Files
            { get { return _Files; } set { _Files = value; } }

            public int LevelNum
            { get { return _LevelNum; } set { _LevelNum = value; } }
        }

        //文件
        public class File
        {
            int _Level;//层次，0为首层
            Folder _ByFolder;//所在文件夹
            string _Name;
            string _Path;

            public File(string Name, Folder ByFolder,string Path,int Level)
            {
                _Name = Name;
                _ByFolder = ByFolder;
                _Path = Path;
                _Level = Level;
            }

            public string Name
            { get { return _Name; } set { _Name = value; } }

            public Folder ByFolder
            { get { return _ByFolder; } set { _ByFolder = value; } }

            public string Path
            { get { return _Path; } set { _Path = value; } }

            public int Level
            { get { return _Level; } set { _Level = value; } }
        }


        public static bool CopyFile(FileSystem fs, string TargetPath)
        {

            bool ret = false;
            try
            {
                if (Directory.Exists(TargetPath))
                {
                    Directory.Delete(TargetPath, true);
                }

                Directory.CreateDirectory(TargetPath);

                for (int i = 0; i <= fs.LevelNum; i++)
                {
                    for (int k = 0; k < fs.Folders.Count; k++)
                    {
                        Folder folder = fs.Folders[k];
                        if (folder.Level == i)
                        {
                            string folderPath = string.Empty;
                            string temp = string.Empty;
                            temp = folder.Path;
                            for (int j = 1; j < folder.Level; j++)
                            {
                                if (temp.EndsWith(@"\"))
                                    temp = temp.Remove(temp.LastIndexOf(@"\"), 1);
                                temp = temp.Remove(temp.LastIndexOf(@"\"), temp.Length - temp.LastIndexOf(@"\"));
                            }

                            folderPath = folder.Path.Substring(temp.Length);
                            temp = TargetPath + @"\" + folderPath + @"\" + folder.Name;
                            Directory.CreateDirectory(temp);
                        }
                    }

                    for (int k = 0; k < fs.Files.Count; k++)
                    {
                        File file = fs.Files[k];
                        if (file.Level == i)
                        {
                            string FilePath = string.Empty;
                            string temp = string.Empty;
                            temp = file.Path;
                            for (int j = 0; j < file.Level; j++)
                            {
                                if (temp.EndsWith(@"\"))
                                    temp = temp.Remove(temp.LastIndexOf(@"\"), 1);
                                temp = temp.Remove(temp.LastIndexOf(@"\"), temp.Length - temp.LastIndexOf(@"\"));
                            }

                            FilePath = file.Path.Substring(temp.Length);
                            temp = TargetPath + @"\" + FilePath + @"\" + file.Name;
                            System.IO.File.Copy(file.Path + @"\" + file.Name, temp);
                        }
                    }
                }

                ret = true;
            }
            catch (Exception ex) { ret = false; }
            return ret;
        }

        public static List<File> GetAllFile(string Path, List<File> Files, int Level)
        {
            try
            {
                string[] dir = Directory.GetDirectories(Path);
                string[] files = Directory.GetFiles(Path);

                for (int i = 0; i < dir.Length; i++)
                {
                    string NewPath = dir[i] + @"\";
                    Files = GetAllFile(NewPath, Files, Level+1);
                }

                for (int i = 0; i < files.Length; i++)
                {
                    string name = files[i].Substring(files[i].LastIndexOf(@"\")+1);
                    if(Path.EndsWith(@"\"))
                    {
                        Path= Path.Remove(Path.Length-1,1);
                    }
                    string fname=Path.Substring(Path.LastIndexOf(@"\") + 1);
                    Folder f = new Folder(fname, Level,
                    Path.Remove(Path.LastIndexOf(@"\"), Path.Length - Path.LastIndexOf(@"\")));

                    Files.Add(new File(name, f, Path, Level));
                }
            }
            catch (Exception ex) { };

            return Files;
        }

        public static List<Folder> GetAllDirectories(string Path,List<Folder> Folders,int Level)
        {
            try
            {
                Level++;
                string[] dir = Directory.GetDirectories(Path);

                for (int i = 0; i < dir.Length; i++)
                {
                    string NewPath=dir[i]+@"\";
                    Folders=GetAllDirectories(NewPath, Folders, Level);

                    Folders.Add(new Folder(dir[i].Substring(dir[i].LastIndexOf(@"\")+1), Level, Path));
                }
            }
            catch(Exception ex){};

            return Folders;
        }

        public static FileSystem GetFileSystem(string Path)
        {
            FileSystem ret = new FileSystem();
            try
            {
                List<Common.IOHelper.Folder> folder = new List<Common.IOHelper.Folder>();
                folder = Common.IOHelper.GetAllDirectories(Path, folder, 0);

                List<Common.IOHelper.File> files = new List<Common.IOHelper.File>();
                files = Common.IOHelper.GetAllFile(Path, files, 0);

                for (int i = folder.Count - 1; i > -1; i--)
                {
                    ret.AddFolder(folder[i]);
                }

                for (int i = files.Count - 1; i > -1; i--)
                {
                    ret.AddFile(files[i]);
                }

            }
            catch (Exception ex) { }
            return ret;
        }

      


        
    }
}
