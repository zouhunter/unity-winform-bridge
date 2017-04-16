using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class Demo : MonoBehaviour {
    public Text title;
    public Image image;
    void Start()
    {
        FormLoaderUser.InitEnviroment();
    }
    void OnGUI()
    {
        if (GUILayout.Button("打开文件选择窗口"))
        {
            FormLoaderUser.OpenFileDialog("打开文件选择窗口", FormLoaderUser.FileType.Txt,"C://", (x) =>
            {
                title.text = x;
            });
        }
        if (GUILayout.Button("关闭文件选择窗口"))
        {
            FormLoaderUser.SaveFileDialog("关闭文件选择窗口", FormLoaderUser.FileType.Txt, "C://", (x) =>
            {
                title.text = x;
            });
        }
        if (GUILayout.Button("颜色选择窗口"))
        {
            FormLoaderUser.ColorDialog(Color.red,(x)=> {
                image.color = x;
            });
        }
    }
}
