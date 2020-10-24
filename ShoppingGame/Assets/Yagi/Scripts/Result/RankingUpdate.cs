using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//今回のスコアによってランキングを更新するスクリプト

public class RankingUpdate : MonoBehaviour
{
    //詳細データ
    class DetailData
    {
        string TotalTime;   //合計時間
        string ItemNum;     //商品数
        string OnceTime;    //商品一つ当たりの時間

        //値設定メソッド
        public void DataSet(string[] Data)
        {
            TotalTime = Data[0];
            ItemNum = Data[1];
            OnceTime = Data[2];
        }

        public void setTotalTime(string data)
        {
            TotalTime = data;
        }
        public string getTotalTime()
        {
            return TotalTime;
        }
        public void setItemNum(string data)
        {
            ItemNum = data;
        }
        public string getItemNum()
        {
            return ItemNum;
        }
        public void setOnceTime(string data)
        {
            OnceTime = data;
        }
        public string getOnceTime()
        {
            return OnceTime;
        }
    }

    string filePath;            //ファイルパス
    float[] RankScoreFloat = new float[5];          //ランキングトップ5を格納

    //詳細情報のファイルパス
    string detailFilePath;
    DetailData[] detailData = new DetailData[5];        //詳細情報 

    // Start is called before the first frame update
    void Start()
    {
    }
    
    //今回の結果を引数、ランキングを更新するスクリプト
    public void ScoreUpdate(float ResultNum, string RTotalTime, string RItemNum, string ROnceTime)
    {
        #if UNITY_EDITOR        //デバッグ時
            filePath = Application.dataPath + @"\Ranking\TopScore.txt";
            detailFilePath = Application.dataPath + @"\Ranking\";
        #elif UNITY_ANDROID  //リリース時
            filePath = Application.persistentDataPath + @"\Ranking\TopScore.txt";
            detailFilePath = Application.persistentDataPath + @"\Ranking\";
        #endif

        //ファイルが無かった時にファイルを作成
        if (!File.Exists(filePath))
        {
            File.AppendAllText(filePath, "99:99\n99:99\n99:99\n99:99\n99:99");
        }
        //詳細データファイルが無かった時に作成
        for (int i = 0; i < 5; i++)
        {
            if (!File.Exists(detailFilePath + "Detail" + i + ".txt"))
            {
                File.AppendAllText(detailFilePath + "Detail" + i + ".txt", "99:99\n1\n99:99");
            }
        }

        string[] allText = File.ReadAllLines(filePath);     //ファイルの中身を取り出す
        //詳細ファイルの中身を取り出す
        for(int i = 0; i < 5; i++)
        {
            string[] DetailText = File.ReadAllLines(detailFilePath + "Detail" + i + ".txt");
            detailData[i].DataSet(DetailText);
        }

        //ランキング内の数字を秒で表示
        for (int i = 0; i < 5; i++)
        {
            string[] RankScoreString = allText[i].Split(':');
            RankScoreFloat[i] = float.Parse(RankScoreString[0]) * 60 + float.Parse(RankScoreString[1]);
        }


        //現在のスコアをランキングに反映
        float temp, temp2;
        DetailData detailTemp = new DetailData();
        DetailData detailTemp2 = new DetailData();
        for (int i = 0; i < 5; i++)
        {
            //現在のスコアがランキングに入るとき
            if(RankScoreFloat[i] > ResultNum)
            {
                //ランキング更新
                temp = RankScoreFloat[i];
                RankScoreFloat[i] = ResultNum;
                //詳細情報更新
                detailTemp.setTotalTime(detailData[i].getTotalTime());
                detailTemp.setItemNum(detailData[i].getItemNum());
                detailTemp.setOnceTime(detailData[i].getOnceTime());
                detailData[i].setTotalTime(RTotalTime);
                detailData[i].setItemNum(RItemNum);
                detailData[i].setOnceTime(ROnceTime);
                i++;
                for (; i < 5; i++)
                {
                    //ランキング情報をずらす
                    temp2 = RankScoreFloat[i];
                    RankScoreFloat[i] = temp;
                    temp = temp2;
                    //詳細情報をずらす
                    detailTemp2.setTotalTime(detailData[i].getTotalTime());
                    detailTemp2.setItemNum(detailData[i].getItemNum());
                    detailTemp2.setOnceTime(detailData[i].getOnceTime());
                    detailData[i].setTotalTime(detailTemp.getTotalTime());
                    detailData[i].setItemNum(detailTemp.getItemNum());
                    detailData[i].setOnceTime(detailTemp.getOnceTime());
                    detailTemp.setTotalTime(detailTemp2.getTotalTime());
                    detailTemp.setItemNum(detailTemp2.getItemNum());
                    detailTemp.setOnceTime(detailTemp2.getOnceTime());
                }
            }
        }

        //スコアを分、秒表記に変換
        string[] ScoreTime = new string[5];
        for(int i = 0; i < 5; i++)
        {
            ScoreTime[i] = ((int)(RankScoreFloat[i]) / 60).ToString() + ":" + (RankScoreFloat[i] - ((int)(RankScoreFloat[i]) / 60)*60).ToString("00.00");
        }


        //更新したスコアを保存
        string ScoreAllText = ScoreTime[0] + "\n" + ScoreTime[1] + "\n" + ScoreTime[2] + "\n" + ScoreTime[3] + "\n" + ScoreTime[4];
        File.WriteAllText(filePath, ScoreAllText);
        for(int i = 0; i < 5; i++)
        {
            string DetailAllText = detailData[i].getTotalTime() + "\n" + detailData[i].getItemNum() + "\n" + detailData[i].getOnceTime();
            File.WriteAllText(detailFilePath + "Detail" + i + ".txt", DetailAllText);
        }
    }
}
