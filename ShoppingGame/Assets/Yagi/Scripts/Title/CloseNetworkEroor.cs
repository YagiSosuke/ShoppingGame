using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ネットワークエラーパネルを閉じる*/

public class CloseNetworkEroor : MonoBehaviour
{
    //エラーパネルを閉じるボタン
    public void closeButton()
    {
        Destroy(this.gameObject);
    }
}
