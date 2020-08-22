using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*リスト1つを管理するスクリプト*/

public class ProductContainerScript : MonoBehaviour
{
    GameObject listnameObject;      //商品名を管理するスクリプトを含むオブジェクト
    ListName listname;         //商品名を管理するスクリプト

    public int ListID;      //この商品のID

    GameObject Containor;      //リストを格納するエリア
    GameObject ScrollArea;      //スクロールエリア
    RectTransform AreaRect;     //スクロールエリアのサイズ

    GameObject AddButton;       //追加ボタン

    // Start is called before the first frame update
    void Start()
    {
        listnameObject = GameObject.Find("ViewArea");
        listname = listnameObject.GetComponent<ListName>();

        Containor = GameObject.Find("Containers");
        ScrollArea = GameObject.Find("ScrollArea");
        AreaRect = ScrollArea.GetComponent<RectTransform>();

        AddButton = GameObject.Find("AddButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //削除ボタンを押した時
    public void DeleteButton()
    {
        listname.ListLen--;

        //一番下が削除されるため、内容を入れ替えてあげる必要がある
        for(int i = ListID; i < listname.ListLen; i++)
        {
            listname.ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text = listname.ListContainerEntity[i+1].transform.GetChild(1).GetComponent<InputField>().text;
        }

        //一番下のオブジェクトを削除
        Destroy(listname.ListContainerEntity[listname.ListLen].gameObject);
        listname.ListContainerEntity.RemoveAt(listname.ListLen);

        //コンテナの位置を変更
        Containor.transform.localPosition = new Vector3(Containor.transform.localPosition.x, Containor.transform.localPosition.y - 100, Containor.transform.localPosition.z);
        //エリアのサイズを変更
        AreaRect.sizeDelta = new Vector2(AreaRect.sizeDelta.x, AreaRect.sizeDelta.y - 200);
        //追加ボタンの位置を上げる
        AddButton.transform.localPosition = new Vector3(0, -AreaRect.sizeDelta.y / 2 + 100, 0);
    }
}
