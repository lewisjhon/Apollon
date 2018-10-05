using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollon.Models
{
    public enum CutParenthesisItem
    {
        Title,
        Artist
    }

    public class FileMusic : FileClass
    {
        public FileMusic(string path)
        {
            TagLib.File file = TagLib.File.Create(path);

            this.FileName = path.Substring(path.LastIndexOf('\\') + 1);
            this.FilePath = path.Substring(0, path.LastIndexOf('\\'));

            this.Title = ConvertEncoding(file.Tag.Title);
            this.Artist = ConvertEncoding(file.Tag.FirstAlbumArtist);
            this.Album = ConvertEncoding(file.Tag.Album);
            this.Genre = ConvertEncoding(file.Tag.FirstGenre);
        }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }


        public void CutParenthesis(CutParenthesisItem item)
        {
            switch (item)
            {
                case CutParenthesisItem.Artist:
                    if (Artist.IndexOf("(") > 0)
                        Artist = Artist.Substring(0, Artist.IndexOf("(", 2));
                    break;
                case CutParenthesisItem.Title:
                    if (Title.IndexOf("(") > 0)
                        Title = Title.Substring(0, Title.IndexOf("(", 2));
                    break;
            }
        }

        private static string ConvertEncoding(string tagText)
        {
            if (string.IsNullOrEmpty(tagText))
                return string.Empty;

            Encoding newEncoding = Encoding.GetEncoding("ISO-8859-1");
            byte[] utfBytes = Encoding.UTF8.GetBytes(tagText);
            byte[] asciBytes = Encoding.Convert(Encoding.UTF8, newEncoding, utfBytes);
            string testText = newEncoding.GetString(asciBytes);

            //UTF-8 인코딩 후 변화 없으면 아래 방식으로 다시 인코딩.
            if (tagText.Equals(testText))
            {
                string ReturnText = string.Empty;

                if (string.IsNullOrEmpty(tagText)) return ReturnText;

                byte[] ByteArr = new byte[tagText.Length];

                for (int i = 0; i < tagText.Length; i++)
                {
                    ByteArr[i] = (byte)tagText[i];
                }

                ReturnText = Encoding.Default.GetString(ByteArr);
                return ReturnText;
            }
            else
            {
                return tagText;
            }

            //mp3 Tag 한글 저장시에는 아래와 같이 함.
            //file.Tag.Title = Encoding.UTF7.GetString(Encoding.Default.GetBytes("달려라하니"));
            //file.Save();
        }

    }
}
