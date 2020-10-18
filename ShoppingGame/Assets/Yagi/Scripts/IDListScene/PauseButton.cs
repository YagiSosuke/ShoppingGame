using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NCMB;

/*ポーズ画面展開*/

public class PauseButton : MonoBehaviour
{
    GameObject ScrollArea;      //アカウント情報を格納する領域

    //ポーズ時に展開されるパネル
    [SerializeField] GameObject PausePanel;
    //名前のテキスト
    [SerializeField] Text NameText;


    string UserID;          //ユーザーID
    string fileID;          //Fileから読み込んだIDを記録する場所
    string FilePath;        //家族情報のファイルパス

    GameObject Container;   //削除するアカウントのコンテナ

    void Start()
    {
        #if UNITY_EDITOR        //デバッグ時
            FilePath = Application.dataPath + @"\Family\FamilyData.txt";
        #elif UNITY_ANDROID     //リリース時
            FilePath = Application.persistentDataPath + @"\Family\FamilyData.txt";
        #endif

        
        ScrollArea = GameObject.Find("ScrollArea");
    }

    //ポーズウィンドウを開く
    public void PauseWindowOpen()
    {
        PausePanel.SetActive(true);
    }


    //ポーズウィンドウを閉じる
    public void PauseWindowClose()
    {
        PausePanel.SetActive(false);
    }

    //ポーズウィンドウの削除アカウント名を設定する
    public void DataSet(string name, GameObject container)
    {
        NameText.text = name;
        Container = container;
    }


    //フレンド解除するボタン(決定)
    public void RemoveButton()
    {
        RectTransform ScrollRT = ScrollArea.GetComponent(typeof(RectTransform)) as RectTransform;       //RectTransform

        //スクロールエリアを縮小する
        ScrollRT.sizeDelta = new Vector2(1000, ScrollRT.sizeDelta.y - 200);


        //ファイルからアカウント情報を削除する
        //ユーザーのIDを知る
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //NameがUserNameと一致するオブジェクト検索
        query.WhereEqualTo("Name", NameText.text);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null)
            {
                //検索失敗時の処理
                Debug.Log("検索失敗です");
            }
            else {
                //NameがUserNameのオブジェクトのIDを入手
                foreach (NCMBObject obj in objList)
                {
                    //IDを記録
                    UserID = (string)obj["ID"];

                    string[] FriendIDs = File.ReadAllLines(FilePath);
                    fileID = "";

                    for (int i = 0; i < FriendIDs.Length; i++)
                    {
                        if (!FriendIDs[i].Equals(UserID))
                        {
                            fileID += FriendIDs[i] + "\n";
                        }
                    }

                    File.Delete(FilePath);
                    File.Delete(Application.dataPath + @"\Family\FamilyData.txt.meta");
                    File.AppendAllText(FilePath, fileID);

                    Debug.Log("fileID = " + fileID);


                }
            }
        });
        //アカウント情報を削除する
        Destroy(Container.gameObject);
    }
}
