using System;
using System.IO;

namespace slsr
{
    public class Logger
    {
        public void Write(string text)
        {
            using StreamWriter writer = new StreamWriter("logs/slsr.txt");
            var date = DateTime.Now.ToShortDateString();
            var time = DateTime.Now.ToShortTimeString();
            writer.WriteLine(text);
            writer.Close();
        }
    }
}
