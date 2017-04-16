using UnityEngine;
using Protocal;
using Newtonsoft.Json;
using System;
using System.Reflection;
using MessageTrans.Interal;
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
        Behaiver.AddSendQueue(ProtocalType.dllfunction, text);
        Behaiver.RegisteReceive(ProtocalType.dllfunction, onReceive);

        //Test(text);
    }
    public static void SaveFileDialog(string title, FileType fileType, string initialDirectory, Action<string> onReceive)
    {
        string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Demo")) + "Demo/FileDialogHelp/FileDialogHelp/bin/Debug/FileDialogHelp.dll";
        string clsname = "FileDialogHelp.FileDialog";
        string methodname = "SaveFileDialog";
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
        Behaiver.AddSendQueue(ProtocalType.dllfunction, text);
        Behaiver.RegisteReceive(ProtocalType.dllfunction, onReceive);
        //Test(text);
    }
    public static void ColorDialog(Color color, Action<Color> onReceive)
    {
        string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Demo")) + "Demo/FileDialogHelp/FileDialogHelp/bin/Debug/FileDialogHelp.dll";
        string clsname = "FileDialogHelp.FileDialog";
        string methodname = "ColorDialog";
        string col = "#" + ColorUtility.ToHtmlStringRGBA(color);
        object[] aregument = new object[] { col };
        DllFuction pro = new DllFuction(path, clsname, methodname, aregument);
        string text = JsonConvert.SerializeObject(pro);
        Action<string> action = (x) =>
        {
            Color receiveColor;
            if (ColorUtility.TryParseHtmlString(x,out receiveColor))
            {
                onReceive(receiveColor);
            }
            else
            {
                Debug.LogWarning(x);
            }
        };
        Behaiver.AddSendQueue(ProtocalType.dllfunction, text);
        Behaiver.RegisteReceive(ProtocalType.dllfunction, action);
        //Test(text);
    }
    public static void LunchCubeCtrl(Action<string> onLunched)
    {
        string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Demo")) + "Demo/CubeMove/CubeMove/bin/Debug/CubeMove.dll";
        string clsname = "CubeMove.Program";
        string methodname = "Main";
        HolderLunchFunc pro = new HolderLunchFunc("CubeMove", "RegisterHolder", path, clsname, methodname, null);
        string text = JsonConvert.SerializeObject(pro);
        Behaiver.AddSendQueue(ProtocalType.lunchholder, text);
        Behaiver.RegisteReceive(ProtocalType.lunchholder,onLunched);
        //LunchCommnuicateHolderTest(text);
    }
    public static void CommuateInfo(string methodName,object[] argument)
    {
        Protocal.ChartData data = new Protocal.ChartData();
        data.arguments = argument;
        data.methodName = methodName;
        data.holderName = "CubeMove";
        string text = JsonConvert.SerializeObject(data);
        Behaiver.AddSendQueue(ProtocalType.communicate, text);
    }
    public static void ReigsterCube(Cube cube)
    {
        try
        {
            Behaiver.RegisteReceive(ProtocalType.communicate, (txt) =>
            {
                Debug.Log(txt);
                JSONNode node = JSONNode.Parse(txt);
                string methodName = node.AsObject["methodName"].Value;
                var method = cube.GetType().GetMethod(methodName);
                method.Invoke(cube, null);
            });
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    static void LunchCommnuicateHolderTest(string x)
    {
        try
        {
            HolderLunchFunc protocal = JsonConvert.DeserializeObject<HolderLunchFunc>(x);
            Assembly asb = Assembly.LoadFrom(protocal.dllData.dllpath);
            var cls = asb.GetType(protocal.dllData.classname);
            var method = cls.GetMethod(protocal.dllData.methodname);
            var instence = Activator.CreateInstance(cls);
            string back = (string)method.Invoke(instence, protocal.dllData.argument);
           
            var registerMethod = cls.GetMethod(protocal.holderRegistFunc);
            //method.Invoke(instence, new object[] { new Action<string>() });
        }
        catch (Exception e)
        {
            Debug.Log(e);
            //MessageBox.Show(e.Message);
        }

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
