using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

/*IDを検索するスクリプト*/

public class SerchID : MonoBehaviour
{
    [SerializeField] InputField FamilyID;       //検索したいIDを入力するInputField
    [SerializeField] GameObject SerchCheckPanel;//検索結果確認のパネル
    [SerializeField] Text FamilyNameText;       //家族の名前を表示するテキスト     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //検索するボタンをクリックしたとき
    public void SerchButton()
    {
        //UserIDsのクラスを検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //検索条件を指定
        query.WhereEqualTo("ID", FamilyID.text);

        //入力したIDのアカウントが存在するか調べる
        query.CountAsync((int count, NCMBException e) => {
            if (e != null)
            {
                //件数取得失敗時の処理
            }
            else {
                Debug.Log("count : " + count);
                //アカウントが存在した場合
                if(count > 0)
                {
                    GetAccount();
                }
                //アカウントが存在しない場合
                else
                {
                    Debug.Log("アカウントが存在しません");
                }
            }
        });

    }

    //指定されたIDのアカウントを取得してくる関数
    public void GetAccount()
    {
        //UserIDsのクラスを検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //検索条件を指定
        query.WhereEqualTo("ID", FamilyID.text);

        //IDの値が入力したものと一致するオブジェクト検索
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
            }
            else {
                //IDが入力したものの名前を出力 + パネル表示
                foreach (NCMBObject obj in objList)
                {
                    SerchCheckPanel.SetActive(true);
                    FamilyNameText.text = (string)obj["Name"];
                    break;
                }
            }
        });
    }
}
