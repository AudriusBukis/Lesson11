using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson11.Models
{
    public class FileWriterServiceStorage
    {
        //private readonly string _path = @"C:\Users\audri\Documents\Code_Academy_mokymai\lesson11\lesson11\lesson11\AllBookDataBase.txt";
        private readonly string _path = @"C:\Users\User\Desktop\net_mok\lesson11\lesson11\lesson11\AllBookDataBase.txt";
        public void AppendText(string text)
        {
            using (StreamWriter sw = File.AppendText(_path)) 
            {
                sw.WriteLine(text);
            }
        }

        public void WriteAllText(string[] lines)
        {
            File.WriteAllLines(_path, lines);
        }

        public List<string> GetAllLines()
        {
            var lines = File.ReadAllLines(_path);
            return lines.ToList();
        }
    }
    
}
