using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*スマホで入力するときに、InputFieldを上に持ってくる*/

public class InputPhone : MonoBehaviour
{
    [SerializeField] InputField input;
    ScrollRect Rect;
    GameObject ScrollArea;          //スクロールする範囲
    ProductContainerScript List;                  //リスト数を取得
    ListName ListName;
        
    //[SerializeField] Text tc;

    // Start is called before the first frame update
    void Start()
    {
        //リスト作成シーンの時
        if (SceneManager.GetActiveScene().name == "CreateListScene")
        {
            Rect = GameObject.Find("ViewArea").GetComponent<ScrollRect>();
            ScrollArea = GameObject.Find("ScrollArea");
            List = this.GetComponent<ProductContainerScript>();
            ListName = GameObject.Find("ViewArea").GetComponent<ListName>();
            #if UNITY_ANDROID       //リリース時
                TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);      //キーボードを開く
            #endif

        }

        EventSystem.current.SetSelectedGameObject(input.gameObject);        //この入力領域を選択状態にする
    }

    // Update is called once per frame
    void Update()
    {
        //リスト作成シーンの時
        if(SceneManager.GetActiveScene().name == "CreateListScene")
        {
            /*
            if (input.touchScreenKeyboard.status == TouchScreenKeyboard.Status.LostFocus)
                tc.text = "lost";
            else if (input.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Done)
                tc.text = "Done";
            else if (input.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Canceled)
                tc.text = "cancel";
            else if (input.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Visible)
                tc.text = "visible";
                */
                
            if (TouchScreenKeyboard.visible == true && input.isFocused)
            {
                Rect.movementType = ScrollRect.MovementType.Unrestricted;
                if (List.ListID == ListName.ListLen - 1)
                {
                    ScrollArea.transform.localPosition = new Vector3(0, 300 + ListName.ListLen * 100, 0);
                }
                else
                {
                    ScrollArea.transform.localPosition = new Vector3(0, (300 + ListName.ListLen * 100) - ((ListName.ListLen-1)-List.ListID)*200, 0);
                }
            }
            else if(TouchScreenKeyboard.visible == false)
            {
                Rect.movementType = ScrollRect.MovementType.Elastic;
            }

        }
    }
}
