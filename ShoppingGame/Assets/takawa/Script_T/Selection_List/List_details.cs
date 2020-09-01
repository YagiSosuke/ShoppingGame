using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

//Selection_Listでボタンを押したらリストの詳細を表示させる（未完成）
public class List_details : MonoBehaviour
{
    string FilePath;//開きたいファイルまでのファイルパス
    string FileName;
    GameObject List_Child;//Listの子オブジェクト
    [SerializeField] private GameObject List_detail;//作成しておいたリスト
    TextMeshProUGUI Listnametext;//List_Childのテキスト

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void details()
    {
        FileName = this.GetComponent<TextMeshProUGUI>().text;
        FilePath = Application.dataPath + @"\List\" + FileName + ".txt";

        string[] allText1 = File.ReadAllLines(FilePath);//指定したファイルを一行ずつ読み込む

        foreach (var s in allText1)//リストを一つずつ出現させ、テキストから読み取った内容を書き込ませる
        {
            Debug.Log("一つのリスト名を表示" + s);

            //リストを一つ作る

            Instantiate(List_detail, new Vector3(0, 0, 0), Quaternion.identity, this.transform);//Containers.transform.position.y//- (myList.Count) * 200
            
            List_detail.transform.parent = GameObject.Find("コンテンツ").transform;
            List_detail.transform.SetSiblingIndex(1);

            //リストの子にあるテキストを取得する
            List_Child = List_detail.transform.GetChild(0).gameObject;
            Listnametext = List_Child.GetComponent<TextMeshProUGUI>();
            Debug.Log(List_detail.transform.GetChild(0).gameObject);

            //読み込んだファイルの一行を今作ったリストの名前にする
            Listnametext.text = s;

        }
    }
}
