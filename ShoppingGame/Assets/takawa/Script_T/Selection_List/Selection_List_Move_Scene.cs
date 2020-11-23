using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//次のシーンに進むか前のシーンに戻るかの処理

public class Selection_List_Move_Scene : MonoBehaviour
{
    [SerializeField] private List_Instanceate list_Instanceate;//list_Instanceateの値を使う
    public GameObject myList_parent;//選択したリスト
    GameObject myList_Child;//myList_parentの子オブジェクト
    Toggle ListToggle;//選択したリストのToggle
    //TextMeshProUGUI Listnametext;//選択したリストに書いてある
    public static string fileName;//Toggleで選んだリストのファイル名
    int true_count = 0;//Toggleを一つだけ選んだかどうか

    [SerializeField] GameObject AttentionPanel;             //ゲーム開始前に表示するパネル
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextScene()//選択したリストのデータを保持したまま次のシーンへ行く
    {
        /*幾つかコメントしました*/
        /*トグルを押した瞬間に呼び出しています*/
        /*Asset/Yagi/Script/SelectionList/ListCheckButtonScript.cs で呼び出し*/

        //全てのリストのToggleを探して、trueになっているところのリストの内容をfileNameに格納させる
        //for (int i = 0; i < list_Instanceate.List_num; i++)
        //{
        //myList_parent = GameObject.Find("List" + i + "(Clone)");
            Debug.Log("取得確認" + myList_parent);
            myList_Child = myList_parent.transform.GetChild(1).gameObject;
            ListToggle = myList_Child.GetComponent<Toggle>();

            if (ListToggle.isOn == true)//もしtrueになっているものがあったら、そのリストの内容をfileNameに格納させる
            {
                Debug.Log("選んだリスト:" + myList_parent.transform.GetChild(0).gameObject.GetComponent<Text>().text);
                fileName = myList_parent.transform.GetChild(0).gameObject.GetComponent<Text>().text;
                true_count++;
                Debug.Log("fileName:" + fileName);
                
                ListToggle.isOn = false;        //追加 - チェックボックスを外す
                Debug.Log("ListToggle.isOn = " + ListToggle.isOn);
            }
        //}
        if (true_count == 1)//一つだけリストを選んでいたら次のシーンに行ける
        {
            AttentionPanel.SetActive(true);     //開始前パネルを開く
            true_count++;
            /*
            Debug.Log("次のシーンへ");
            SceneManager.LoadScene("Time_Attack");
            */
        }
        else//0か1つ以上リストを選んでいたら次のシーンに行けない
        {
            true_count = 0;
        }

    }

    public void BackScene()//値をリセットしてタイトルに戻る
    {
        Timer_Ctrl.total_time = 0;
        Timer_Ctrl.second = 0;
        Timer_Ctrl.minute = 0;
        Stop_Button.One = true;
        SceneManager.LoadScene("Title");
    }
}
