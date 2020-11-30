using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*詳細を見るボタンのスクリプト*/

public class DetailPreview : MonoBehaviour
{
    //ランキングのデータ
    [SerializeField] GameObject[] RankingObject = new GameObject[5];

    //詳細情報を表示するUI
    [SerializeField] GameObject DetailPanel;
    [SerializeField] Text TotalTimeText;
    [SerializeField] Text ItemNumText;
    [SerializeField] Text OnceTimeText;
    [SerializeField] Text DateText;
    [SerializeField] Text ListText;
    
    string filePath;                    //ファイルパス
    bool DetailPreviewF;                //詳細を表示しているかどうかのフラグ
    
    [SerializeField] int nowOpenDetail;          //現在閲覧している詳細情報は何番か
    Vector3 nowPos;             //現在位置
    Vector3 afterPos;           //移動後の位置

    public float count;

    // Start is called before the first frame update
    void Start()
    {        
        #if UNITY_EDITOR        //デバッグ時
            filePath = Application.dataPath + @"\Ranking\Detail";
        #elif UNITY_STANDALONE
            filePath = Application.persistentDataPath + @"\Ranking\Detail";
        #elif UNITY_ANDROID  //リリース時
            filePath = Application.persistentDataPath + @"\Ranking\Detail";
        #endif
        DetailPreviewF = false;
        nowPos = afterPos = RankingObject[0].transform.position;     //移動後の位置
        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //詳細情報を表示するとき
        if (DetailPreviewF)
        {
            DetailOpen();
        }
        //詳細情報を閉じている時
        else if (!DetailPreviewF)
        {
            DetailClose();
        }
    }

    //詳細情報を開くスクリプト
    public void DetailOpen()
    {
        if (count < 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == nowOpenDetail)
                {
                    RankingObject[i].transform.position = Vector3.Lerp(nowPos, afterPos, 1-Mathf.Pow(1-count, 5));
                }
                else
                {
                    RankingObject[i].GetComponent<CanvasGroup>().alpha = (1 - Mathf.Pow(count, 1));
                }
            }
            DetailPanel.GetComponent<CanvasGroup>().alpha = Mathf.Pow(count, 1);
            count += Time.deltaTime * 2;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == nowOpenDetail)
                {
                    RankingObject[i].transform.position = Vector3.Lerp(nowPos, afterPos, 1);
                }
                else
                {
                    RankingObject[i].GetComponent<CanvasGroup>().alpha = 0;
                }
            }
            DetailPanel.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    //詳細情報を閉じるスクリプト
    public void DetailClose()
    {
        if (count < 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == nowOpenDetail)
                {
                    RankingObject[i].transform.position = Vector3.Lerp(afterPos, nowPos, 1 - Mathf.Pow(1 - count, 5));
                }
                else
                {
                    RankingObject[i].GetComponent<CanvasGroup>().alpha = (Mathf.Pow(count, 1));
                }
            }
            DetailPanel.GetComponent<CanvasGroup>().alpha = (1 - Mathf.Pow(count, 1));
            count += Time.deltaTime * 2;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == nowOpenDetail)
                {
                    RankingObject[i].transform.position = Vector3.Lerp(afterPos, nowPos, 1);
                }
                else
                {
                    RankingObject[i].GetComponent<CanvasGroup>().alpha = 1;
                }
            }
            DetailPanel.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    //ボタンを押した時
    public void ButtonPush(int num)
    {
        DetailPreviewF = !DetailPreviewF;
        //移動時に使うカウントを0にセットする
        count = 0;

        //詳細を開いた時
        if (DetailPreviewF)
        {
            //押したボタン番号を記録
            nowOpenDetail = num;
            //現在のランキング情報の位地を取得
            nowPos = RankingObject[nowOpenDetail].transform.position;

            //表示する詳細情報を設定
            string[] DetailText = File.ReadAllLines(filePath + nowOpenDetail + ".txt");
            TotalTimeText.text = DetailText[0];
            ItemNumText.text = DetailText[1];
            OnceTimeText.text = DetailText[2];
            DateText.text = DetailText[3];
            ListText.text = DetailText[4];

        }
    }
}
