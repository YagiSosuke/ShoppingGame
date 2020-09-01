using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//買い物を終了して次に進むかそれともまだ続けるかの処理

public class confirmationButton_Ctrl : MonoBehaviour
{
    [SerializeField] private GameObject confirmation_obj;//このゲームオブジェクト(ゲームを終了しますか?)
    [SerializeField] private Challenge_List challenge_List;//買い物リスト
    GameObject myList_parent;//一つの買い物リスト
    GameObject myList_Child;//myList_parentの子供
    Toggle ListToggle;//myList_parentのToggle
    int true_count = 0;//Toggleを一つだけ選んだかどうか
    // Start is called before the first frame update
    void Start()
    {

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
            Timer_Ctrl.count_up = false;
            confirmation_obj.SetActive(false);
            //SceneManager.LoadScene("Result");
        }
        else//全て押してなかったらウィンドウが閉じるだけ（ここに注意書きが流れるようにしたい）
        {
            confirmation_obj.SetActive(false);
            true_count = 0;
        }
        
    }

}
