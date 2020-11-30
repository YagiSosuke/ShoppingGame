using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//今回のスコアによってランキングを更新するスクリプト


//詳細データ
public class DetailData
{
    string TotalTime;   //合計時間
    string ItemNum;     //商品数
    string OnceTime;    //商品一つ当たりの時間
    string NowDate;     //現在の時間
    string ListName;      //リスト名

    //値設定メソッド
    public void DataSet(string Data0, string Data1, string Data2, string Data3, string Data4)
    {
        Debug.Log("値設定");
        TotalTime = Data0;
        ItemNum = Data1;
        OnceTime = Data2;
        NowDate = Data3;
        ListName = Data4;
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
    public void setNowDate(string data)
    {
        NowDate = data;
    }
    public string getNowDate()
    {
        return NowDate;
    }
    public void setListName(string data)
    {
        ListName = data;
    }
    public string getListName()
    {
        return ListName;
    }
}

public class RankingUpdate : MonoBehaviour
{

    string filePath;            //ファイルパス
    float[] RankScoreFloat = new float[5];          //ランキングトップ5を格納

    //詳細情報のファイルパス
    string detailFilePath;

    public static string listName;      //リスト名

    // Start is called before the first frame update
    void Start()
    {

    }
    
    //今回の結果を引数、ランキングを更新するスクリプト
    public void ScoreUpdate(float ResultNum, string RTotalTime, string RItemNum, string ROnceTime)
    {
        DetailData[] detaildata = new DetailData[5];
        for(int i = 0; i < 5; i++)
        {
            detaildata[i] = new DetailData();
        }

        #if UNITY_EDITOR        //デバッグ時
            filePath = Application.dataPath + @"\Ranking\TopScore.txt";
            detailFilePath = Application.dataPath + @"\Ranking\";
        #elif UNITY_STANDALONE
            filePath = Application.persistentDataPath + @"\Ranking\TopScore.txt";
            detailFilePath = Application.persistentDataPath + @"\Ranking\";
        #elif UNITY_ANDROID  //リリース時
            filePath = Application.persistentDataPath + @"\Ranking\TopScore.txt";
            detailFilePath = Application.persistentDataPath + @"\Ranking\";
        #endif

        //ファイルが無かった時にファイルを作成
        if (!File.Exists(filePath))
        {
            File.AppendAllText(filePath, "99:00.00\n99:00.00\n99:00.00\n99:00.00\n99:00.00");
        }
        //詳細データファイルが無かった時に作成
        for (int i = 0; i < 5; i++)
        {
            if (!File.Exists(detailFilePath + "Detail" + i + ".txt"))
            {
                File.AppendAllText(detailFilePath + "Detail" + i + ".txt", "99:00.00\n1\n99:00.00\n9999/99/99\nリスト");
            }
        }

        //ランキングが旧型式だった場合アップデート
        string[] detailtext = File.ReadAllLines(detailFilePath + "Detail0.txt");
        if (detailtext.Length < 5)
        {
            for (int i = 0; i < 5; i++)
            {
                File.WriteAllText(detailFilePath + "Detail" + i + ".txt", "99:00.00\n1\n99:00.00\n9999/99/99\nリスト");
            }
        }

        string[] allText = File.ReadAllLines(filePath);     //ファイルの中身を取り出す
        //詳細ファイルの中身を取り出す
        for(int i = 0; i < 5; i++)
        {
            string[] DetailText = File.ReadAllLines(detailFilePath + "Detail" + i + ".txt");
            detaildata[i].DataSet(DetailText[0], DetailText[1], DetailText[2], DetailText[3], DetailText[4]);
            Debug.Log(i + "の合計時間を取得" + detaildata[i].getTotalTime());
        }

        //ランキング内の数字を秒で表示
        for (int i = 0; i < 5; i++)
        {
            string[] RankScoreString = allText[i].Split(':');
            RankScoreFloat[i] = float.Parse(RankScoreString[0]) * 60 + float.Parse(RankScoreString[1]);
        }


        //現在のスコアをランキングに反映
        DateTime dt = DateTime.Now;
        Debug.Log("今は + " + dt.ToString("d"));

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
                detailTemp.setTotalTime(detaildata[i].getTotalTime());
                detailTemp.setItemNum(detaildata[i].getItemNum());
                detailTemp.setOnceTime(detaildata[i].getOnceTime());
                detailTemp.setNowDate(detaildata[i].getNowDate());
                detailTemp.setListName(detaildata[i].getListName());
                detaildata[i].setTotalTime(RTotalTime);
                detaildata[i].setItemNum(RItemNum);
                detaildata[i].setOnceTime(ROnceTime);
                detaildata[i].setNowDate(dt.ToString("d"));
                detaildata[i].setListName(RankingUpdate.listName);
                i++;
                for (; i < 5; i++)
                {
                    //ランキング情報をずらす
                    temp2 = RankScoreFloat[i];
                    RankScoreFloat[i] = temp;
                    temp = temp2;
                    //詳細情報をずらす
                    detailTemp2.setTotalTime(detaildata[i].getTotalTime());
                    detailTemp2.setItemNum(detaildata[i].getItemNum());
                    detailTemp2.setOnceTime(detaildata[i].getOnceTime());
                    detailTemp2.setNowDate(detaildata[i].getNowDate());
                    detailTemp2.setListName(detaildata[i].getListName());
                    detaildata[i].setTotalTime(detailTemp.getTotalTime());
                    detaildata[i].setItemNum(detailTemp.getItemNum());
                    detaildata[i].setOnceTime(detailTemp.getOnceTime());
                    detaildata[i].setNowDate(detailTemp.getNowDate());
                    detaildata[i].setListName(detailTemp.getListName());
                    detailTemp.setTotalTime(detailTemp2.getTotalTime());
                    detailTemp.setItemNum(detailTemp2.getItemNum());
                    detailTemp.setOnceTime(detailTemp2.getOnceTime());
                    detailTemp.setNowDate(detailTemp2.getNowDate());
                    detailTemp.setListName(detailTemp2.getListName());
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
            string DetailAllText = detaildata[i].getTotalTime() + "\n" + detaildata[i].getItemNum() + "\n" + detaildata[i].getOnceTime() + "\n" + detaildata[i].getNowDate() + "\n" + detaildata[i].getListName();
            File.WriteAllText(detailFilePath + "Detail" + i + ".txt", DetailAllText);
        }
    }
}
