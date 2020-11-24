using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*リスト選択画面のゴミ箱ボタンを押した時*/

public class ListDeleteButtonScript : MonoBehaviour
{
    GameObject DeletePanel;     //削除確認パネル
    ListDeletePanelScript panelScript;      //パネルのスクリプト

    void Start()
    {
        DeletePanel = GameObject.Find("ListDeleteCheckPanel");
        panelScript = GameObject.Find("Canvas").GetComponent<ListDeletePanelScript>();
    }

    //ゴミ箱ボタンを押した時
    public void DeleteCheckButton()
    {
        //このゲームオブジェクトを渡してパネルを開く
        panelScript.ListContainer = this.gameObject;
        panelScript.PanelOpen();
    }
}
