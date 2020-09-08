using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*家族一覧シーンへ移動*/

public class GoToAccountListScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //家族一覧へ行くボタン
    public void AccountListButton()
    {
        //登録アカウントへシーン遷移する
        SceneManager.LoadScene("IDListScene");
    }
}
