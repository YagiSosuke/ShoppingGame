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
    ListName List;                  //リスト数を取得

    // Start is called before the first frame update
    void Start()
    {
        //リスト作成シーンの時
        if (SceneManager.GetActiveScene().name == "CreateListScene")
        {
            Rect = GameObject.Find("ViewArea").GetComponent<ScrollRect>();
            ScrollArea = GameObject.Find("ScrollArea");
            List = GameObject.Find("ViewArea").GetComponent<ListName>();
        }

        EventSystem.current.SetSelectedGameObject(input.gameObject);        //この入力領域を選択状態にする
    }

    // Update is called once per frame
    void Update()
    {
        //リスト作成シーンの時
        if(SceneManager.GetActiveScene().name == "CreateListScene")
        {
            if (input.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                Rect.movementType = ScrollRect.MovementType.Unrestricted;
                ScrollArea.transform.localPosition = new Vector3(0, 300 + List.ListLen * 100, 0);
            }
            else
            {
                Rect.movementType = ScrollRect.MovementType.Elastic;
            }

        }
    }
}
