﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*保存するスクリプト*/

public class SaveWindowScript : MonoBehaviour
{
    string filePath;        //保存先のファイルパス
    string fileName;        //ファイル名
    [SerializeField] ListName list;     //リストを管理するスクリプト
    [SerializeField] GameObject SavePanel;     //保存時に表示するウィンドウ
    [SerializeField] Text SavePanelText;        //保存ウィンドウのテキスト
    [SerializeField] GameObject SaveCorrectPanel;     //保存完了時に表示するウィンドウ
    [SerializeField] InputField ListNameInput;      //リスト名を入力する領域

    
    [SerializeField] GameObject ErrorPanel;     //エラー時に表示するウィンドウ
    [SerializeField] Text ErrorText;            //エラーウィンドウに表示されるテキスト

    string nameFilePath;        //ファイル名を保存するときのファイルパス
    

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR        //デバッグ時
            filePath = Application.dataPath + @"\List\";
            nameFilePath = Application.dataPath + @"\List\_ListName.txt";
        #elif UNITY_STANDALONE
            filePath = Application.persistentDataPath + @"\List\";
            nameFilePath = Application.persistentDataPath + @"\List\_ListName.txt";
        #elif UNITY_ANDROID  //リリース時
            filePath = Application.persistentDataPath + @"\List\";
            nameFilePath = Application.persistentDataPath + @"\List\_ListName.txt";
        #endif
        
        //ファイルが無かった時にファイルを作成
        if (!File.Exists(nameFilePath))
        {
            Directory.CreateDirectory(filePath);
            File.AppendAllText(nameFilePath, "");
            Debug.Log("ファイル作成完了2");
        }

    }
    
    //保存する
    public void SaveButton()
    {
        string myString = "";       //ファイル内容をまとめて格納

        fileName = ListNameInput.text;      //ファイル名を取得



        //入力された内容をstring型配列にコピー
        for (int i = 0; i < list.ListLen; i++)
        {
            list.ShoppingList.Add(list.ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text);
            //項目内容が""でなければ
            if (list.ShoppingList[i] != "")
            {
                myString += (list.ShoppingList[i] + "\n");
            }
        }


        //ファイルが存在しない場合、ファイルを作成
        if (!File.Exists(filePath + fileName + ".txt"))
        {
            File.Create(filePath + fileName + ".txt");
        }

        //textファイルにリスト内容を渡す
        File.AppendAllText(filePath+fileName+".txt", myString);
        //ファイル名を保存
            File.AppendAllText(nameFilePath, fileName+"\n");

        SaveCorrectPanel.SetActive(true);       //保存完了パネルを開く
        SavePanel.SetActive(false);             //保存確認パネルを閉じる
    }

    //ウィンドウを閉じる
    public void BackButton()
    {
        SavePanel.SetActive(false);
    }

    //セーブウィンドウを開く
    public void WindowOpen()
    {
        string myString = "";       //ファイル内容をまとめて格納

        //入力された内容をstring型配列にコピー
        for (int i = 0; i < list.ListLen; i++)
        {
            //項目内容が""でなければ
            if (list.ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text != "")
            {
                myString += (list.ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text + "\n");
            }
        }
        

        //リストの項目があっても、中身が空なら保存できない
        if (list.ListLen > 0 && myString == "")
        {
            ErrorPanel.SetActive(true);
            ErrorText.text = "依頼する商品を\n入力してください";
        }
        //リスト名が無いなら保存できない
        else if (ListNameInput.text == "")
        {
            ErrorPanel.SetActive(true);
            ErrorText.text = "リスト名を入力してください";
        }
        //リストの項目がある場合は保存できる
        else if (list.ListLen > 0)
        {
            //リスト名が既に保存されてるものと被るなら保存できない
            string[] AllList = File.ReadAllLines(nameFilePath);
            bool AlreadyF = false;      //既に準備されてるかを判断するフラグ

            //リストの検索
            for(int i = 0; i < AllList.Length; i++)
            {
                //既にリスト名が被っていたら
                if(AllList[i] == ListNameInput.text)
                {
                    AlreadyF = true;
                }
            }

            Debug.Log("ListNameInput.text.IndexOf(\"a\") = " + ListNameInput.text.IndexOf('a'));

            //リスト名が被っていたなら
            if (AlreadyF)
            {
                ErrorPanel.SetActive(true);
                ErrorText.text = "そのリスト名は\n既に使用されています\n別のものを入力してください";
            }
            //ファイル名に使用できない文字が入っていたら
            else if (ListNameInput.text.IndexOf("\\") >= 0 ||
                     ListNameInput.text.IndexOf("/") >= 0 ||
                     ListNameInput.text.IndexOf(":") >= 0 ||
                     ListNameInput.text.IndexOf("*") >= 0 ||
                     ListNameInput.text.IndexOf("?") >= 0 ||
                     ListNameInput.text.IndexOf("\"") >= 0 ||
                     ListNameInput.text.IndexOf("<") >= 0 ||
                     ListNameInput.text.IndexOf(">") >= 0 ||
                     ListNameInput.text.IndexOf("|") >= 0 ||
                     ListNameInput.text.IndexOf(".") >= 0)
            {
                ErrorPanel.SetActive(true);
                ErrorText.text = "リスト名に\n\\ / : + ? \" < > | .\nは使用できません\n他のものを入力してください";
            }
            //リスト名が被っていないなら
            else
            {
                SavePanel.SetActive(true);
                SavePanelText.text = "「" + ListNameInput.text + "」\nこのファイル名で保存します";
            }
        }
        //リストの項目がない場合は保存できない
        else
        {
            ErrorPanel.SetActive(true);
            ErrorText.text = "リストの項目は\n最低でも1つ入れてください";
        }
    }


    //リスト名 - スペースキーが押されたとき
    //中身を空にする
    public void DeleteSpace()
    {
        if(ListNameInput.text == " " || ListNameInput.text == "　")
        {
            ListNameInput.text = "";
        }
    }
}
