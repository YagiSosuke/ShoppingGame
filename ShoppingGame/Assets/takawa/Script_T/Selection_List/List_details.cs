using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

//Selection_Listでリストのボタンを押したらリストの詳細を表示させる
public class List_details : MonoBehaviour
{
    private bool details_button = false;//リスト詳細が表示されているかいないか
    [SerializeField] private GameObject button_text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Selection_Listでリストのボタンを押したらリストの詳細を表示させる
    public void details()
    {
        if (details_button == false)//詳細が表示されていなかったら表示させる
        {
            //詳細を表示するボタンの表記を変える
            button_text.GetComponent<Text>().text = "▼";
            //foreachでコンテンツの子オブジェクトを全て取得し、
            foreach (Transform Container_Child in List_Instanceate.Container.transform)
            {
                Debug.Log(this.transform.parent.gameObject.name);
                //if文で詳細の名前についてある番号から該当するものを全て取得する
                if (Container_Child.gameObject.name == this.transform.parent.gameObject.name + "details(Clone)")
                {
                    //その取得したもののsetActiveを全てtrueにする
                    Container_Child.gameObject.SetActive(true);
                }
            }
            details_button = true;//全部表示された
        }
        else//詳細が表示されていたら消す
        {
            //詳細を表示するボタンの表記を変える
            button_text.GetComponent<Text>().text = "◀";
            //foreachでコンテンツの子オブジェクトを全て取得し、
            foreach (Transform Container_Child in List_Instanceate.Container.transform)
            {
                Debug.Log(this.transform.parent.gameObject.name);
                //if文で詳細の名前についてある番号から該当するものを全て取得する
                if (Container_Child.gameObject.name == this.transform.parent.gameObject.name + "details(Clone)")
                {
                    //その取得したもののsetActiveを全てfalseにする
                    Container_Child.gameObject.SetActive(false);
                }
            }
            details_button = false;//全部消した
        }
        
        
        
    }
}
