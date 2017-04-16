using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeMove
{
    public class Program
    {
        MainForm form;
        public void Main()
        {
            form = new MainForm();
            form.Show();
        }
        public void RegisterHolder(Action<string> onStateChange)
        {
            form.RegisterHolder(onStateChange);
        }
        public void SimpleTxt(string data)
        {
            form.SetLable(data);
        }
    }
}
