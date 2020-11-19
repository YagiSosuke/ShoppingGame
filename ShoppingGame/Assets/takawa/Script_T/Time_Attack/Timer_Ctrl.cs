using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//買い物中にタイマーを進めて時間を記録し、表示させる

public class Timer_Ctrl : MonoBehaviour
{
    public static bool count_up = false;//カウントアップをするかしないか（falseにすると時間を止められる）
    [SerializeField] private Text time_text;//秒を文字で表示する
    float time;//小数含めた秒数を格納する
    public static int total_time;//一個当たりの平均を求めるのに使う
    public static int second;//秒を格納する(結果発表の時に使う)
    public static int minute;//分を格納する(結果発表の時に使う)
    public static float millisecond;//ミリ秒を格納する(0.○○と表記されるので、そのまま文字にする時は100倍したりしないといけない)


    //八木追加分
    public static long firstTime;     //開始時の時間
    AndroidJavaObject _javaClass = null;        //バックグラウンドで時間測るプログラム(Javaで記述)


    // Start is called before the first frame update
    void Start()//時間の初期化
    {
        time = 0;


        //八木追加分
        _javaClass = new AndroidJavaObject("com.example.mylibrary.NativeCalculaotor");      //Javaを読み込み
        firstTime = _javaClass.Call<long>("getMillisec");       //開始時間を格納
    }

    // Update is called once per frame
    void Update()//時間をtimeに格納し、それを文字にして表示する
    {
        if (count_up == true)//まだ買い物が続いてるとき
        {
#if UNITY_STANDALONE
            time += Time.deltaTime;
#elif UNITY_EDITOR
            time += Time.deltaTime;
#elif UNITY_ANDROID
            time = _javaClass.Call<long>("getMillisec") - firstTime;
            time = time/1000;
#endif
            total_time = Mathf.FloorToInt(time);//合計時間（int）を格納
            second = Mathf.FloorToInt(time % 60);//秒を格納
            minute = Mathf.FloorToInt(time / 60);//分を格納
            millisecond = time - second-(minute*60);//ミリ秒を格納
        }
        
        time_text.text = minute + ":" + second.ToString("00") + "."+ (Mathf.FloorToInt(millisecond*100).ToString("00"));//経過時間を表示
    }

   
}
