using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mode_End_Time : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI End_time_text;//買い物モード終了の時間を文字で表示する
    // Start is called before the first frame update
    void Start()
    {
        End_time_text.text = Timer_Ctrl.minute + "分" + Timer_Ctrl.second + "秒";//経過時間を表示
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
