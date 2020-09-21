using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*開始前パネルのスクリプト*/

public class AtentionPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //開始前パネル - スタートボタン
    public void StartButton()
    {
        SceneManager.LoadScene("Time_Attack");
    }

    //開始前パネル - 戻るボタン
    public void BackButton()
    {
        this.gameObject.SetActive(false);
    }
}
