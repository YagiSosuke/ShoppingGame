using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*リストのトグルのスクリプト、チェックが入った時に作動*/

public class ListCheckButtonScript : MonoBehaviour
{
    Selection_List_Move_Scene ListMoveSceneScript;      //スクリプト

    // Start is called before the first frame update
    void Start()
    {
        ListMoveSceneScript = GameObject.Find("戻る").GetComponent<Selection_List_Move_Scene>();
    }

    //値が変わった時
    public void OnCheck()
    {
        //読み込むリストを指定して、次のシーンへ行くスクリプトを実行
        ListMoveSceneScript.myList_parent = this.gameObject;
        ListMoveSceneScript.NextScene();
    }

}
