using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*IDリストシーン読み込み時、スクロールエリアを上にそろえる*/

public class ScrollAreaSet : MonoBehaviour
{
    RectTransform RT;   //幅を調べる変数
    float Height;       //縦幅

    // Start is called before the first frame update
    void Start()
    {
        RT = GetComponent<RectTransform>();
        Height = RT.rect.y;
        Debug.Log("Height = " + Height);
        this.transform.localPosition = new Vector2(0, -Height/2);
    }
}
