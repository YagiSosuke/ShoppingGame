using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*保存ボタン - リスト名が入力されていないときは押せないようにする*/

public class SaveButton : MonoBehaviour
{
    [SerializeField] Button button;         //保存ボタン
    [SerializeField] InputField input;      //入力領域

    [SerializeField] GameObject SavePanel;      //保存パネル
    [SerializeField] GameObject SaveCorrectPanel;      //保存完了パネル

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //保存ボタン - 入力領域からの値を受け取る
    public void SaveButtonMethod()
    {
        if(input.text == "")
        {
            //名前が設定されていなければ、ボタンを押せないようにする
            button.interactable = false;
        }
        else
        {
            //名前が設定されていれば、ボタンを押せるようにする
            button.interactable = true;
        }
    }
}
