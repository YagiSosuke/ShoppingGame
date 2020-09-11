using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームを終了しますか？のオブジェクトを表示させる

public class Stop_Button : MonoBehaviour
{
    [SerializeField] private GameObject confirmation_obj;//ゲームを終了しますか？のオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void confirmation()//ゲームを終了しますか？のオブジェクトを出し、それ以外のボタンを押せなくさせる
    {
        confirmation_obj.SetActive(true);
    }
}
