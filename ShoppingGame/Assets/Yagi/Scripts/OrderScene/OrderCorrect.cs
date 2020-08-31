using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

/*依頼を完了するスクリプト*/

public class OrderCorrect : MonoBehaviour
{
    [SerializeField] InputField OrderInput;     //依頼先を入力するUI
    string OrderName;       //依頼先

    [SerializeField] GameObject SendPanel;     //保存時に表示するウィンドウ
    [SerializeField] Text SendPanelText;     //保存時に表示するウィンドウのテキスト
    [SerializeField] GameObject CorrectPanel;     //保存時に表示するウィンドウ

    [SerializeField] ListName ListScript;     //リストを管理するスクリプト

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //送信確認画面を表示するボタン
    public void OKButton()
    {
        SendPanel.SetActive(true);
        SendPanelText.text = "以上の内容を\n" + OrderInput.text +"\nに送信します";
    }

    //送信確認画面を見えなくするボタン
    public void BackButton()
    {
        SendPanel.SetActive(false);
    }

    //依頼を送信するボタン
    public void SendButton()
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
        CorrectPanel.SetActive(true);
    }

    //依頼を完了するボタン
    public void CorrectButton()
    {
        SceneManager.LoadScene("OrderScene");
    }
}
