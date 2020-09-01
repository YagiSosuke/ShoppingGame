using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

/*初回起動時、IDを自動生成してくれるスクリプト*/
/*タイトル画面につけたい*/

public class IDCreate : MonoBehaviour
{
    int ID_int;         //int型のID
    string ID_str = "";      //string型のID

    [SerializeField]
    Text ID_text;      //自分のIDヲ表示するテキスト

    [SerializeField]
    bool IDResetF;

    // Start is called before the first frame update
    void Start()
    {
        //IDをリセットしたいとき（主にデバッグ時に使用）
        if (IDResetF)
        {
            //ID生成済みという情報を削除する
            PlayerPrefs.DeleteKey("IDCreateYet");

        }


        //IDをまだ作ってないときにIDを作る
        if (!PlayerPrefs.HasKey("IDCreateYet"))
        {
            //IDを生成
            CreateID();

            PlayerPrefs.SetString("IDCreateYet", ID_str);   //IDを作ったことを端末に記憶させる
        }
        else
        {
            //IDが既に存在する場合、ID_strに渡しておく
            ID_str = PlayerPrefs.GetString("IDCreateYet", "null");
            //IDを表示する
            ID_text.text = "ID:" + ID_str;
        }






        Debug.Log("Start終了");

    }

    // Update is called once per frame
    void Update()
    {

    }

    //IDを作成する関数
    void CreateID()
    {
        ID_int = (int)Random.Range(0, 999);       //0～999の乱数を生成し、IDにする
                                                 //IDをstring型に直す
        if (ID_int < 100)
        {
            ID_str += "0";
            if (ID_int < 10)
            {
                ID_str += "0";
            }
        }
        ID_str += ID_int.ToString();
        

        //取得テスト
        //UserIDsを検索するクラスを作成
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //IDの値がID_strと一致するオブジェクトを検索
        query.WhereEqualTo("ID", ID_str);
        query.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時
                Debug.Log("エラーです");
            }
            else
            {
                //被っていなければ
                if (count == 0)
                {
                    //IDを表示する
                    ID_text.text = "ID:" + ID_str;


                    //クラスを作成
                    NCMBObject objClass = new NCMBObject("UserIDs");
                    // オブジェクトに値を設定
                    objClass["ID"] = ID_str;
                    objClass["Name"] = "name";
                    // データストアへの登録
                    objClass.SaveAsync();
                }
                //被っていたら
                else
                {
                    ID_str = "";
                    CreateID();
                }
            }
        });
        
        Debug.Log("終了");
    }
}
