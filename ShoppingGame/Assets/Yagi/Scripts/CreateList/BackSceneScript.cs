using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*前のシーンに戻るスクリプト*/

public class BackSceneScript : MonoBehaviour
{
    [SerializeField] InputField ListName;       //名前入力領域
    [SerializeField] SaveWindowScript SaveButton;     //保存するボタンが入っているスクリプト


    //前のシーンに戻る
    public void BackScene()
    {
        SceneManager.LoadScene("Title");
        /*
        if (ListName.text == "")
        {
            SceneManager.LoadScene("Title");
            //破棄しますか？の表示？
        }
        else
        {
            //保存する
            SaveButton.WindowOpen();
        }
        */
    }
}
