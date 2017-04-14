using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Forms;
namespace FileDialogHelp
{
    public class FileDialog
    { 
        public string Main(string type)
        {
            string resultFile = "";
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\" ;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*" ;
            openFileDialog1.FilterIndex = 2 ;
            openFileDialog1.RestoreDirectory = true ;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            resultFile = openFileDialog1.FileName;
                        }
                    } }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            return resultFile;

        }
    }
}
