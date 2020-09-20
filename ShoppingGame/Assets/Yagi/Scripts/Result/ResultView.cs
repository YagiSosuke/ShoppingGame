using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*リザルトを徐々に表示する*/

public class ResultView : MonoBehaviour
{
    [SerializeField] GameObject ResultWord;     //結果発表の文字
    

    [SerializeField] GameObject TotalTime;     //合計時間
    Text TotalTimeText;     //合計時間
    float TotalTimeNum;                  //合計時間
    float TotalTimeNumCount = 0;         //合計時間をカウントする
    
    [SerializeField] GameObject OnceTime;      //1つ当たりの時間
    Text OnceTimeText;      //1つ当たりの時間
    float OnceTimeNum;                   //1つ当たりの時間
    float OnceTimeNumCount = 0;          //1つ当たりの時間をカウントする

    [SerializeField] GameObject Bonus;         //ボーナス
    Image BonusImage;
    
    [SerializeField] GameObject Result;        //最終結果
    Text ResultText;        //最終結果
    float ResultNum;                   //1つ当たりの時間
    float ResultNumCount = 0;          //1つ当たりの時間をカウントする

    bool EffectF = false;
    [SerializeField] GameObject PaperEffect;            //紙吹雪のエフェクト

    float count = 0;          //時間をカウントする変数

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Timer_Ctrl.millisecond = " + Timer_Ctrl.millisecond);
        TotalTimeText = TotalTime.GetComponent<Text>();
        TotalTimeText.color = new Color(0, 0, 0, 0);
        TotalTimeNum = (Timer_Ctrl.minute * 100 + Timer_Ctrl.second + Timer_Ctrl.millisecond) * 100;

        OnceTimeText = OnceTime.GetComponent<Text>();
        OnceTimeText.color = new Color(0, 0, 0, 0);
        OnceTimeNum = (int)TotalTimeNum/100.0f / Challenge_List.count;

        BonusImage = Bonus.GetComponent<Image>();
        BonusImage.color = new Color(224 / 255, 240 / 255, 1, 0);
        BonusImage.transform.GetChild(0).GetComponent<Text>().color = BonusImage.transform.GetChild(1).GetComponent<Text>().color = new Color(0, 0, 0, 0);

        ResultText = Result.GetComponent<Text>();
        ResultText.color = new Color(0, 0, 0, 0);
        ResultNum = OnceTimeNum - 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count < 3.0f)
        {
            TotalTimeNumCount = Mathf.Lerp(0, TotalTimeNum, count-1f);     //数字が徐々に増えていく

            //下2桁表示にする
            TotalTimeNumCount = (int)TotalTimeNumCount/100.0f;
            TotalTimeText.text = "" + ((int)TotalTimeNumCount / 100) + ":" + (TotalTimeNumCount - ((int)TotalTimeNumCount / 100)*100);

            if (TotalTimeNumCount % 10 == 0)
            {
                TotalTimeText.text += "0";
            }
            TotalTime.transform.localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 1), Vector3.one, (count-2f)* (count-2f)* (count-2f)* (count-2f));
            TotalTimeText.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, (count-2f));

        }
        else if (count < 4.0f)
        {
            TotalTimeNumCount = Mathf.Lerp(0, TotalTimeNum, 1);     //数字が徐々に増えていく
            //下2桁表示にする
            TotalTimeNumCount = (int)TotalTimeNumCount / 100.0f;
            TotalTimeText.text = "" + ((int)TotalTimeNumCount / 100) + ":" + (TotalTimeNumCount - ((int)TotalTimeNumCount / 100) * 100);
            if (TotalTimeNumCount % 10 == 0) TotalTimeText.text += "0";
            TotalTimeText.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, 1);



            OnceTimeNumCount = Mathf.Lerp(0, OnceTimeNum, count-3.0f);     //数字が徐々に増えていく
            //下2桁表示にする
            OnceTimeNumCount = OnceTimeNumCount * 100.0f;
            OnceTimeNumCount = (int)OnceTimeNumCount / 100.0f;
            OnceTimeText.text = "" + Mathf.FloorToInt(OnceTimeNumCount / 60)+":";
            if (OnceTimeNumCount / 10 == 0)
            {
                OnceTimeText.text += "0";
            }
            OnceTimeText.text += (OnceTimeNumCount - (int)OnceTimeNumCount / 60 *60);

            OnceTime.transform.localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 1), Vector3.one, (count-3.0f)* (count - 3.0f)* (count - 3.0f)* (count - 3.0f));
            OnceTimeText.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, count-3.0f);
        }
        else if (count < 5.0f)
        {
            OnceTimeNumCount = Mathf.Lerp(0, OnceTimeNum, 1);     //数字が徐々に増えていく
            OnceTimeNumCount = OnceTimeNumCount * 100.0f;
            OnceTimeNumCount = (int)OnceTimeNumCount / 100.0f;
            OnceTimeText.text = "" + Mathf.FloorToInt(OnceTimeNumCount / 60) + ":";
            if (OnceTimeNumCount / 10 == 0) OnceTimeText.text += "0";
            OnceTimeText.text += (OnceTimeNumCount - (int)OnceTimeNumCount / 60 * 60);
            OnceTimeText.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, 1);

            
            BonusImage.color = Color.Lerp(new Color(224f / 255f, 240f / 255f, 1, 0), new Color(224f / 255f, 240f / 255f, 1, 1), count - 4.0f);
            BonusImage.transform.GetChild(0).GetComponent<Text>().color = BonusImage.transform.GetChild(1).GetComponent<Text>().color = Color.Lerp(new Color(0,0,0,0), Color.black, count - 4.0f);
        }
        else if (count < 6.0f)
        {
            BonusImage.color = Color.Lerp(new Color(224f / 255f, 240f / 255f, 1, 0), new Color(224f / 255f, 240f / 255f, 1, 1), 1);
            BonusImage.transform.GetChild(0).GetComponent<Text>().color = BonusImage.transform.GetChild(1).GetComponent<Text>().color = Color.black;



            ResultNumCount = Mathf.Lerp(0, ResultNum, count - 5.0f);     //数字が徐々に増えていく
            //下2桁表示にする
            ResultNumCount = ResultNumCount * 100.0f;
            ResultNumCount = (int)ResultNumCount / 100.0f;
            ResultText.text = "" + Mathf.FloorToInt(ResultNumCount / 60) + ":";
            if (ResultNumCount / 10 == 0)
            {
                ResultText.text += "0";
            }
            ResultText.text += (ResultNumCount - (int)ResultNumCount / 60 * 60);


            Result.transform.localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 1), Vector3.one, (count-5.0f)* (count - 5.0f)* (count - 5.0f)* (count - 5.0f));
            ResultText.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, count - 5.0f);
        }
        else
        {
            if (!EffectF)
            {
                Instantiate(PaperEffect);
            }
            EffectF = true;
            ResultText.color = Color.white;
        }

        count += Time.deltaTime;        //時間をカウントする
    }
}
