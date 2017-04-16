using UnityEngine;
using Protocal;
using Newtonsoft.Json;
using System;
public partial class FormLoaderUser
{
    public enum FileType{
        Txt,
    }
}
public partial class FormLoaderUser {
    private static FormLoaderBehavier behaiver = default(FormLoaderBehavier);
    private static object lockHelper = new object();
    public static bool mManualReset = false;
    public static FormLoaderBehavier Behaiver
    {
        get
        {
            if (behaiver == null)
            {
                lock (lockHelper)
                {
                    GameObject go = new GameObject(typeof(FormLoaderBehavier).ToString());
                    behaiver = go.AddComponent<FormLoaderBehavier>();
                }
            }
            return behaiver;
        }
    }
    /// <summary>
    /// 场景打开就调用
    /// </summary>
    public static void InitEnviroment()
    {
        string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Demo")) + "FormLoader/FormLoader/bin/Debug/FormLoader.exe";
        Behaiver.TryOpenHelpExe(path);
    }
    public static void OpenFileDialog(string title, FileType fileType,string initialDirectory, Action<string> onReceive)
    {
        string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Demo")) + "Demo/FileDialogHelp/FileDialogHelp/bin/Debug/FileDialogHelp.dll";
        string clsname = "FileDialogHelp.FileDialog";
        string methodname = "OpenFileDialog";
        string filter = "";
        switch (fileType)
        {
            case FileType.Txt:
                filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                break;
            default:
                break;
        }
        object[] aregument = new object[] { title, filter, initialDirectory };
        DllFuction pro = new DllFuction(path, clsname, methodname, aregument);
        string text = JsonConvert.SerializeObject(pro);
        Behaiver.AddSendQueue(ProtocalType.dllfunction, text, onReceive);
        //Test(text);
    }

    static void Test(string x)
    {
        DllFuction protocal = JsonConvert.DeserializeObject<DllFuction>(x);
        System.Reflection.Assembly asb = System.Reflection.Assembly.LoadFrom(protocal.dllpath);
        var cls = asb.GetType(protocal.classname);
        var method = cls.GetMethod(protocal.methodname);
        var instence = Activator.CreateInstance(cls);
        string back = (string)method.Invoke(instence, protocal.argument);
        Debug.Log(back);
    }

}
