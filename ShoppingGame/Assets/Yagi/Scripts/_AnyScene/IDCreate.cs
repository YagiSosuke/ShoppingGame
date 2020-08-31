using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

/*初回起動時、IDを自動生成してくれるスクリプト*/

public class IDCreate : MonoBehaviour
{
    int ID_int;         //int型のID
    string ID_str = "";      //string型のID

    [SerializeField] Text ID_text;      //自分のIDヲ表示するテキスト

    [SerializeField] bool IDResetF;

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
        }
        
        //IDを表示する
        ID_text.text = "ID:" + ID_str;






        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IDを作成する関数
    void CreateID()
    {
        bool IDCreateF = false;     //IDが被っているかどうかのフラグ

        do
        {
            Debug.Log("実行されました");
            ID_int = (int)Random.Range(0, 10);       //0～999の乱数を生成し、IDにする
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

            IDCreateF = false;
            Debug.Log("ここまでできた");
            //取得テスト
            //UserIDsを検索するクラスを作成
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
            Debug.Log("ここまでできた2");
            //IDの値がID_strと一致するオブジェクトを検索
            query.WhereEqualTo("ID", ID_str);
            Debug.Log("ここまでできた3");
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                Debug.Log("ここまでできた4");
                if (e != null)
                {
                    //検索失敗時
                    Debug.Log("エラーです");
                }
                else
                {
                    //検索成功時
                    foreach (NCMBObject obj in objList)
                    {
                        Debug.Log("このデータは存在します");
                        IDCreateF = true;
                        Debug.Log("IDF = " + IDCreateF);
                    }
                }
            });

            Debug.Log("IDF2 = " + IDCreateF);
        } while (IDCreateF);


        //クラスを作成
        NCMBObject objClass = new NCMBObject("UserIDs");
        // オブジェクトに値を設定
        objClass["ID"] = ID_str;
        objClass["Name"] = "name";
        // データストアへの登録
        objClass.SaveAsync();
    }
}
