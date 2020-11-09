using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*インターネット接続されているかチェックするスクリプト*/
/*その後、シーンを遷移するか決める*/

public class NetworkCheck : MonoBehaviour
{
    //エラー時に表示するプレハブ
    [SerializeField] GameObject ErrorPanelPrefab;
    //インスタンス
    GameObject Instance;
    //親に設定するオブジェクト
    GameObject parent;

    //正常時にシーンを読み込むスクリプト
    titleButtonScript titlescript;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Canvas1");
        titlescript = GetComponent<titleButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CheckNetworkState()
    {
        //ネットワークの状態を確認する
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            //ネットワークに接続されていない状態
            Instance = Instantiate(ErrorPanelPrefab);
            Instance.transform.parent = parent.transform;
            Instance.transform.localPosition = Vector3.zero;
            Instance.GetComponent<RectTransform>().offsetMax = Instance.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        }
        else
        {
            //ネットワークに接続されている状態
            titlescript.TappedButton();
        }
    }
}
