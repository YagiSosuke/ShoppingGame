using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ポーズボタン*/

public class pause : MonoBehaviour
{
    private AndroidJavaObject _javaClass = null;
    public static long intervalTime;        //停止している間の時間

    public void Start()
    {
        _javaClass = new AndroidJavaObject("com.example.mylibrary.NativeCalculaotor");
    }

    public void PauseButtonClick()
    {
        if (Timer_Ctrl.count_up)
        {
            //時間を止める
            intervalTime = _javaClass.Call<long>("getMillisec");
        }
        else
        {
            //時間を進める
            Timer_Ctrl.firstTime = Timer_Ctrl.firstTime + (_javaClass.Call<long>("getMillisec") - intervalTime);
        }
        Timer_Ctrl.count_up = !Timer_Ctrl.count_up;
    }
}
