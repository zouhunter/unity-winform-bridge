using System;
using System.Collections.Generic;
using System.Text;

namespace Protocal
{

    [System.Serializable]
    public class DllFuction
    {
        public string dllpath;
        public string classname;
        public string methodname;
        public object[] argument;
        public DllFuction(string dllpath,string classname,string methodname, object[] argument)
        {
            this.dllpath = dllpath;
            this.classname = classname;
            this.methodname = methodname;
            this.argument = argument;
        }
    }
}
