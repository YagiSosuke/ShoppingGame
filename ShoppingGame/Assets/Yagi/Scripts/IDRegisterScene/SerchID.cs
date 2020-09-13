using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.IO;

/*IDを検索するスクリプト*/

public class SerchID : MonoBehaviour
{
    [SerializeField] InputField FamilyID;       //検索したいIDを入力するInputField
    [SerializeField] GameObject SerchCheckPanel;//検索結果確認のパネル
    [SerializeField] GameObject UncorrectPanel; //検索失敗のパネル
    [SerializeField] Text UncorrectText;        //検索失敗テキスト

    [SerializeField] Text FamilyNameText;       //家族の名前を表示するテキスト     
    [SerializeField] Text FamilyNameText2;       //家族の名前を表示するテキスト(登録完了パネル用)     

    string FilePath;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR        //デバッグ時
            FilePath = Application.dataPath + @"\Family\FamilyData.txt";
        #elif UNITY_STANDALONE  //リリース時
            FilePath = Application.persistentDataPath + @"\Family\FamilyData.txt";
        #endif
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
                    //登録されている家族のIDを読み込む
                    string[] FileText = File.ReadAllLines(FilePath);
                    //IDが既に登録されているかを見るフラグ
                    bool IDRegisterYet = false;

                    //IDがすでに登録されているか検索
                    for(int i=0;i<FileText.Length; i++)
                    {
                        Debug.Log("ファイル["+i+"] = "+FileText[i]);
                        if (FileText[i] == FamilyID.text) {
                            IDRegisterYet = true;
                            break;
                        }
                    }


                    //自分のIDが入力されたときの処理
                    if (FamilyID.text == PlayerPrefs.GetString("IDCreateYet", "null"))
                    {
                        UncorrectPanel.SetActive(true);
                        UncorrectText.text = "自分のIDを\n登録することはできません";
                    }

                    //IDが既に登録されている場合の処理
                    else if (IDRegisterYet)
                    {
                        UncorrectPanel.SetActive(true);
                        UncorrectText.text = "入力されたIDは\n既に登録されています";
                    }

                    else {
                        //指定されたIDのアカウントを取得
                        GetAccount();
                    }
                }
                //アカウントが存在しない場合
                else
                {
                    //エラーパネルを表示
                    UncorrectPanel.SetActive(true);
                    UncorrectText.text = "入力されたIDは存在しません\nもう一度お試しください";
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
                    FamilyNameText2.text = (string)obj["Name"];
                    break;
                }
            }
        });
    }
}
