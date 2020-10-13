using System;
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

    [SerializeField] GameObject ErrorPanel;     //エラー時に表示するウィンドウ
    [SerializeField] Text ErrorText;            //エラー時に表示するテキスト


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
        string myString = "";       //ファイル内容をまとめて格納

        //入力された内容をstring型配列にコピー
        for (int i = 0; i < ListScript.ListLen; i++)
        {
            //項目内容が""でなければ
            if (ListScript.ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text != "")
            {
                myString += (ListScript.ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text + "\n");
            }
        }


        //リストの項目があっても、中身が空なら保存できない
        if (ListScript.ListLen > 0 && myString == "")
        {
            ErrorPanel.SetActive(true);
            ErrorText.text = "依頼する商品を入力してください";
        }
        //項目数がある場合のみ送信可能にする
        else if (ListScript.ListLen > 0) {
            SendPanel.SetActive(true);
            SendPanelText.text = "以上の内容を\n" + OrderName + "\nに送信します";
        }
        //項目数がない場合、送信できなくする
        else
        {
            ErrorPanel.SetActive(true);
            ErrorText.text = "リストの項目は\n最低でも1つ入れてください";
        }
    }

    //送信確認画面を見えなくするボタン
    public void BackButton()
    {
        SendPanel.SetActive(false);
    }

    //依頼を完了するボタン
    public void SendButton()
    {
        //依頼先を名前 -> IDに変える必要がある
        //Nameが指定されたもののIDを検索する

        //UserIDsを検索するクラスを作成
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //Nameの値が入力されたものと一致するオブジェクト検索
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

                        //項目が空でない物のみアップロード
                        if (List != "")
                        {
                            //サーバ - データストアに値をアップロード
                            NCMBObject OrderClass = new NCMBObject("_" + (string)obj["ID"]);      //サーバ - 依頼先のクラスを作成
                            OrderClass["message"] = List;       //値を設定
                            OrderClass["SendID"] = PlayerPrefs.GetString("IDCreateYet", "null");       //値を設定
                            OrderClass["OrderDate"] = (DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
                            OrderClass.SaveAsync();             // データストアへの登録
                        }
                    }
                    CorrectPanel.SetActive(true);
                }
            }
        });        
    }

    //依頼をするボタン
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
