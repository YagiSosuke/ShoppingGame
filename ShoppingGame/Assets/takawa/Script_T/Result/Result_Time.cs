using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Result_Time : MonoBehaviour
{
    [SerializeField] private Text Result_time_text;//買い物モード終了の時間を文字で表示する
    // Start is called before the first frame update
    void Start()
    {
        Result_time_text.text = Timer_Ctrl.minute + "分" + Timer_Ctrl.second + "秒";//経過時間を表示
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
