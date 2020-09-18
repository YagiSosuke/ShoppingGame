using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*リザルトを徐々に表示する*/

public class ResultView : MonoBehaviour
{

    [SerializeField] Text TotalTime;     //合計時間
    float TotalTimeNum;                  //合計時間
    float TotalTimeNumCount = 0;         //合計時間をカウントする

    [SerializeField] Text OnceTime;      //1つ当たりの時間
    float OnceTimeNum;                   //1つ当たりの時間
    float OnceTimeNumCount = 0;          //1つ当たりの時間をカウントする

    [SerializeField] GameObject Bonus;         //ボーナス
    Image BonusImage;

    [SerializeField] Text Result;        //最終結果
    float ResultNum;                   //1つ当たりの時間
    float ResultNumCount = 0;          //1つ当たりの時間をカウントする

    float count = 0;          //時間をカウントする変数

    // Start is called before the first frame update
    void Start()
    {
        BonusImage = Bonus.GetComponent<Image>();
        BonusImage.color = new Color(224 / 255, 240 / 255, 1, 0);
        BonusImage.transform.GetChild(0).GetComponent<Text>().color = BonusImage.transform.GetChild(1).GetComponent<Text>().color = new Color(0, 0, 0, 0);

        TotalTime.color = new Color(0, 0, 0, 0);
        TotalTimeNum = (Timer_Ctrl.minute * 100 + Timer_Ctrl.second)*100;

        OnceTime.color = new Color(0, 0, 0, 0);
        OnceTimeNum = (float)Timer_Ctrl.total_time / Challenge_List.count;

        Result.color = new Color(0, 0, 0, 0);
        ResultNum = OnceTimeNum - 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count < 1.0f)
        {
            TotalTimeNumCount = Mathf.Lerp(0, TotalTimeNum, count);     //数字が徐々に増えていく

            //下2桁表示にする
            TotalTimeNumCount = (int)TotalTimeNumCount/100.0f;
            TotalTime.text = "" + ((int)TotalTimeNumCount / 100) + ":" + (TotalTimeNumCount - ((int)TotalTimeNumCount / 100)*100);

            if (TotalTimeNumCount % 10 == 0)
            {
                TotalTime.text += "0";
            }
            TotalTime.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, count);

        }
        else if (count < 2.0f)
        {
            TotalTimeNumCount = Mathf.Lerp(0, TotalTimeNum, 1);     //数字が徐々に増えていく
            //下2桁表示にする
            TotalTimeNumCount = (int)TotalTimeNumCount / 100.0f;
            TotalTime.text = "" + ((int)TotalTimeNumCount / 100) + ":" + (TotalTimeNumCount - ((int)TotalTimeNumCount / 100) * 100);
            if (TotalTimeNumCount % 10 == 0)        TotalTime.text += "0";
            TotalTime.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, 1);



            OnceTimeNumCount = Mathf.Lerp(0, OnceTimeNum, count-1.0f);     //数字が徐々に増えていく
            //下2桁表示にする
            OnceTimeNumCount = OnceTimeNumCount * 100.0f;
            OnceTimeNumCount = (int)OnceTimeNumCount / 100.0f;
            OnceTime.text = "" + Mathf.FloorToInt(OnceTimeNumCount / 60)+":";
            if (OnceTimeNumCount / 10 == 0)
            {
                OnceTime.text += "0";
            }
            OnceTime.text += (OnceTimeNumCount - (int)OnceTimeNumCount / 60 *60);

            OnceTime.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, count-1.0f);
        }
        else if (count < 3.0f)
        {
            OnceTimeNumCount = Mathf.Lerp(0, OnceTimeNum, 1);     //数字が徐々に増えていく
            OnceTimeNumCount = OnceTimeNumCount * 100.0f;
            OnceTimeNumCount = (int)OnceTimeNumCount / 100.0f;
            OnceTime.text = "" + Mathf.FloorToInt(OnceTimeNumCount / 60) + ":";
            if (OnceTimeNumCount / 10 == 0)    OnceTime.text += "0";
            OnceTime.text += (OnceTimeNumCount - (int)OnceTimeNumCount / 60 * 60);
            OnceTime.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, 1);



            BonusImage.color = Color.Lerp(new Color(224f / 255f, 240f / 255f, 1, 0), new Color(224f / 255f, 240f / 255f, 1, 1), count - 2.0f);
            BonusImage.transform.GetChild(0).GetComponent<Text>().color = BonusImage.transform.GetChild(1).GetComponent<Text>().color = Color.Lerp(new Color(0,0,0,0), Color.black, count - 2.0f);
        }
        else if (count < 4.0f)
        {
            BonusImage.color = Color.Lerp(new Color(224f / 255f, 240f / 255f, 1, 0), new Color(224f / 255f, 240f / 255f, 1, 1), 1);
            BonusImage.transform.GetChild(0).GetComponent<Text>().color = BonusImage.transform.GetChild(1).GetComponent<Text>().color = Color.black;



            ResultNumCount = Mathf.Lerp(0, ResultNum, count - 3.0f);     //数字が徐々に増えていく
            //下2桁表示にする
            ResultNumCount = ResultNumCount * 100.0f;
            ResultNumCount = (int)ResultNumCount / 100.0f;
            Result.text = "" + Mathf.FloorToInt(ResultNumCount / 60) + ":";
            if (ResultNumCount / 10 == 0)
            {
                Result.text += "0";
            }
            Result.text += (ResultNumCount - (int)ResultNumCount / 60 * 60);


            Result.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, count - 3.0f);
        }
        else
        {
            Result.color = Color.white;
        }

        count += Time.deltaTime;        //時間をカウントする
    }
}
