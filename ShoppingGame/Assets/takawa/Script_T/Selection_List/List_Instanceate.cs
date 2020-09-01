using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

//テキストファイルの情報を全てリストに表示させ、myListにリストの名前を格納させる

public class List_Instanceate : MonoBehaviour
{
    string FilePath;//開きたいファイルまでのファイルパス
    [SerializeField] private GameObject List;//作成しておいたリスト
    [SerializeField] GameObject Containers; //コンテンツを入れて置くゲームオブジェクト
    GameObject List_Child;//Listの子オブジェクト
    TextMeshProUGUI Listnametext;//List_Childのテキスト
    public int List_num = 0;//Listの名前に番号を付けるのと、個数を数える
    public static GameObject Container;

    //テキストファイルの情報を全てリストに表示させ、myListにリストの名前を格納させる
    void Start()
    {
        FilePath = Application.dataPath + @"\List\_ListName.txt";

        //DirectoryInfo dir = new DirectoryInfo(FilePath);//指定したフォルダーの中身を全て読み込む
        //FileInfo[] info = dir.GetFiles("*.txt");

        string[] allText1 = File.ReadAllLines(FilePath);//指定したファイルを一行ずつ読み込む

        foreach (var s in allText1)//リストを一つずつ出現させ、テキストから読み取った内容を書き込ませる
        {
            Debug.Log("一つのリスト名を表示" + s);

            //Listオブジェクトの名前に番号を付ける
            List.name = "List" + List_num;
            List_num++;

            //リストを一つ作る
            Instantiate(List, new Vector3(Containers.transform.position.x, Containers.transform.position.y, Containers.transform.position.z), Quaternion.identity, Containers.transform);//- (myList.Count) * 200

            //リストの子にあるテキストを取得する
            List_Child = List.transform.GetChild(0).gameObject;
            Listnametext = List_Child.GetComponent<TextMeshProUGUI>();
            Debug.Log(List.transform.GetChild(0).gameObject);

            //読み込んだファイルの一行を今作ったリストの名前にする
            Listnametext.text = s;

        }
        Container = Containers;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
