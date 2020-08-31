using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//買い物を終了して次に進むかそれともまだ続けるかの処理

public class confirmationButton_Ctrl : MonoBehaviour
{
    [SerializeField] private GameObject confirmation_obj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //キャンセルボタンを押したとき、他のボタンを再び押せるようにし、このゲームオブジェクトを見えなくさせる
    public void Cancel()
    {
        confirmation_obj.SetActive(false);
    }

    //OKボタンを押したとき、他のボタンを再び押せるようにし、時間を止めて次のシーンに行けるようにする
    public void NextScene()
    {
        Timer_Ctrl.count_up = false;
        confirmation_obj.SetActive(false);
        //SceneManager.LoadScene("Result");
    }

}
