using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NCMB;

/*検索結果確認パネルでのスクリプト、主にボタン系*/

public class SerchCheckPanelScript : MonoBehaviour
{
    string FilePath;        //家族のIDの保存場所
    [SerializeField] GameObject SerchCheckPanel;        //検索結果確認パネル

    [SerializeField] GameObject CorrectPanel;           //登録完了パネル
    [SerializeField] GameObject ErrorPanel;             //エラーパネル

    [SerializeField] Text ErrorText;                    //エラーテキスト

    [SerializeField] InputField InputFamilyID;          //家族のIDを書き込む場所
    [SerializeField] InputField InputFamilyName;          //家族の名前を書き込む場所

    // Start is called before the first frame update
    void Start()
    {
        FilePath = Application.dataPath + @"\Family\FamilyData.txt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //検索ID確認 - はいを押した時
    public void YesButton()
    {
        //入力されたユーザ名が正しいかチェックする
        //UserIDsを検索するクラスを作成
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserIDs");
        //IDの値が指定されたものと一致するオブジェクト検索
        query.WhereEqualTo("ID", InputFamilyID.text);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
            if (e != null)
            {
                //検索失敗時の処理
                Debug.Log("検索失敗です");
            }
            else
            {
                Debug.Log("検索できました");

                //IDが指定されたものの、名前が正しいか調べる
                foreach (NCMBObject obj in objList)
                {
                    //名前が入力されたものと一致するとき
                    if ((string)obj["Name"] == InputFamilyName.text)
                    {
                        Debug.Log("名前一致");
                        //テキストを空にする
                        InputFamilyName.text = "";
                        //ファイルに家族のIDを保存
                        File.AppendAllText(FilePath, InputFamilyID.text + "\n");
                        SerchCheckPanel.SetActive(false);       //登録確認パネルを消す
                        CorrectPanel.SetActive(true);           //登録完了パネルを表示
                    }
                    //名前が入力されたものと一致しないとき
                    else
                    {
                        Debug.Log("名前不一致");
                        //テキストを空にする
                        InputFamilyName.text = "";
                        SerchCheckPanel.SetActive(false);       //登録確認パネルを消す
                        //名前が違う事を示すパネルを表示
                        ErrorPanel.SetActive(true);
                        ErrorText.text = "入力さえれた名前が違います\nもう一度お試しください";
                    }
                }
            }
        });
    }

    //検索ID確認 - いいえを押した時
    public void NoButton()
    {
        SerchCheckPanel.SetActive(false);       //パネルを閉じる
    }
}
