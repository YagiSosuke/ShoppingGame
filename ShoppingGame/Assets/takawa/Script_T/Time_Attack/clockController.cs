using System;   // DateTimeに必要
using System.Collections;
using UnityEngine;


//時計の秒針を操作する
public class clockController : MonoBehaviour
{

    public bool sec;   // 秒針の有無
    public bool secTick;   // 秒針を秒ごとに動かすか

    //public GameObject hour;//時間
    //public GameObject minute;//分
    public GameObject second;//秒

    void Start()
    {
        if (!sec)
            Destroy(second); // 秒針を消す
    }

    void Update()
    {
        //DateTime dt = DateTime.Now;//リアルタイムの時計にしたい場合
        float dt_s = Timer_Ctrl.second;
        float dt_m = Timer_Ctrl.minute;

        //hour.transform.eulerAngles = new Vector3(0, 0, (float)dt.Hour / 12 * -360 + (float)dt.Minute / 60 * -30);//時間の針を動かす
        //minute.transform.eulerAngles = new Vector3(0, 0, (float)dt.Minute / 60 * -360);//分の針を動かす

        //if (sec)//リアルタイムで動かす場合
        //{
        //    if (secTick)
        //        second.transform.eulerAngles = new Vector3(0, 0, (float)dt.Second / 60 * -360);
        //    else
        //        second.transform.eulerAngles = new Vector3(0, 0, (float)dt.Second / 60 * -360 + (float)dt.Millisecond / 60 / 1000 * -360);
        //}

        if (sec)
        {
            if (secTick)
                second.transform.eulerAngles = new Vector3(0, 0, dt_s / 60 * -360);
            else
                second.transform.eulerAngles = new Vector3(0, 0, dt_s / 60 * -360 + dt_m / 60 / 1000 * -360);
        }

        //Debug.Log("(float)dt.Second" + (float)dt0.Second);
        //Debug.Log("dt" + dt);


    }
}
