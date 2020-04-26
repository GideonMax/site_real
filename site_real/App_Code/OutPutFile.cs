using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace site_real
{
    public class OutPutFile: IDisposable
    {
        StreamWriter writer;
        public OutPutFile(string path="D:/output.txt")
        {
            writer = File.AppendText(path);
            writer.AutoFlush = true;
        }
        public void Write(string text)
        {
            writer.Write(text);
        }
        public void WriteLine()
        {
            writer.WriteLine();
        }
        public void WriteLine(string text)
        {
            writer.WriteLine(text);
        }
        public void Dispose()
        {
            writer.Close();
            writer.Dispose();
        }
    }
}