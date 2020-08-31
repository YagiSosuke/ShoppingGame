﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

//テキストファイルの情報を全てリストに表示させる

public class List_Instanceate : MonoBehaviour
{
    public List<GameObject> myList = new List<GameObject>();//リストを管理するオブジェクト
    public string fileName;        //ファイル名
    string FilePath;//開きたいファイルまでのファイルパス
    [SerializeField] private GameObject List;//作成しておいたリスト
    [SerializeField] GameObject Containers; //コンテンツを入れて置くゲームオブジェクト
    GameObject List_Child;
    TextMeshProUGUI Listnametext;
    bool One = false;

    // Start is called before the first frame update
    void Start()
    {
        FilePath= Application.dataPath + @"\List\";
        DirectoryInfo dir = new DirectoryInfo(FilePath);
        FileInfo[] info = dir.GetFiles("*.txt");
        foreach (var s in info)
        {
            Debug.Log(s.Name);
            if (One == true)
            {
                
                Instantiate(List, new Vector3(Containers.transform.position.x, Containers.transform.position.y - (myList.Count) * 200, Containers.transform.position.z), Quaternion.identity, Containers.transform);
                List_Child = List.transform.GetChild(0).gameObject;
                Listnametext = List_Child.GetComponent<TextMeshProUGUI>();
                Debug.Log(List.transform.GetChild(0).gameObject);
                Listnametext.text = s.Name;
                myList.Add(List_Child);
                Debug.Log("今startで処理したところ"+List.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
            }
            One = true;
            
        }

        foreach(var o in myList)
        {
            Debug.Log("myListに格納されているものの確認1" + myList[3].gameObject.GetComponent<TextMeshProUGUI>().text);
            //Debug.Log("myListに格納されているものの確認2"+o.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
