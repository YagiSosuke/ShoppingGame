using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

//テキストファイルの情報を全てリストに表示させ、myListにリストの名前を格納させる

public class List_Instanceate : MonoBehaviour
{
    string FilePath;//開きたいファイルまでのファイルパス
    string FilePath2;//開きたいファイルまでのファイルパス
    [SerializeField] private GameObject List;//作成しておいたリスト
    [SerializeField] GameObject Containers; //コンテンツを入れて置くゲームオブジェクト
    GameObject List_Child;//Listの子オブジェクト
    Text Listnametext;//List_Childのテキスト
    public int List_num = 0;//Listの名前に番号を付けるのと、個数を数える
    public static GameObject Container;//コンテンツを入れて置くゲームオブジェクト(staticバージョン)
    [SerializeField] private GameObject List_detail;//作成しておいたリスト

    //テキストファイルの情報を全てリストに表示させ、myListにリストの名前を格納させる
    void Start()
    {
        #if UNITY_EDITOR        //デバッグ時
            FilePath = Application.dataPath + @"\List\_ListName.txt";
        #elif UNITY_ANDROID     //リリース時
            FilePath = Application.persistentDataPath + @"\List\_ListName.txt";
        #endif

        //DirectoryInfo dir = new DirectoryInfo(FilePath);//指定したフォルダーの中身を全て読み込む
        //FileInfo[] info = dir.GetFiles("*.txt");

        string[] allText1 = File.ReadAllLines(FilePath);//指定したファイルを一行ずつ読み込む

        foreach (var s in allText1)//リストを一つずつ出現させ、テキストから読み取った内容を書き込ませる
        {
            Debug.Log("一つのリスト名を表示" + s + "何個目:" + List_num);

            //Listオブジェクトの名前に番号を付ける
            List.name = "List" + List_num;
            

            //リストの子にあるテキストを取得する
            List_Child = List.transform.GetChild(0).gameObject;
            Listnametext = List_Child.GetComponent<Text>();
            //Debug.Log(List.transform.GetChild(0).gameObject);

            //読み込んだファイルの一行を今作ったリストの名前にする
            Listnametext.text = s;

            //リストを一つ作る
            Instantiate(List, new Vector3(Containers.transform.position.x, Containers.transform.position.y, Containers.transform.position.z), Quaternion.identity, Containers.transform);//- (myList.Count) * 200

            //今読み込んでいる一行分のテキストを渡してリストの詳細を生成する
            detail_instance(s);

            //番号を一つ増やす
            List_num++;

            Debug.Log("------------一つのリスト表示終了-----------------");
        }
        Container = Containers;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //リストの詳細を生成する関数
    void detail_instance(string text)
    {
        #if UNITY_EDITOR        //デバッグ時
            FilePath2 = Application.dataPath + @"\List\" + text + ".txt";
        #elif UNITY_ANDROID     //リリース時
            FilePath2 = Application.persistentDataPath + @"\List\" + text + ".txt";
        #endif

        string[] allText2 = File.ReadAllLines(FilePath2);//指定したファイルを一行ずつ読み込む

        foreach (var s2 in allText2)//リストの詳細を一つずつ出現させ、テキストから読み取った内容を書き込ませる
        {
            //詳細オブジェクトの名前に番号を付ける
            List_detail.name ="List" + List_num+ "(Clone)details";

            //リストの詳細の子にあるテキストを取得する
            List_Child = List_detail.transform.GetChild(0).gameObject;
            Listnametext = List_Child.GetComponent<Text>();
            //Debug.Log(List_detail.transform.GetChild(0).gameObject);

            //読み込んだファイルの一行を今作ったリストの詳細の名前にする
            Listnametext.text = s2;

            //リストの詳細を一つ作る
            Instantiate(List_detail, new Vector3(0, 0, 0), Quaternion.identity, Containers.transform);//Containers.transform.position.y//- (myList.Count) * 200
            
        }
    }
}
