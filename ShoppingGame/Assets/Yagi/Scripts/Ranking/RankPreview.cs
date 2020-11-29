using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

//シーンを開いた時、現在のランキングを設定するスクリプト

public class RankPreview : MonoBehaviour
{
    //ファイルパス
    string filePath;
    //スコアを表示するテキスト
    [SerializeField] Text[] ScoreText = new Text[5];


    //詳細情報のファイルパス
    string detailFilePath;

    // Start is called before the first frame update
    void Start()
    {
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
        if(!File.Exists(filePath))
        {
            File.AppendAllText(filePath, "99:00.00\n99:00.00\n99:00.00\n99:00.00\n99:00.00");
        }
        //詳細データファイルが無かった時に作成
        for (int i = 0; i < 5; i++)
        {
            if (!File.Exists(detailFilePath + "Detail" + i + ".txt"))
            {
                File.AppendAllText(detailFilePath + "Detail" + i + ".txt", "99:00.00\n1\n99:00.00\n9999/99/99");
            }
        }

        string[] allText = File.ReadAllLines(filePath);

        for(int i = 0; i < 5; i++)
        {
            //テキストに値をセット
            ScoreText[i].text = allText[i];
        }
    }
}
