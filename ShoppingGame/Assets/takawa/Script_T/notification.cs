using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;


public class notification : MonoBehaviour
{
    string myID;//自分のアカウントのID
    private long now_datetime;//現在の日付
    private string createdate_tmp;//コピー用の日付
    private long createdate;//リストが生成された日の日付
    char[] del = { ' ', '/', ':' };//余計な文字を消す
    [SerializeField] private GameObject notification_obj;//通知のオブジェクト
    private string notification_text;//通知オブジェクトのテキスト
    NCMBObject my_account2;
    // Start is called before the first frame update
    void Start()
    {
        //自分のアカウントのIDを取得
        myID = PlayerPrefs.GetString("IDCreateYet");
        Debug.Log(myID);
        // 現在の日付を取得
        now_datetime = long.Parse(System.DateTime.Now.Year.ToString("0000")+ System.DateTime.Now.Month.ToString("00")+ System.DateTime.Now.Day.ToString("00")+ System.DateTime.Now.Hour.ToString("00")+ System.DateTime.Now.Minute.ToString("00")+ System.DateTime.Now.Second.ToString("00"));
        Debug.Log(now_datetime);
        notification_text = notification_obj.transform.GetChild(0).GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        //①サーバーが更新されたかどうかを確認する
        //QueryTestを検索するクラスを作成
        NCMBQuery<NCMBObject> my_account = new NCMBQuery<NCMBObject>("_" + myID);//自分のクラスを探す
        //基準の時間よりも後に新しくデータが作られていたら  
        //IDの値が正しいオブジェクト検索
        //データベースを検索する変数(クエリ)
        //NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("_604");
        my_account.WhereNotEqualTo("SendID", "-1");
        my_account.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null)
            {
                //検索失敗時の処理
                Debug.Log("検索に失敗しました");
            }
            else
            {
                //IDがありえる数値のオブジェクトを出力
                foreach (NCMBObject obj in objList)
                {
                    Debug.Log("message:" + obj["message"] + "\nSendID:" + obj["SendID"]+"\nCreateDate" + obj.CreateDate+ "\nOrderDate:" + obj["OrderDate"]);
                    //createdate_tmp = obj.CreateDate.ToString();
                    ////削除する文字を1文字ずつ削除する
                    //foreach (char c in del)
                    //{
                    //    createdate_tmp = createdate_tmp.Replace(c.ToString(), "");
                    //}
                    createdate = long.Parse(obj["OrderDate"].ToString());
                    Debug.Log("createdate" + createdate);
                    if (createdate >= now_datetime)
                    {
                        Debug.Log("新しいリストが来た");
                        notification_text = obj["SendID"].ToString();
                        Debug.Log(notification_text);
                        Instantiate(notification_obj, new Vector3(0, 0, 0), Quaternion.identity);
                    }
                }
            }
        });

        //③自分向けに依頼が来ていたら、○○さんから依頼が来ましたと通知させる
    }
}

//using UnityEngine;
//using System.Collections;

///////////////////////////ここから追加コード////////////////////////
////SDKの利用を宣言
//using NCMB;
///////////////////////////ここまで追加コード////////////////////////

//using System.Collections.Generic;

//public class notification : MonoBehaviour
//{

//    /////////////////////////ここから追加コード////////////////////////
//    private static bool _isInitialized = false;

//    /// <summary>
//    ///イベントリスナーの登録
//    /// </summary>
//    void OnEnable()
//    {
//        NCMBManager.onRegistration += OnRegistration;
//        NCMBManager.onNotificationReceived += OnNotificationReceived;
//    }

//    /// <summary>
//    ///イベントリスナーの削除
//    /// </summary>
//    void OnDisable()
//    {
//        NCMBManager.onRegistration -= OnRegistration;
//        NCMBManager.onNotificationReceived -= OnNotificationReceived;
//    }

//    /// <summary>
//    ///端末登録後のイベント
//    /// </summary>
//    void OnRegistration(string errorMessage)
//    {
//        if (errorMessage == null)
//        {
//            Debug.Log("OnRegistrationSucceeded");
//        }
//        else
//        {
//            Debug.Log("OnRegistrationFailed:" + errorMessage);
//        }
//    }

//    /// <summary>
//    ///メッセージ受信後のイベント
//    /// </summary>
//    void OnNotificationReceived(NCMBPushPayload payload)
//    {
//        Debug.Log("OnNotificationReceived");
//    }

//    /// <summary>
//    ///シーンを跨いでGameObjectを利用する設定
//    /// </summary>
//    public virtual void Awake()
//    {
//        if (!_isInitialized)
//        {
//            _isInitialized = true;
//            DontDestroyOnLoad(this.gameObject);
//        }
//        else
//        {
//            Destroy(this.gameObject);
//        }
//    }


//    /////////////////////////ここまで追加コード////////////////////////

//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//}
