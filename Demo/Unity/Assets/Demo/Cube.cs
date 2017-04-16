using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using MessageTrans.Interal;

public class Cube : MonoBehaviour {
    void Start()
    {
        FormLoaderUser.ReigsterCube(this);
    }
    public void turnBig()
    {
        transform.localScale *= 2;
    }
    public void turnSmall()
    {
        transform.localScale *= 0.5f;
    }
    public void toLeft()
    {
        transform.localPosition += Vector3.left;
    }
    public void toRight()
    {
        transform.localPosition += Vector3.right;
    }
    public void toDown()
    {
        transform.localPosition += Vector3.down;
    }
    public void toUp()
    {
        transform.localPosition += Vector3.up;
    }
}
