using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NCMB;

/*依頼画面で依頼先を選択するときのドロップダウンメニュー*/
/*自動で依頼先の候補が表示される*/

public class OrderDropDownMenu : MonoBehaviour
{
    string[] FamilyID;          //家族のID
    string FamilyFilePath;      //家族情報のファイルパス
    List<string> FamilyName = new List<string>();    //家族名

    int FamilyNum;              //家族の人数
    int Cnt;                    //家族の人数をカウントしていく変数

    Dropdown dropdown;      //ドロップダウンメニュー
    string NoAdressText = "送信先がありません";      //送信先が登録されていないときのスクリプト

    [SerializeField] OrderCorrect ordercorrect;      //依頼を完了するスクリプト

    // Start is called before the first frame update
    void Start()
    {
        FamilyFilePath = Application.dataPath + @"\Family\FamilyData.txt";
        FamilyID = File.ReadAllLines(FamilyFilePath);      //家族のIDを格納
        Cnt = 0;            //カウントの初期値を0にしておく
        FamilyNum = FamilyID.Length;            //家族の人数を記録

        dropdown = GetComponent<Dropdown>();        //ドロップダウンメニューを習得
        dropdown.ClearOptions();        //現在の要素をクリアする

        //家族名を取得したい
        //家族の人数分検索を繰り返す
        if (FamilyNum > 0)
        {
            //送信先の候補がある場合
            AddFamilyMenu();
        }
        else
        {
            //送信先の候補がない場合
            dropdown.options.Add(new Dropdown.OptionData(NoAdressText));  //新しく要素のリストを設定する
            dropdown.value = 0;         //デフォルトを設定(0～n-1)
            ordercorrect.GetValue(0);         //依頼先のデフォルト値を設定  
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //メニューに家族を追加するスクリプト
    public void AddFamilyMenu()
    {
        //QueryTestを検索するクラスを作成
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //IDの値が登録されたものと一致するオブジェクト検索
        query.WhereEqualTo("ID", FamilyID[Cnt]);
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
                    //家族の名前を記憶する
                    FamilyName.Add((string)obj["Name"]);
                                        
                    Cnt++;
                    if(Cnt < FamilyNum)
                    {
                        AddFamilyMenu();
                    }
                    else
                    {
                        //ドロップダウンメニューに項目の追加
                        if (dropdown)
                        {
                            dropdown.AddOptions(FamilyName);  //新しく要素のリストを設定する
                            dropdown.value = 0;               //デフォルトを設定(0～n-1)
                            ordercorrect.GetValue(0);         //依頼先のデフォルト値を設定  
                        }
                    }
                }
            }
        });
    }
}
