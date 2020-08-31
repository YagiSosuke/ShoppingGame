using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*検索結果確認パネルでのスクリプト、主にボタン系*/

public class SerchCheckPanelScript : MonoBehaviour
{
    [SerializeField] GameObject SerchCheckPanel;        //検索結果確認パネル

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //検索ID確認 - はいを押した時
    public void YesButton()
    {

    }

    //検索ID確認 - いいえを押した時
    public void NoButton()
    {
        SerchCheckPanel.SetActive(false);       //パネルを閉じる
    }
}
