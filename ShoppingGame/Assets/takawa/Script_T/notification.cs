using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;

//内容：通知用のオブジェクトを生成するクラス

//バグ内容
//①Notification_completedのcompleted_idに何度も通知用オブジェクトに使ったオブジェクトidが記録される
//②依頼主の名前が取得できない

//未実装
//①通知しないといけない内容が二つ以上ある時、順番にゆっくりと表示させること
//処理がupdateでやらないといけないので、コルーチン以外でやる必要がある
public class notification : MonoBehaviour
{
    string myID;//自分のアカウントのID
    string requestername;//依頼者のアカウントの名前
    private long now_datetime;//現在の日付
    private string createdate_tmp;//コピー用のリストが生成された日の日付
    private long createdate;//リストが生成された日の日付
    char[] del = { ' ', '/', ':' };//余計な文字を消す
    [SerializeField] private GameObject notification_obj;//通知のオブジェクト
    private string notification_text;//通知オブジェクトのテキスト
    private List<string> verification = new List<string>();//もう表示されたかどうかを確認するリスト
    private List<string> completed_id = new List<string>();//もう表示されたかどうかを確認するid
    bool text_once = true;//一回だけ通知を表示したかどうか
    bool Display_once = true;//一回だけ表示したかどうか
    bool writing = true;//データベースに書き込めるかどうか
    float interval_time=0;//連続して通知オブジェクトの生成時の間隔
    int loop_count = 0;//連続して通知オブジェクトの生成時のループ回数

    // Start is called before the first frame update
    void Start()
    {
        //IEnumerator coroutine = InstanceCoroutine();//通知を一定の間隔で表示させるための関数
        //StartCoroutine(coroutine);//一定の間隔で通知オブジェクトを生成
        //自分のアカウントのIDを取得
        myID = PlayerPrefs.GetString("IDCreateYet");
        Debug.Log(myID);

        // 現在の日付を取得
        now_datetime = long.Parse(System.DateTime.Now.Year.ToString("0000")+ System.DateTime.Now.Month.ToString("00")+ System.DateTime.Now.Day.ToString("00")+ System.DateTime.Now.Hour.ToString("00")+ System.DateTime.Now.Minute.ToString("00")+ System.DateTime.Now.Second.ToString("00"));
        Debug.Log(now_datetime);

        //通知オブジェクトのテキストの取得
        notification_text = notification_obj.transform.GetChild(0).GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        //自分に依頼している内容を検索するクラスを作成
        NCMBQuery<NCMBObject> my_account = new NCMBQuery<NCMBObject>("_" + myID);//自分のクラスを探す
        //自分に依頼している内容のものを全て取得
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
                    //確認
                    Debug.Log("message:" + obj["message"] + "\nSendID:" + obj["SendID"]+"\nCreateDate" + obj.CreateDate+ "\nOrderDate:" + obj["OrderDate"]);
                    
                    //依頼した時間を取得
                    createdate = long.Parse(obj["OrderDate"].ToString());
                    Debug.Log("createdate" + createdate);

                    //最近依頼されたかどうかを確認
                    if (createdate >= now_datetime)
                    {
                        //もう通知したものであれば、二度と通知しないようにする
                        if (verification != null)
                        {
                            foreach (string Existing_text in verification)
                            {
                                if (Existing_text == obj["SendID"].ToString())
                                    //二度と通知しない
                                    text_once = false;
                            }
                        }
                        
                        //まだ通知してないので、通知するために必要なデータを記録する
                        if (text_once == true)
                        {
                            Debug.Log("新しいリストが来た");
                            //依頼主のIDを記録する
                            verification.Add(obj["SendID"].ToString());
                            //今記録したオブジェクトIDを記録し、もう一度通知させないようにする
                            completed_id.Add(obj.ObjectId);
                            Debug.Log(completed_id);
                            //通知用のオブジェクトを画面に出力するようにする
                            Display_once = true;

                            //確認用
                            foreach(string d in completed_id)
                            {
                                Debug.Log("保存されているリスト"+d);
                            }
                        }
                        //リセット
                        text_once = true;
                    }
                }
                //通知できる状態だったら、通知するようにする
                if (Display_once == true)
                {
                    Display();
                }
            }
        });
    }


    //通知用オブジェクトに通知内容を記録させ、画面に出力するようにする
    void Display()
    {
        if (verification != null)
        {
            foreach (string Existing_text in verification)
            {
                //依頼者のアカウントの名前を取得するため、UserIDsを検索するクラスを作成
                NCMBQuery<NCMBObject> ID = new NCMBQuery<NCMBObject>("UserIDs");
                //IDの欄からExisting_text(探すIDの値)と一致するオブジェクト検索
                ID.WhereEqualTo("ID", Existing_text);
                ID.FindAsync((List<NCMBObject> objList, NCMBException e) =>
                {
                    if (e != null)
                    {
                        //検索失敗時の処理
                        Debug.Log("ユーザーネーム取得に失敗");
                    }
                    else
                    {
                        //検索したいユーザーネームを取得
                        foreach (NCMBObject name in objList)
                        {

                            requestername = name["Name"].ToString();
                            Debug.Log("nameから取得:" + name["Name"].ToString());

                            //取得した依頼主の名前をnotification_textに渡す
                            notification_text = requestername;

                            //通知用のオブジェクトのテキストに表示する文字の内容を与える
                            notification_obj.gameObject.transform.GetChild(0).GetComponent<Text>().text = notification_text + "さんから依頼が届きました";
                            ////通知用のオブジェクトを表示させる
                            //Instantiate(notification_obj, new Vector3(540, 80.55005f, 0), Quaternion.identity, this.transform);
                            //Debug.Log("通知オブジェクトを生成した");
                            //もし、連続で通知オブジェクトを生成するなら、2秒ごとに生成されるようになる
                            interval_time = 2.0f * loop_count;
                            Debug.Log("通知を出す");
                            Invoke("notifi_Instance", interval_time);//notifi_Instance関数で通知オブジェクトを生成
                            loop_count++;
                        }
                        
                    }
                    
                });
            }
            //連続する通知オブジェクト生成の間隔をリセット
            loop_count = 0;
            interval_time = 0;

            //Notification_completedクラスの内容を取得
            NCMBObject obj = new NCMBObject("Notification_completed");
            //通知用オブジェクトのために使ったオブジェクトIDを全て取得
            foreach (string id in completed_id)
            {
                //すでに通知を記録したものかどうかを調べる
                //Notification_completedを検索するクラスを作成
                NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Notification_completed");
                //クラスの中のcompleted_idを全て取得する
                query.WhereNotEqualTo("completed_id", "0");
                query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
                {
                    if (e != null)
                    {
                        //検索失敗時の処理
                        Debug.Log("エラー");
                    }
                    else
                    {
                        //確認用
                        foreach (NCMBObject quaryobj in objList)
                        {
                            Debug.Log("quaryobj[completed_id]" + quaryobj["completed_id"].ToString());
                        }



                        foreach (NCMBObject quaryobj in objList)
                        {
                            //もし、Notification_completedクラスのcompleted_idの中に
                            //まだ通知用オブジェクトに使ったオブジェクトidが書き込まれていなかったら、書き込みを始める
                            if (quaryobj["completed_id"].ToString() == id)
                            {
                                Debug.Log("書き込みを開始");
                                writing = false;
                            }
                            else
                            {
                                Debug.Log("違う");
                            }

                        }


                        //通知用オブジェクトに使ったオブジェクトidをcompleted_idに記録する
                        if (writing == true)
                        {
                            obj["completed_id"] = id;
                            obj.SaveAsync((NCMBException e2) =>
                            {
                                if (e2 != null)
                                {
                                    //エラー処理
                                    Debug.Log("失敗");
                                }
                                else
                                {
                                    //成功時の処理
                                    Debug.Log("保存した");
                                }
                            });

                        }
                        //リセット
                        writing = true;
                    }
                });

                //二回以上表示されることを防止
                Display_once = false;
            }
        }
    }

    void notifi_Instance()//通知オブジェクトの生成
    {
        //通知用のオブジェクトを表示させる
        Instantiate(notification_obj, new Vector3(540, 80.55005f, 0), Quaternion.identity, this.transform);
        Debug.Log("通知オブジェクトを生成した");
    }
}
