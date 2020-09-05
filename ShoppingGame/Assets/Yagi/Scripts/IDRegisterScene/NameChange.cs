using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

/*名前を変更するスクリプト*/

public class NameChange : MonoBehaviour
{
    [SerializeField] GameObject NameChangePanel;        //名前変更パネル
    [SerializeField] InputField NameInput;              //新しい名前の入力領域    
    [SerializeField] Text NamePrev;                     //名前のプレビュー

    //名前変更ボタンを押したとき
    public void NameChangeButton()
    {
        //名前変更パネルを表示
        NameChangePanel.SetActive(true);
    }

    //名前変更確定ボタン
    public void CorrectButton()
    {
        //UserIDsを検索するクラスを作成
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //IDの値がこのデータと一致するオブジェクト検索
        query.WhereEqualTo("ID", PlayerPrefs.GetString("IDCreateYet"));
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null)
            {
                //検索失敗時の処理
                Debug.Log("検索失敗です");
            }
            else {
                //ユーザ名の変更をする
                foreach (NCMBObject obj in objList)
                {
                    //サーバ - 名前を変更
                    NameChangeMethod(obj);
                   
                    //ゲーム - プレビューの表示を変更
                    NamePrev.text = NameInput.text;

                    //入力領域を空に
                    NameInput.text = "";

                    //名前変更パネルを非表示に
                    NameChangePanel.SetActive(false);
                }
            }
        });

    }

    //戻るボタン
    public void BackButton()
    {
        //名前変更パネルを非表示
        NameChangePanel.SetActive(false);
    }


    //名前変更関数（変数が被る関係で別関数を使用）
    public void NameChangeMethod(NCMBObject obj)
    {
        obj["Name"] = NameInput.text;

        obj.SaveAsync((NCMBException e) => {
            if (e != null)
            {
                //エラー処理
                Debug.Log("保存に失敗しました");
            }
            else {
                //成功時の処理
            }
        });
    }
}
