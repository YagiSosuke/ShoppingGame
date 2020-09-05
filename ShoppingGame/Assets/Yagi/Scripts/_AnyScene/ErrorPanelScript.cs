using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*エラー時に表示されるパネル*/
/*リスト作成シーン , 依頼シーン*/

public class ErrorPanelScript : MonoBehaviour
{
    [SerializeField] GameObject ErrorPanel;     //エラー時に表示するパネル

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //OKを押したらウィンドウが消える
    public void OKButton()
    {
        ErrorPanel.SetActive(false);
    }
}
