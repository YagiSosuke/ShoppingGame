using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleButtonScript : MonoBehaviour
{
    public SceneButton sceneButton;
 
    //enumの中の項目をインスペクターから選べるようになってます。
    //ボタンを増減させたい場合はenumとswitch文いじってください
    //enumの中いじくった場合はインスペクターから全部のsceneButton設定しなおしてください。狂ってる場合あります。
    public enum SceneButton
    {
        Start,
        CreateList,
        Order,
        Rank,
        IDRegister
    };

    public void TappedButton()
    {
        switch (sceneButton)
        {
            case SceneButton.Start:
                SceneManager.LoadScene("Selection_List");
                Debug.Log("ショッピングボタンが押されました");
                break;
            case SceneButton.CreateList:
                SceneManager.LoadScene("CreateListScene");
                Debug.Log("リスト作成ボタンが押されました");
                break;
            case SceneButton.Order:
                SceneManager.LoadScene("OrderScene");
                Debug.Log("依頼ボタンが押されました");
                break;
            case SceneButton.Rank:
                //SceneManager.LoadScene("");
                Debug.Log("ランキングボタンが押されました");
                break;
            case SceneButton.IDRegister:
                SceneManager.LoadScene("IDRegisterScene");
                Debug.Log("ID登録ボタンが押されました");
                break;
            default:
                break;
        }
    }
}
