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
    public partial class HideOnStartupApplicationContext : ApplicationContext
    {
        private DataReceiver dataReceiver;
        private DataSender dataSender;
        private Form mainForm;
        private IntPtr parent { get { return swi.Parent; } }
        private WindowSwitchWinform swi;
        private bool writeData = false;//unityeditor暂不支持信息传回,这是处理方法是共享文件
        private ExecuteHelp exeHelp;
        public HideOnStartupApplicationContext(string[] datas)
        {
            mainForm = new Form();
            mainForm.WindowState = FormWindowState.Minimized;
            mainForm.Show();

            swi = new WindowSwitchWinform();
            if (swi.OnOpenedByParent(ref datas))
            {
                try
                {
                    if (datas.Length > 0) writeData = int.Parse(datas[0]) == 1;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                RegistMessageTrans(parent);
            }
            mainForm.Hide();
        }

        /// <summary>
        private void RegistMessageTrans(IntPtr parent)
        /// 注册事件
        /// </summary>
        /// <param name="parent"></param>
        {
            dataSender = new DataSender();
            dataSender.RegistHandle(parent);
            dataReceiver = new DataReceiver();
            dataReceiver.RegistHook();
            RegistEntry();
        }
        /// <summary>
        /// 注册执行事件
        /// </summary>
        private void RegistEntry()
        {
            exeHelp = new ExecuteHelp(dataSender,writeData);
            Win32API.SendMessage(swi.Current, Win32API.WM_ACTIVATE, Win32API.WA_ACTIVE, IntPtr.Zero);
            dataReceiver.RegisterEvent(ProtocalType.dllfunction.ToString(), exeHelp.InvokeSimpleMethodByProtocal);
            dataReceiver.RegisterEvent(ProtocalType.lunchholder.ToString(), exeHelp.LunchCommnuicateHolder);
            dataReceiver.RegisterEvent(ProtocalType.communicate.ToString(), exeHelp.RegisterCommuateModle);
        }



        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            base.OnMainFormClosed(sender, e);
            dataReceiver.RemoveHook();
            Application.Exit();
        }
    }
}
