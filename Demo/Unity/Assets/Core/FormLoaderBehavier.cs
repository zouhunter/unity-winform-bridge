using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System;
using MessageTrans;
using MessageTrans.Interal;
using FormSwitch.Internal;
using FormSwitch;
using Newtonsoft.Json;
using Protocal;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;

public class FormLoaderBehavier : MonoBehaviour {

    IWindowSwitchCtrl windowswitch;
    DataReceiver receiver;
    DataSender childSender;
    Thread sendThread;

    Queue<KeyValuePair<string, string>> sendQueue = new Queue<KeyValuePair<string, string>>();
    Dictionary<ProtocalType, Action<string>> waitDic = new Dictionary<ProtocalType, Action<string>>();

    private void Awake()
    {
        receiver = new MessageTrans.DataReceiver();
        receiver.RegistHook();
        childSender = new MessageTrans.DataSender();
        sendThread = new Thread(SendThread);
    }

    private void SendThread()
    {
        while (true)
        {
            Thread.Sleep(100);
            if (sendQueue.Count > 0 && windowswitch.Child != IntPtr.Zero)
            {
                KeyValuePair<string, string> data = sendQueue.Dequeue();
                childSender.SendMessage(data.Key, data.Value);
            }
        }
        
    }

    public void TryOpenHelpExe(string exePath)
    {
        windowswitch = new WindowSwitchUnity();
        string path = exePath;// Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Demo")) + "FormLoader/FormLoader/bin/Debug/FormLoader.exe";
        if (System.IO.File.Exists(path))
        {
#if UNITY_EDITOR
            if (windowswitch.OpenChildWindow(path, false, "1"))
#else
            if (windowswitch.OpenChildWindow(path, false))
#endif
            {
                //打开子应用程序
                StartCoroutine(DelyRegisterSender());
            }
        }
        else
        {
            Debug.LogError("exe not fond");
        }
        sendThread.Start();
    }
    private IEnumerator DelyRegisterSender()
    {
        while (windowswitch.Child == IntPtr.Zero){
            yield return null;
        }
        childSender.RegistHandle(windowswitch.Child);
#if UNITY_EDITOR
        yield return ReadPathSelect();
#endif
    }

#if UNITY_EDITOR
    private IEnumerator ReadPathSelect()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            foreach (var item in waitDic)
            {
                try
                {
                    string path = System.IO.Directory.GetCurrentDirectory() + "/" + item.Key+ ".txt";
                    if (System.IO.File.Exists(path))
                    {
                        string receivedText = System.IO.File.ReadAllText(path);
                        if (item.Value != null) item.Value.Invoke(receivedText);
                        System.IO.File.Delete(path);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(item.Key +"[err]" + e);
                }
            }
            
          
        }
    }
#endif

    public void AddSendQueue(ProtocalType type,string value)
    {
        sendQueue.Enqueue(new KeyValuePair<string, string>(type.ToString(), value));
    }
    public void RegisteReceive(ProtocalType type, Action<string> onReceive)
    {
        if (onReceive != null) receiver.RegisterEvent(type.ToString(), onReceive);
#if UNITY_EDITOR
        waitDic[type] = onReceive;
#endif
    }
    private void OnDestroy()
    {
        receiver.RemoveHook();
        windowswitch.CloseChildWindow();
        windowswitch.OnCloseThisWindow();
        sendThread.Abort();
    }
}
