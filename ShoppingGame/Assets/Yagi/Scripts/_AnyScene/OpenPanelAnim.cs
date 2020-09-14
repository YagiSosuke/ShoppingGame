using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*パネルを展開させたときのアニメーション*/

public class OpenPanelAnim : MonoBehaviour
{
    float count = 0;        //時間をカウント
    public void OpenPanel(GameObject obj)
    {
        if (count < 1.0f)
        {
            count += Time.deltaTime;
        }
        else
        {
            count = 1.0f;
        }
    }
}
