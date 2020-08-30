using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

/*依頼を完了するスクリプト*/

public class OrderCorrect : MonoBehaviour
{
    [SerializeField] InputField OrderInput;     //依頼先を入力するUI
    string OrderName;       //依頼先

    [SerializeField] ListName ListScript;     //リストを管理するスクリプト

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //依頼を完了するボタン
    public void OrderButton()
    {
        OrderName = OrderInput.text;        //依頼先を取得

        //リストの回数繰り返す
        for (int i = 0; i < ListScript.ListLen; i++)
        {
            //リストを取得
            string List = ListScript.ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text;

            //サーバ - データストアに値をアップロード
            NCMBObject OrderClass = new NCMBObject(OrderName);      //サーバ - 依頼先のクラスを作成
            OrderClass["message"] = List;                           //値を設定
            OrderClass.SaveAsync();             // データストアへの登録
        }
    }
}
