using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using MessageTrans;
using MessageTrans.Interal;
public class JsonTest {

    [Test]
    public void EditorTest()
    {
        JSONClass cla = new JSONClass();
        cla["key"] = "key0";
        cla["value"] = "value";
        Debug.Log(cla.ToString());
    }
}
