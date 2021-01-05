using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpUtility.Helpers
{
    class DirectoryHelper
    {
        /// <summary>To get all files with a given extension under given root folder</summary>
        public List<FileInfo> GetFiles(string path, string pattern = "*.*")
        {
            return new DirectoryInfo(path).GetFiles(pattern, SearchOption.AllDirectories).ToList();
        }

        /// <summary>To get a collection of files from given root array with a given extension</summary>
        public List<FileInfo> GetFiles(string[] path, string pattern)
        {
            List<FileInfo> temp = new List<FileInfo>();
            for (int i = 0; i < path.Length; i++)
            {
                temp.AddRange(GetFiles(path[i], pattern));
            }
            return temp;
        }
    }
}
