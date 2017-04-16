using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class Demo : MonoBehaviour {
    public Text title;
    public Image image;

    public Button openFileBtn;
    public Button saveFileBtn;
    public Button colorBtn;
    public Button lunchCommuBtn;
    public Button simpleTextBtn;
    public InputField txtField;
    void Start()
    {
        FormLoaderUser.InitEnviroment();
        openFileBtn.onClick.AddListener(openFileBtnClicked);
        saveFileBtn.onClick.AddListener(saveFileBtnClicked);
        colorBtn.onClick.AddListener(colorBtnClicked);
        lunchCommuBtn.onClick.AddListener(lunchCommuBtnClicked);
        simpleTextBtn.onClick.AddListener(CommuCatBtnClicked);
    }

    private void openFileBtnClicked()
    {
        FormLoaderUser.OpenFileDialog("打开文件选择窗口", FormLoaderUser.FileType.Txt, "C://", (x) =>
        {
            title.text = x;
        });
    }
    private void saveFileBtnClicked()
    {
        FormLoaderUser.SaveFileDialog("关闭文件选择窗口", FormLoaderUser.FileType.Txt, "C://", (x) =>
        {
            title.text = x;
        });
    }
    private void colorBtnClicked()
    {
        FormLoaderUser.ColorDialog(Color.red, (x) => {
            image.color = x;
        });
    }

    private void lunchCommuBtnClicked()
    {
        FormLoaderUser.LunchCubeCtrl((x) => {
            Debug.Log(x);
        });
    }

    private void CommuCatBtnClicked()
    {
        FormLoaderUser.CommuateInfo("SimpleTxt", new object[] { txtField.text });
    }

}
