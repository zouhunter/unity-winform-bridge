using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using SimpleJSON;
namespace FileDialogHelp
{
    public class FileDialog
    {
        private string ConverByte(object titlebytes)
        {
            JSONArray array = JSONArray.Parse(titlebytes.ToString()).AsArray;
            byte[] byts = new byte[array.Count];
            for (int i = 0; i < byts.Length; i++)
            {
                byts[i] = (byte)array[i].AsInt;
            }
            string title = System.Text.Encoding.UTF8.GetString(byts);
            return title;
        }
        public string OpenFileDialog(object titlebytes, string filter, string initialDirectory)
        {
            string title = ConverByte(titlebytes);

            string resultFile = "";
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = initialDirectory;
                openFileDialog1.Filter = filter;// "txt files (*.txt)|*.txt|All files (*.*)|*.*" ;
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.Title = title;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    // Insert code to read the stream here.
                    resultFile = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }

            return resultFile;

        }
        public string SaveFileDialog(object titlebytes, string filter, string initialDirectory)
        {
            string title = ConverByte(titlebytes);

            string resultFile = "";
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = title;
                saveFileDialog1.InitialDirectory = initialDirectory;
                saveFileDialog1.Filter = filter;
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    resultFile = saveFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
            return resultFile;

        }
        public string ColorDialog(string color)
        {
            string resultFile = "";
            try
            {
                ColorDialog MyDialog = new ColorDialog();
                // Keeps the user from selecting a custom color.
                MyDialog.AllowFullOpen = false;
                // Allows the user to get help. (The default is false.)
                MyDialog.ShowHelp = true;
                // Sets the initial color select to the current text color.
                MyDialog.Color = ColorTranslator.FromHtml(color);

                // Update the text box color if the user clicks OK 
                if (MyDialog.ShowDialog() == DialogResult.OK)
                {
                    resultFile = ColorTranslator.ToHtml(MyDialog.Color);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not ConverColor error: " + ex.Message);
                throw;
            }
            
            return resultFile;
        }
    }

}
