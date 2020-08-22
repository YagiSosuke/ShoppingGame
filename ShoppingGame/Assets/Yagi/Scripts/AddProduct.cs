using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*「+ 商品を追加」ボタンを押した時*/

public class AddProduct : MonoBehaviour
{
    int ContentsNum;        //コンテンツの個数を格納
    [SerializeField] GameObject ListContainer;      //リストをPrefabから持ってくる
    
    [SerializeField] GameObject Containers; //コンテンツを入れて置くゲームオブジェクト
    [SerializeField] GameObject Area;       //スクロールするエリア
    RectTransform AreaRect;                 //エリアのRectTransform

    [SerializeField] ListName listname;          //商品を管理するスクリプト
    

    // Start is called before the first frame update
    void Start()
    {
        ContentsNum = 1;
        AreaRect = Area.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ボタンを押した時
    public void AddProductPush()
    {        
        //新しいリストを追加
        listname.ListContainerEntity.Add(Instantiate(ListContainer, new Vector3(Containers.transform.position.x, Containers.transform.position.y - (listname.ListLen)*200, Containers.transform.position.z), Quaternion.identity, Containers.transform));
        
        //コンテナの位置を変更
        Containers.transform.localPosition = new Vector3(Containers.transform.localPosition.x, Containers.transform.localPosition.y + 100, Containers.transform.localPosition.z);
        //エリアのサイズを変更
        AreaRect.sizeDelta = new Vector2(AreaRect.sizeDelta.x, AreaRect.sizeDelta.y+200);
        //ボタンは最下部に置く
        this.transform.localPosition = new Vector3(0, -AreaRect.sizeDelta.y/2+100, 0);
        //IDを与える
        listname.ListContainerEntity[listname.ListLen].GetComponent<ProductContainerScript>().ListID = listname.ListLen;
        listname.ListLen++;             //商品数を増やす
    }
}
