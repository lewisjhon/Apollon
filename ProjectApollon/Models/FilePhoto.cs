using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApollon.Models
{
    public enum FileType
    {
        JPG,
        JPEG,
        BMP,
        PNG,
        GIF
    }


    public class FilePhoto : FileClass
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public FileType type { get; set; }
    }
}
