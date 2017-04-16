using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CubeMove
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public Action<string> changedaction;
        private void turnBig(object sender, EventArgs e)
        {
            var obj = new { methodName = "turnBig" };
            string txt = JsonConvert.SerializeObject(obj);
            if(changedaction != null)
            {
                changedaction.Invoke(txt);
            }
            else
            {
                MessageBox.Show("changedaction == null");
            }
        }

        private void turnSmall(object sender, EventArgs e)
        {
            var obj = new { methodName = "turnSmall" };
            string txt = JsonConvert.SerializeObject(obj);
            if (changedaction != null)
            {
                changedaction.Invoke(txt);
            }
            else
            {
                MessageBox.Show("changedaction == null");
            }

        }

        private void toLeft(object sender, EventArgs e)
        {
            var obj = new { methodName = "toLeft" };
            string txt = JsonConvert.SerializeObject(obj);

            if (changedaction != null)
            {
                changedaction.Invoke(txt);
            }
            else
            {
                MessageBox.Show("changedaction == null");
            }

        }

        private void toRight(object sender, EventArgs e)
        {
            var obj = new { methodName = "toRight" };
            string txt = JsonConvert.SerializeObject(obj);

            if (changedaction != null)
            {
                changedaction.Invoke(txt);
            }
            else
            {
                MessageBox.Show("changedaction == null");
            }
        }

        private void toDown(object sender, EventArgs e)
        {
            var obj = new { methodName = "toDown" };
            string txt = JsonConvert.SerializeObject(obj);

            if (changedaction != null)
            {
                changedaction.Invoke(txt);
            }
            else
            {
                MessageBox.Show("changedaction == null");
            }
        }

        private void toUp(object sender, EventArgs e)
        {
            var obj = new { methodName = "toUp" };
            string txt = JsonConvert.SerializeObject(obj);

            if (changedaction != null)
            {
                changedaction.Invoke(txt);
            }
            else
            {
                MessageBox.Show("changedaction == null");
            }
        }

        public void SetLable(string txt)
        {
            label1.Text = txt;
        }

        internal void RegisterHolder(Action<string> onStateChange)
        {
            this.changedaction = onStateChange;
        }
    }
}
