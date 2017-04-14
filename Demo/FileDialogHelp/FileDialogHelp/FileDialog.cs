using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
namespace FileDialogHelp
{
    public class FileDialog
    { 
        public string Main(string type)
        {
            string resultFile = "";
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "D:\\";
                openFileDialog1.Filter = "All files (*.*)|*.*|txt files (*.txt)|*.txt";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    resultFile = openFileDialog1.FileName;
                   
                }
            }
            return resultFile;
        }
    }
}
