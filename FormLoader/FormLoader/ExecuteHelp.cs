using System;
using System.Reflection;
using FormSwitch;
using MessageTrans;
using MessageTrans.Interal;
using Protocal;
using Newtonsoft.Json;
using System.Windows.Forms;


namespace FormLoader
{
    public class ExecuteHelp
    {
        DataSender dataSender;
        bool writeData;
        public ExecuteHelp(DataSender dataSender, bool writeData)
        {
            this.dataSender = dataSender;
            this.writeData = writeData;
        }
        public void InvokeMethodByProtocal(string x)
        {
            try
            {
                DllFuction protocal = JsonConvert.DeserializeObject<DllFuction>(x);
                Assembly asb = Assembly.LoadFrom(protocal.dllpath);
                var cls = asb.GetType(protocal.classname);
                var method = cls.GetMethod(protocal.methodname);
                var instence = Activator.CreateInstance(cls);
                string back = (string)method.Invoke(instence, new object[] { protocal.argument });
                dataSender.SendMessage(ProtocalType.dllfunction.ToString(), back);
                //由于在unityedior模式下不支持信息传回所以用读写的方式
                if (writeData)
                {
                    string path = System.IO.Directory.GetCurrentDirectory() + "/" + ProtocalType.dllfunction.ToString() + ".txt";
                    System.IO.File.WriteAllText(path, back);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
