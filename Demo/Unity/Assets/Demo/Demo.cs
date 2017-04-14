using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class Demo : MonoBehaviour {
    void Start()
    {
        FormLoaderUser.InitEnviroment();
    }
    void OnGUI()
    {
        if (GUILayout.Button("打开文件选择窗口"))
        {
            FormLoaderUser.OpenFileDialog("txt", (x) =>
            {
                Debug.Log(x);
            });
        }
    }
}
