using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestScript : MonoBehaviour
{
    public Text networkState;

    void Start()
    {

    }

    void Update()
    {
        CheckNetworkState();
    }

    void CheckNetworkState()
    {
        //ネットワークの状態を確認する
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //ネットワークに接続されていない状態
            networkState.text = "ネットワークに未接続";
        }else{
            //ネットワークに接続されている状態
            networkState.text = "ネットワークに接続されている";
        }
    }


}