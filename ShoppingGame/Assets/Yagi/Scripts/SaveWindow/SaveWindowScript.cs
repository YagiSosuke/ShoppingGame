
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

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + @"\List\";
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
            myString += (list.ShoppingList[i] + "\n");
        }

        //textファイルにリスト内容を渡す
        File.AppendAllText(filePath+fileName+".txt", myString);

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
        SavePanel.SetActive(true);
    }
}
