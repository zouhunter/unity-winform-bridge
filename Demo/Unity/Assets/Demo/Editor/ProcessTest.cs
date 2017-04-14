using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;
using System.Diagnostics;
public class ProcessTest {

    [Test]
    public void EditorTest()
    {
        ProcessStartInfo info = new ProcessStartInfo();
        Process pro = Process.Start(info);
    }
}
