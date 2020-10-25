using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ボタンカーソル*/

public class ButtonCursor : MonoBehaviour
{
    //矢印が上方向か下方向か
    bool CursorSygnalF = false;
    float count = 2;

    void Update()
    {
        //カーソルを下にするとき
        if (CursorSygnalF)
        {
            if (count < 1)
            {
                this.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -90), Quaternion.Euler(0, 0, -270), 1 - Mathf.Pow(1 - count, 5));

                count += Time.deltaTime * 2;
            }
        }
        //カーソルを上にするとき
        else
        {
            if (count < 1)
            {
                this.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -270), Quaternion.Euler(0, 0, -90), 1 - Mathf.Pow(1 - count, 5));

                count += Time.deltaTime * 2;
            }
        }
    }

    //矢印を回転させる
    public void CursorMove()
    {
        CursorSygnalF = !CursorSygnalF;
        count = 0;
    }
}
