﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

//Time_Attackシーンで選択したリストの詳細を表示させる
public class Challenge_List : MonoBehaviour
{
    string FilePath;//開きたいファイルまでのファイルパス
    [SerializeField] private GameObject List;//作成しておいたリスト
    [SerializeField] GameObject Containers; //コンテンツを入れて置くゲームオブジェクト
    GameObject List_Child;//Listの子オブジェクト
    TextMeshProUGUI Listnametext;//List_Childのテキスト
    public int List_num = 0;//Listの名前に番号を付けるのと、個数を数える

    // Start is called before the first frame update
    void Start()
    {
        FilePath = Application.dataPath + @"\List\" + Selection_List_Move_Scene.fileName + ".txt";
        Debug.Log("TimeAttackのfilepath:" + FilePath);

        string[] allText1 = File.ReadAllLines(FilePath);//指定したファイルを一行ずつ読み込む

        foreach (var s in allText1)//リストを一つずつ出現させ、テキストから読み取った内容を書き込ませる
        {
            Debug.Log("一つのリスト名を表示" + s);

            //Listオブジェクトの名前に番号を付ける
            List.name = "List" + List_num;
            List_num++;

            //リストの子にあるテキストを取得する
            List_Child = List.transform.GetChild(0).gameObject;
            Listnametext = List_Child.GetComponent<TextMeshProUGUI>();
            Debug.Log(List.transform.GetChild(0).gameObject);

            //読み込んだファイルの一行を今作ったリストの名前にする
            Listnametext.text = s;

            //リストを一つ作る
            Instantiate(List, new Vector3(Containers.transform.position.x, Containers.transform.position.y, Containers.transform.position.z), Quaternion.identity, Containers.transform);//- (myList.Count) * 200

        }
        Debug.Log("リストの数:"+List_num);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
