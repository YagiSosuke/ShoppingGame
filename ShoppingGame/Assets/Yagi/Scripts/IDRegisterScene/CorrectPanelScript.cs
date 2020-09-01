using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*登録完了を示すパネルのスクリプト*/
/*検索失敗を示すパネルのスクリプト*/

public class CorrectPanelScript : MonoBehaviour
{
    [SerializeField] GameObject CorrectPanel;       //登録完了パネル
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //登録完了 - OKを押した時のスクリプト
    public void OKPanel()
    {
        CorrectPanel.SetActive(false);      //登録完了パネルを見えなくする
    }
}
