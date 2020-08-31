using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*あなたのIDを表示するスクリプト*/

public class ViewYourID : MonoBehaviour
{
    [SerializeField] Text IDText;       //あなたのIDを表示するテキスト

    // Start is called before the first frame update
    void Start()
    {
        IDText.text = PlayerPrefs.GetString("IDCreateYet");         //あなたのIDを表示する
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
