using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*リスト削除確認パネルのスクリプト*/

public class ListDeletePanelScript : MonoBehaviour
{
    [SerializeField] GameObject ListDeletePanel;
    public GameObject ListContainer;

    string filePath;        //リストデータ
    string nameFilePath;    //リスト名データ
    string fileName;        //ファイル名

    //パネルを開く
    public void PanelOpen()
    {
        ListDeletePanel.SetActive(true);
    }

    //パネルを閉じる
    public void PanelClose()
    {
        ListDeletePanel.SetActive(false);
    }

    //リストを削除する
    public void DeleteList()
    {
        //ファイル情報を削除        
        //ファイル名を設定
        fileName = ListContainer.transform.GetChild(0).GetComponent<Text>().text;

        #if UNITY_EDITOR        //デバッグ時
            filePath = Application.dataPath + @"\List\" + fileName + ".txt";
            nameFilePath = Application.dataPath + @"\List\_ListName.txt";
        #elif UNITY_STANDALONE     //リリース時
            filePath = Application.persistentDataPath + @"\List\" + fileName + ".txt";
            nameFilePath = Application.persistentDataPath + @"\List\_ListName.txt";
        #elif UNITY_ANDROID     //リリース時
            filePath = Application.persistentDataPath + @"\List\" + fileName + ".txt";
            nameFilePath = Application.persistentDataPath + @"\List\_ListName.txt";
        #endif
        //ファイルが存在するとき
        if(File.Exists(filePath)){
            //ファイルを削除
            File.Delete(filePath);
        }
        //ファイル名要素を取得
        string[] element = File.ReadAllLines(nameFilePath);
        //ファイル要素を再構成
        string elementLine = "";
        for(int i = 0; i < element.Length; i++)
        {
            if(element[i] != fileName)
            {
                elementLine += element[i] + "\n";
            }
        }
        File.WriteAllText(nameFilePath, elementLine);

        //リストを削除する
        Destroy(ListContainer);

        //リスト詳細を削除する
        string childName = ListContainer.name + "details(Clone)";
        GameObject[] child = GameObject.FindGameObjectsWithTag("ListDetail");
        foreach (GameObject Obj in child)
        {
            if (Obj.name == childName)
            {
                Destroy(Obj.gameObject);
            }
        }

        //パネルを閉じる
        PanelClose();
    }
}
