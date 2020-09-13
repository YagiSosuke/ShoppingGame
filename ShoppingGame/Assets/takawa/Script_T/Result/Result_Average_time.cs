using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//平均時間を表示する
public class Result_Average_time : MonoBehaviour
{
    [SerializeField] private Text ave_time_text;
    int tmp_time=0;//平均時間を秒数だけの状態にする
    int ave_second=0;//平均時間の秒に直す
    int ave_minute=0;//平均時間の分に直す
    // Start is called before the first frame update
    void Start()
    {
        tmp_time = Timer_Ctrl.total_time / Challenge_List.count;//平均時間の秒数を計算する
        ave_second = Mathf.FloorToInt(tmp_time % 60);//平均時間の秒を格納
        ave_minute = Mathf.FloorToInt(tmp_time / 60);//平均時間の分を格納
        ave_time_text.text = (ave_minute + "分"+ ave_second + "秒");//平均時間を表示
    }

    // Update is called once per frame
    void Update()
    {
        //ave_time_text.text = ((Timer_Ctrl.total_time / Challenge_List.count).ToString());
    }
}
