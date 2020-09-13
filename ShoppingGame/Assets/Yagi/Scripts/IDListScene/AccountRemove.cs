using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NCMB;

/*登録アカウントを解除するボタン*/

public class AccountRemove : MonoBehaviour
{
    GameObject ScrollArea;      //アカウント情報を格納する領域

    [SerializeField] GameObject Container;      //アカウント情報
    [SerializeField] Text UserName;             //ユーザーネーム
    string UserID;          //ユーザーID

    string fileID;          //Fileから読み込んだIDを記録する場所
    string FilePath;        //家族情報のファイルパス

    // Start is called before the first frame update
    void Start()
    {
        ScrollArea = GameObject.Find("ScrollArea");
        #if UNITY_EDITOR        //デバッグ時
            FilePath = Application.dataPath + @"\Family\FamilyData.txt";
        #elif UNITY_ANDROID     //リリース時
            FilePath = Application.persistentDataPath + @"\Family\FamilyData.txt";
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //アカウント登録解除するボタン
    public void RemoveButton()
    {
        RectTransform ScrollRT = ScrollArea.GetComponent(typeof(RectTransform)) as RectTransform;       //RectTransform

        //スクロールエリアを縮小する
        ScrollRT.sizeDelta = new Vector2(1000, ScrollRT.sizeDelta.y - 200);


        //ファイルからアカウント情報を削除する
        //ユーザーのIDを知る
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //NameがUserNameと一致するオブジェクト検索
        query.WhereEqualTo("Name", UserName.text);
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
                    
                    fileID = File.ReadAllText(FilePath).Replace(UserID, "").Replace("\n", "");

                    Debug.Log("fileID = " + fileID);
                    
                    int beginIndex = fileID.Length;     //文字列の開始インデックス
                    //改行を挿入する
                    do
                    {
                        fileID = fileID.Insert(beginIndex, "\n");
                        beginIndex -= 5;
                    } while (beginIndex > 0);
                    
                    //無駄な改行は削除する

                    //File.Delete(FilePath);
                    //File.Delete(Application.dataPath + @"\Family\FamilyData.txt.meta");
                    //File.AppendAllText(FilePath, fileID);

                    Debug.Log("fileID2 = " + fileID);
                }
            }
        });


        //アカウント情報を削除する
        Destroy(Container.gameObject);
    }
}
