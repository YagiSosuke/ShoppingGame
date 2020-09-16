using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲームを終了しますか？のオブジェクトを表示させる

public class Stop_Button : MonoBehaviour
{
    [SerializeField] private GameObject confirmation_obj;//ゲームを終了しますか？のオブジェクト
    public static bool One = true;//一回だけ動作する

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //初めて押したとき、カウントアップをはじめ、
    //2回目以降、ゲームを終了しますか？のオブジェクトを出し、それ以外のボタンを押せなくさせる
    public void confirmation()
    {
        //ボタンの文字を変え、カウントアップを始める
        if (One == true)
        {
            Timer_Ctrl.count_up = true;
            this.transform.GetChild(0).GetComponent<Text>().text = "買い物おわり";
            One = false;
        }
        else
        {
            confirmation_obj.SetActive(true);
        }
    }
}
