using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*リスト名を取得する(ランキングで使用)*/

public class GetListName : MonoBehaviour
{
    public void GetName()
    {
        RankingUpdate.listName = this.GetComponent<Text>().text;
    }
}
