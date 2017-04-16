using System;
using System.Reflection;
using FormSwitch;
using MessageTrans;
using MessageTrans.Interal;
using Protocal;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FormLoader
{
    public class ExecuteHelp
    {
        DataSender dataSender;
        bool writeData;
        Dictionary<string, object> holdDic = new Dictionary<string, object>();
        public ExecuteHelp(DataSender dataSender, bool writeData)
        {
            this.dataSender = dataSender;
            this.writeData = writeData;
        }
        public void InvokeSimpleMethodByProtocal(string x)
        {
            try
            {
                DllFuction protocal = JsonConvert.DeserializeObject<DllFuction>(x);
                Assembly asb = Assembly.LoadFrom(protocal.dllpath);
                var cls = asb.GetType(protocal.classname);
                var method = cls.GetMethod(protocal.methodname);
                var instence = Activator.CreateInstance(cls);
                string back = (string)method.Invoke(instence, protocal.argument);
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
        public void LunchCommnuicateHolder(string x)
        {
            try
            {
                HolderLunchFunc protocal = JsonConvert.DeserializeObject<HolderLunchFunc>(x);
                Assembly asb = Assembly.LoadFrom(protocal.dllData.dllpath);
                var cls = asb.GetType(protocal.dllData.classname);
                var method = cls.GetMethod(protocal.dllData.methodname);
                var instence = Activator.CreateInstance(cls);
                holdDic[protocal.holderName] = instence;//注册单例
                method.Invoke(instence, protocal.dllData.argument);
                dataSender.SendMessage(ProtocalType.lunchholder.ToString());
                //由于在unityedior模式下不支持信息传回所以用读写的方式
                if (writeData)
                {
                    string path = System.IO.Directory.GetCurrentDirectory() + "/" + ProtocalType.lunchholder.ToString() + ".txt";
                    System.IO.File.WriteAllText(path, "");
                }
                var registerMethod = cls.GetMethod(protocal.holderRegistFunc);
                registerMethod.Invoke(instence, new object[] { new Action<string>(OnHolderStateChanged) });
            }
            catch (Exception e)
            {
                MessageBox.Show("LunchCommnuicateHolder:err\n" + e.Message);
            }

        }
        public void RegisterCommuateModle(string x)
        {
            try
            {
                ChartData protocal = JsonConvert.DeserializeObject<ChartData>(x);
                object instence = null;
                if (holdDic.TryGetValue(protocal.holderName,out instence))
                {
                    if (instence != null)
                    {
                        var method = instence.GetType().GetMethod(protocal.methodName);
                        method.Invoke(instence, protocal.arguments);
                    }
                    else
                    {
                        MessageBox.Show("模块不存在：" + protocal.holderName);
                    }
                }
               
            }
            catch (Exception e)
            {
                MessageBox.Show("RegisterCommuateModle:err\n" + e.Message);
            }
        }

        /// <summary>
        /// dll中的事件回调
        /// </summary>
        /// <param name="state"></param>
        private void OnHolderStateChanged(string state)
        {
            dataSender.SendMessage(ProtocalType.communicate.ToString(), state);
            //由于在unityedior模式下不支持信息传回所以用读写的方式
            if (writeData)
            {
                string path = System.IO.Directory.GetCurrentDirectory() + "/" + ProtocalType.communicate.ToString() + ".txt";
                System.IO.File.WriteAllText(path, state);
            }

        }
    }
}
