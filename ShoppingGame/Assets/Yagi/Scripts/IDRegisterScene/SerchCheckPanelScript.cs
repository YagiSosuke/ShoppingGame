using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*検索結果確認パネルでのスクリプト、主にボタン系*/

public class SerchCheckPanelScript : MonoBehaviour
{
    string FilePath;        //家族のIDの保存場所
    [SerializeField] GameObject SerchCheckPanel;        //検索結果確認パネル

    [SerializeField] GameObject CorrectPanel;           //登録完了パネル
    [SerializeField] InputField InputFamilyID;          //家族のIDを書き込む場所

    // Start is called before the first frame update
    void Start()
    {
        FilePath = Application.dataPath + @"\Family\FamilyData.txt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //検索ID確認 - はいを押した時
    public void YesButton()
    {
        //ファイルに家族のIDを保存
        File.AppendAllText(FilePath, InputFamilyID.text + "\n");
        SerchCheckPanel.SetActive(false);       //登録確認パネルを消す
        CorrectPanel.SetActive(true);           //登録完了パネルを表示
    }

    //検索ID確認 - いいえを押した時
    public void NoButton()
    {
        SerchCheckPanel.SetActive(false);       //パネルを閉じる
    }
}
