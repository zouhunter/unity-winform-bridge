using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Text;
using Newtonsoft.Json;

public class NewEditorTest {

    [Test]
    public void EditorTest()
    {
        string txt = "请求中文实现";
        byte[] byts = Encoding.UTF8.GetBytes(txt);
        string str = JsonConvert.SerializeObject(byts);
        foreach (var item in byts)
        {
            Debug.Log(item);
        }
        Debug.Log(str);
    }
}
