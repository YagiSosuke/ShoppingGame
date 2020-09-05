using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

/*あなたの名前を表示するスクリプト*/

public class ViewYourName : MonoBehaviour
{
    [SerializeField] Text IDText;       //あなたの名前を表示するテキスト

    // Start is called before the first frame update
    void Start()
    {
        //名前を設定
        //QueryTestを検索するクラスを作成
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //Scoreの値が7と一致するオブジェクト検索
        query.WhereEqualTo("ID", PlayerPrefs.GetString("IDCreateYet"));
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null)
            {
                //検索失敗時の処理
                Debug.Log("検索に失敗しました");
            }
            else {
                //名前を表示する
                foreach (NCMBObject obj in objList)
                {
                    IDText.text = (string)obj["Name"];
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
