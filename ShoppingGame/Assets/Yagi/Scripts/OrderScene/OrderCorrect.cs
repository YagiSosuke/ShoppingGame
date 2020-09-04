using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

/*依頼を完了するスクリプト*/

public class OrderCorrect : MonoBehaviour
{
    [SerializeField] Dropdown OrderInput;     //依頼先を入力するUI
    string OrderName;       //依頼先

    [SerializeField] GameObject SendPanel;     //保存時に表示するウィンドウ
    [SerializeField] Text SendPanelText;     //保存時に表示するウィンドウのテキスト
    [SerializeField] GameObject CorrectPanel;     //保存時に表示するウィンドウ

    [SerializeField] ListName ListScript;     //リストを管理するスクリプト

    [SerializeField] Dropdown dropdown;         //ドロップダウンメニュー

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
        SendPanelText.text = "以上の内容を\n" + OrderName +"\nに送信します";
    }

    //送信確認画面を見えなくするボタン
    public void BackButton()
    {
        SendPanel.SetActive(false);
    }

    //依頼を送信するボタン
    public void SendButton()
    {
        //依頼先を名前 -> IDに変える必要がある
        //Nameが指定されたもののIDを検索する

        //UserIDsを検索するクラスを作成
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //Scoreの値が7と一致するオブジェクト検索
        query.WhereEqualTo("Name", OrderName);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null)
            {
                //検索失敗時の処理
                Debug.Log("検索に失敗しました");
            }
            else {
                //サーバに書き込み
                foreach (NCMBObject obj in objList)
                {
                    //リストの回数繰り返す
                    for (int i = 0; i < ListScript.ListLen; i++)
                    {
                        //リストを取得
                        string List = ListScript.ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text;

                        //サーバ - データストアに値をアップロード
                        NCMBObject OrderClass = new NCMBObject("_" + (string)obj["ID"]);      //サーバ - 依頼先のクラスを作成
                        OrderClass["message"] = List;       //値を設定
                        OrderClass.SaveAsync();             // データストアへの登録
                    }
                    CorrectPanel.SetActive(true);
                }
            }
        });        
    }

    //依頼を完了するボタン
    public void CorrectButton()
    {
        SceneManager.LoadScene("OrderScene");
    }

    //ドロップダウンメニューのテキストを取得するスクリプト
    public void GetValue(int value)
    {
        OrderName = dropdown.options[value].text;
    }
}
