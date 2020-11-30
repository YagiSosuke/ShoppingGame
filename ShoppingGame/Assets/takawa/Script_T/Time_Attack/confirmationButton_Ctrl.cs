using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using NCMB;

//買い物を終了して次に進むかそれともまだ続けるかの処理

public class confirmationButton_Ctrl : MonoBehaviour
{
    [SerializeField] private GameObject confirmation_obj;//このゲームオブジェクト(ゲームを終了しますか?)
    [SerializeField] private Challenge_List challenge_List;//買い物リスト
    GameObject myList_parent;//一つの買い物リスト
    GameObject myList_Child;//myList_parentの子供
    Toggle ListToggle;//myList_parentのToggle
    int true_count = 0;//Toggleを一つだけ選んだかどうか
    string myID;//自分のアカウントのID
    // Start is called before the first frame update
    void Start()
    {
        //自分のアカウントのIDを取得
        myID = PlayerPrefs.GetString("IDCreateYet");
        Debug.Log(myID);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //キャンセルボタンを押したとき、他のボタンを再び押せるようにし、このゲームオブジェクトを見えなくさせる
    public void Cancel()
    {
        confirmation_obj.SetActive(false);
    }

    //OKボタンを押したとき、全てのリストのToggleに片方チェックが入っていたら、
    //時間を止めて次のシーンに行けるようにする
    public void NextScene()
    {
        //全てのToggleの状態を確認し、全てのリストに一つずつチェックがついているかどうか確認する
        for (int i = 0; i < challenge_List.List_num; i++)
        {
            //Listのオブジェクトについている「依頼されたものをゲットした」にあたるToggleを取得する
            myList_parent = GameObject.Find("List" + i + "(Clone)");
            Debug.Log("取得確認" + myList_parent);
            myList_Child = myList_parent.transform.GetChild(1).gameObject;
            ListToggle = myList_Child.GetComponent<Toggle>();

            //「依頼されたものをゲットした」にチェックがついたら
            if (ListToggle.isOn == true)
            {
                true_count++;
            }

            //Listのオブジェクトについている「依頼されたものをゲットできなかった」にあたるToggleを取得する
            myList_Child = myList_parent.transform.GetChild(2).gameObject;
            ListToggle = myList_Child.GetComponent<Toggle>();

            //「依頼されたものをゲットできなかった」にチェックがついたら
            if (ListToggle.isOn == true)
            {
                true_count++;
            }
            
        }

        if (true_count == challenge_List.List_num)//すべてチェックを押したら時間を止めて結果画面へ行く
        {
            Debug.Log("次のシーンへ");
            for (int i = 0; i < challenge_List.List_num; i++)
            {
                //Listのオブジェクトについている「依頼されたものをゲットした」にあたるToggleを取得する
                myList_parent = GameObject.Find("List" + i + "(Clone)");
                Debug.Log("①取得確認" + myList_parent);
                myList_Child = myList_parent.transform.GetChild(1).gameObject;
                ListToggle = myList_Child.GetComponent<Toggle>();

                //依頼されたものをゲットしたら削除する
                if (myList_parent.tag == "request" && ListToggle.isOn == true)
                {
                    Debug.Log(myList_parent.transform.GetChild(0).GetComponent<Text>().text + "を削除する");
                    ////Notification_completedクラスの内容を取得
                    //NCMBObject obj = new NCMBObject("_" + myID);
                    //NCMBObject objDelete = new NCMBObject("_" + myID);
                    //objDelete["message"] = "おいうえあ";
                    ////objDelete.ObjectId = obj.ObjectId;  //obj.ObjectIdに保存時のIDが保存されている
                    //objDelete.DeleteAsync((NCMBException e) => {
                    //    if (e != null)
                    //    {
                    //        //エラー処理
                    //        Debug.Log("削除失敗");
                    //    }
                    //    else
                    //    {
                    //        //成功時の処理
                    //        Debug.Log("削除成功");
                    //    }
                    //});

                    NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("_" + myID);
                    //messageの値が該当する削除させるものと一致するオブジェクト検索
                    query.WhereEqualTo("message", myList_parent.transform.GetChild(0).GetComponent<Text>().text);  //検索条件
                    query.FindAsync((List<NCMBObject> objList, NCMBException e) => {  //検索
                        if (e != null)
                        {
                            //検索失敗時の処理
                            Debug.Log("検索失敗");
                        }
                        else
                        {
                            //messageの中で削除させるもののオブジェクトを出力
                            foreach (NCMBObject obj in objList)
                            {
                                obj.DeleteAsync((NCMBException ee) => {  // 削除
                                    if (ee != null)
                                    {
                                        //エラー処理
                                        Debug.Log("削除失敗");
                                    }
                                    else
                                    {
                                        //成功時の処理
                                        Debug.Log("削除成功");
                                    }
                                });
                            }
                        }
                    });

                }
            }
            Timer_Ctrl.count_up = false;
            confirmation_obj.SetActive(false);
            SceneManager.LoadScene("Result");
        }
        else//全て押してなかったらウィンドウが閉じるだけ（ここに注意書きが流れるようにしたい）
        {
            confirmation_obj.SetActive(false);
            true_count = 0;
        }
        
    }

}
