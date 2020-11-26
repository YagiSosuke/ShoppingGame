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


    //ネットワーク接続確認関係
    //エラー時に表示するプレハブ
    [SerializeField]
    GameObject ErrorPanelPrefab;
    //インスタンス
    GameObject Instance;
    //親に設定するオブジェクト
    GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Canvas");
        pausebutton = GameObject.Find("Canvas").GetComponent<PauseButton>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ポーズウィンドウを開くボタン
    public void PauseWindowOpen()
    {
        //ネットワークの状態を確認する
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //ネットワークに接続されていない状態
            Instance = Instantiate(ErrorPanelPrefab);
            Instance.transform.parent = parent.transform;
            Instance.transform.localPosition = Vector3.zero;
            Instance.GetComponent<RectTransform>().offsetMax = Instance.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        }
        else
        {
            pausebutton.PauseWindowOpen();
            //ポーズウィンドウに名前をセットする
            pausebutton.DataSet(UserName.text, Container);
        }
    }

    //アカウント登録解除するボタン
    public void RemoveButton()
    {
        pausebutton.RemoveButton();

    }
}
