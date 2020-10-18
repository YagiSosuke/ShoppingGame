using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//今回のスコアによってランキングを更新するスクリプト

public class RankingUpdate : MonoBehaviour
{
    string filePath;            //ファイルパス
    float[] RankScoreFloat = new float[5];          //ランキングトップ5を格納

    // Start is called before the first frame update
    void Start()
    {
    }
    
    //今回の結果を引数、ランキングを更新するスクリプト
    public void ScoreUpdate(float ResultNum)
    {
        #if UNITY_EDITOR        //デバッグ時
            filePath = Application.dataPath + @"\Ranking\TopScore.txt";
        #elif UNITY_ANDROID  //リリース時
            filePath = Application.persistentDataPath + @"\Ranking\TopScore.txt";
        #endif

        //ファイルが無かった時にファイルを作成
        if (!File.Exists(filePath))
        {
            File.AppendAllText(filePath, "99:99\n99:99\n99:99\n99:99\n99:99");
        }

        string[] allText = File.ReadAllLines(filePath);     //ファイルの中身を取り出す

        for(int i = 0; i < 5; i++)
        {
            Debug.Log("変更前" + i + " = " + allText[i]);
        }

        //ランキング内の数字を秒で表示
        for (int i = 0; i < 5; i++)
        {
            string[] RankScoreString = allText[i].Split(':');
            RankScoreFloat[i] = float.Parse(RankScoreString[0]) * 60 + float.Parse(RankScoreString[1]);
        }

        //現在のスコアをランキングに反映
        float temp, temp2;
        for (int i = 0; i < 5; i++)
        {
            if(RankScoreFloat[i] > ResultNum)
            {
                temp = RankScoreFloat[i];
                RankScoreFloat[i] = ResultNum;
                i++;
                for (; i < 5; i++)
                {
                    temp2 = RankScoreFloat[i];
                    RankScoreFloat[i] = temp;
                    temp = temp2;
                }
            }
        }

        //スコアを分、秒表記に変換
        string[] ScoreTime = new string[5];
        for(int i = 0; i < 5; i++)
        {
            ScoreTime[i] = ((int)(RankScoreFloat[i]) / 60).ToString() + ":" + (RankScoreFloat[i] - ((int)(RankScoreFloat[i]) / 60)).ToString();
        }


        //更新したスコアを保存
        string ScoreAllText = ScoreTime[0] + "\n" + ScoreTime[1] + "\n" + ScoreTime[2] + "\n" + ScoreTime[3] + "\n" + ScoreTime[4];
        File.WriteAllText(filePath, ScoreAllText);


        //表示
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("変更後" + i + " = " + ScoreTime[i]);
        }
    }
}
