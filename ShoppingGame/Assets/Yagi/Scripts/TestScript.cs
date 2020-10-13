using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        NCMBObject obj = new NCMBObject("TestYagi");
        obj.Add("test", "value");
        obj.SaveAsync((NCMBException e) => {
            if (e != null)
            {
                //エラー処理
            }
            else {
                //成功時の処理
            }
        });
        */

        //データベースを検索する変数(クエリ)
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("_604");
        //IDの値が正しいオブジェクト検索
        query.WhereNotEqualTo("SendID", "-1");
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null)
            {
                //検索失敗時の処理
                Debug.Log("検索に失敗しました");
            }
            else {
                //IDがありえる数値のオブジェクトを出力
                foreach (NCMBObject obj in objList)
                {
                    Debug.Log("message:" + obj["message"] + "\nSendID:" + obj.CreateDate);
                }
            }
        });



    }

    // Update is called once per frame
    void Update()
    {

    }
}
