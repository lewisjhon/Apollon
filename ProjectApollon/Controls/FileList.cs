using ProjectApollon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollon.Controls
{
    public static class FileList
    {
        public static List<FileClass> FileLists { get; set; }

        public static string SavePath { get; set; }

        public static bool CutParenthesisTitle { get; set; }

        public static bool CutParenthesisArtist { get; set; }


        private static void AddMusicFile(int No, string path)
        {
            FileMusic fMusic = new FileMusic(path);
            fMusic.No = No;
            if (CutParenthesisTitle) { fMusic.CutParenthesis(CutParenthesisItem.Title); }
            if (CutParenthesisArtist) { fMusic.CutParenthesis(CutParenthesisItem.Artist); }

            FileLists.Add(fMusic);
        }


        public static void CreateListByExtenstion(string path)
        {
            int i = 0;
            
            if (Directory.Exists(path))
            {
                if (FileLists == null)
                    FileLists = new List<FileClass>();
                else
                    FileLists.Clear();                

                foreach (string fileName in Directory.GetFiles(path, "*.MP3"))
                {                    
                    AddMusicFile(++i, fileName);
                }
            }
        }

        public static bool ConvertFileNameAt(int index)
        {
            string newFileName = string.Empty;
            FileMusic muFile = (FileMusic)FileLists.ElementAt(index);

            if (string.IsNullOrEmpty(muFile.Artist) && string.IsNullOrEmpty(muFile.Title))
                newFileName = muFile.FileName;                
            else
                newFileName = string.Concat("[", muFile.Artist, "]", muFile.Title, ".mp3");

            if (SavePath.Equals(string.Empty))
                return false;

            if (!Directory.Exists(@SavePath))
                Directory.CreateDirectory(@SavePath);

            try
            {
                var sourceFile = string.Concat(FileLists.ElementAt(index).FilePath, "\\", FileLists.ElementAt(index).FileName);

                File.Copy(sourceFile, string.Concat(@SavePath, "//", newFileName), true);
                File.Delete(sourceFile);
            }
            catch(Exception ex)
            {

            }

            return true;
        }

    }
}
