using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NCMB;

/*登録アカウント一覧を表示*/

public class LoadFamilyList : MonoBehaviour
{
    [SerializeField] GameObject ScrollArea;         //アカウントを表示する領域
    RectTransform ScrollRT;         //スクロール領域のRectTransform

    [SerializeField] GameObject AccountPrefab;      //表示アカウントのプレファブ
    List<GameObject> AccountInstance = new List<GameObject>();                     //表示アカウントのインスタンス
    GameObject ins;

    string filePath;     //ファイルパス

    List<string> AccountID = new List<string>();       //アカウントID
    int length = 0;               //登録アカウント数
    int count = 0;                //アカウントをカウントする

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
            filePath = Application.dataPath + @"\Family\FamilyData.txt";     //ファイルパス
        #elif UNITY_ANDROID
            filePath = Application.persistentDataPath + @"\Family\FamilyData.txt";     //ファイルパス
        #endif

        ScrollRT = ScrollArea.GetComponent(typeof(RectTransform)) as RectTransform;

        
        string[] allText = File.ReadAllLines(filePath);         //登録アカウントデータ

        foreach (var text in allText)
        {
            ins = Instantiate(AccountPrefab, transform.position, Quaternion.identity, ScrollArea.transform);
            AccountInstance.Add(ins);
            AccountID.Add(text);
            length++;
        }

        InstanceAccount();

        //スクロールエリアのサイズをコンテンツ数に合わせて拡張する
        ScrollRT.sizeDelta = new Vector2(1000, 200 * length);

        //位置を上に合わせる
        float height = ScrollRT.sizeDelta.y;
        this.transform.localPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //アカウント情報を新たに生成する関数
    public void InstanceAccount()
    {
        //for (int i = 0; i < length; i++)
        //{
            //UserIDsを検索するクラスを作成
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
            //IDの値が指定されたものと一致するオブジェクト検索
            query.WhereEqualTo("ID", AccountID[count]);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    //検索失敗時の処理
                    Debug.Log("検索失敗です");
                }
                else {
                    //IDが指定されたもののオブジェクトを出力
                    foreach (NCMBObject obj in objList)
                    {
                        AccountInstance[count].transform.GetChild(1).GetComponent<Text>().text = (string)obj["Name"];
                        
                        count++;
                        if (count < length)
                        {
                            InstanceAccount();
                        }
                    }
                }
            });
        //}
    }
}
