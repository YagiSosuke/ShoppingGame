using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NCMB;

/*登録アカウントを解除するボタン*/

public class AccountRemove : MonoBehaviour
{

    [SerializeField] GameObject Container;      //アカウント情報
    [SerializeField] Text UserName;             //ユーザーネーム
    PauseButton pausebutton;

    // Start is called before the first frame update
    void Start()
    {
        pausebutton = GameObject.Find("Canvas").GetComponent<PauseButton>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ポーズウィンドウを開くボタン
    public void PauseWindowOpen()
    {
        pausebutton.PauseWindowOpen();
        //ポーズウィンドウに名前をセットする
        pausebutton.DataSet(UserName.text, Container);
    }

    //アカウント登録解除するボタン
    public void RemoveButton()
    {
        pausebutton.RemoveButton();

    }
}
