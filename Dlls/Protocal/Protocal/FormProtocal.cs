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

    [System.Serializable]
    public class HolderLunchFunc
    {
        public string holderName;
        public string holderRegistFunc;
        public DllFuction dllData;
        public HolderLunchFunc(string holderName, string holderRegistFunc, string dllpath, string classname, string methodname, object[] argument)
        {
            this.holderName = holderName;
            this.holderRegistFunc = holderRegistFunc;
            this.dllData = new Protocal.DllFuction(dllpath, classname, methodname, argument);
        }
    }
    [System.Serializable]
    public class ChartData
    {
        public string holderName;
        public string methodName;
        public object[] arguments;
    }
}
