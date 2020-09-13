using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*保存が完了したことを示すパネル*/

public class SaveCorrectPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //はいボタン
    public void YesButton()
    {
        //タイムアタック画面へ移動
        SceneManager.LoadScene("Selection_List");
    }

    //いいえボタン
    public void NoButton()
    {
        //タイトル画面へ移動
        SceneManager.LoadScene("Title");
    }
}
