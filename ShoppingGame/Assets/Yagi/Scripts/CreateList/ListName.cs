using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*リストの内容を管理するスクリプト*/

public class ListName : MonoBehaviour
{
    public int ListLen;     //商品数

    public List<GameObject> ListContainerEntity = new List<GameObject>();      //買い物リスト
    public List<string> ShoppingList = new List<string>();   //商品名

    // Start is called before the first frame update
    void Start()
    {
        ListLen = 1;
        ListContainerEntity.Add(GameObject.Find("ProductContainer"));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //全てのリストを削除
        ShoppingList.Clear();
        for(int i = 0; i < ListLen; i++)
        {
            ShoppingList.Add(ListContainerEntity[i].transform.GetChild(1).GetComponent<InputField>().text);
        }
        Debug.Log(ShoppingList[0]);
        */
    }
}
