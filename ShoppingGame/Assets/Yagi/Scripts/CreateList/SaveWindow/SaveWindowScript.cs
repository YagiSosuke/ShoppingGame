using System.Collections;
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
    [SerializeField] InputField ListNameInput;      //リスト名を入力する領域

    
    [SerializeField] GameObject ErrorPanel;     //エラー時に表示するウィンドウ
    [SerializeField] Text ErrorText;            //エラーウィンドウに表示されるテキスト

    string nameFilePath;        //ファイル名を保存するときのファイルパス

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + @"\List\";
        nameFilePath = Application.dataPath + @"\List\_ListName.txt";
    }

    // Update is called once per frame
    void Update()
    {
        
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

        //textファイルにリスト内容を渡す
        File.AppendAllText(filePath+fileName+".txt", myString);

        //ファイル名を保存
        File.AppendAllText(nameFilePath, fileName+"\n");


        SavePanel.SetActive(false);
        SceneManager.LoadScene("CreateListScene");          //戻った画面を表示する
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
            ErrorText.text = "依頼する商品を入力してください";
        }
        //リストの項目がある場合は保存できる
        else if (list.ListLen > 0)
        {
            SavePanel.SetActive(true);
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
