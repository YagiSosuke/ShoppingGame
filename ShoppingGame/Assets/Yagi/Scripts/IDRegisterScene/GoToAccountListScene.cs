using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*家族一覧シーンへ移動*/

public class GoToAccountListScene : MonoBehaviour
{
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
    }

    //家族一覧へ行くボタン
    public void AccountListButton()
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
        else {
            //登録アカウントへシーン遷移する
            SceneManager.LoadScene("IDListScene");
        }
    }
}
