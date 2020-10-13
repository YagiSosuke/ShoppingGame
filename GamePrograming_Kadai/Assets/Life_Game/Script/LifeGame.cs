using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGame : MonoBehaviour
{
    [Range(0.0f, 2.0f)] public float Interval = 0.5f;
    public Material Living;
    public Material Dead;
    public GameObject Cell;

    // MeshRenderer配列の横の数
    int Width = 80;
    // MeshRenderer配列の縦の数
    int Height = 50;

    // 参照用セルの横、縦の数
    int refWidth;
    int refHeight;

    // MeshRenderer配列
    MeshRenderer[] cellsMR;
    // 参照用セルの配列
    bool[] refCells;
    // 作業用セル配列
    bool[] tmpCells;

    // Start is called before the first frame update
    void Start()
    {
        // 配列の確保と初期化
        AllocArray();
        InitArray();
        RandomDot();

        // コルーチンを開始
        StartCoroutine("GameStart");
    }

    // Update is called once per frame
    void Update()
    {
        // マウスの右ボタンのクリックを検出
        if (Input.GetMouseButtonDown(0))
        {
            RandomDot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GliderGun();
        }
    }

    // 配列の確保
    private void AllocArray()
    {
        // MeshRenderer配列の要素数
        int size = Width * Height;

        // センチネル用に縦横2つセルを増やす
        refWidth = Width + 2;
        refHeight = Height + 2;
        // 参照用セルの要素数
        int refSize = refWidth * refHeight;

        // 配列の確保
        cellsMR = new MeshRenderer[size];
        refCells = new bool[refSize];
        tmpCells = new bool[refSize];
    }

    // 配列の初期化
    private void InitArray()
    {
        for (int h = 0; h < Height; h++)
        {
            for (int w = 0; w < Width; w++)
            {
                // 要素(h,w)の1次元配列上の位置を求める
                int n = h * Width + w;
                // Cubuの角を原点に合わせる
                Vector3 position = new Vector3(w + 0.5f, 0, h + 0.5f);
                cellsMR[n] = ((GameObject)Instantiate(Cell, position, Quaternion.identity)).GetComponent<MeshRenderer>();
            }
        }
    }
    // LifeGameのスタート
    public IEnumerator GameStart()
    {
        // コルーチンとして実行するの、無限ループでOK
        while (true)
        {
            Next(); // 次の状態にする
            RenewCells(); // 表示を更新
            yield return new WaitForSeconds(Interval); // 指定秒数の間、処理を中断する
        }
    }

    // 次の状態に変更する
    private void Next()
    {
        // 両端はセンチネルなので、除外して処理を行う
        for (int h = 1; h < refHeight - 1; h++)
        {
            for (int w = 1; w < refWidth - 1; w++)
            {
                // 参照のセルの位置を求める
                int pos = h * refWidth + w;
                // 位置（w,h）の周囲の生きているセルの数を得る
                int lives = SumAround(w, h);
                if (lives == 3)
                {
                    // 周りに3つの生きているセルがいれば新しい命が誕生
                    tmpCells[pos] = true;
                }
                else if (lives == 2)
                {
                    // 周りに2つの生きているセルがいれば現状維持
                    tmpCells[pos] = refCells[pos];
                }
                else
                {
                    // それ以外は、寂しすぎるか混雑しすぎで死亡
                    tmpCells[pos] = false;
                }
            }
        }
        tmpCells.CopyTo(refCells, 0);
        System.Array.Clear(tmpCells, 0, tmpCells.Length);
    }

    private int SumAround(int w, int h)
    {
        int total = 0;
        int up = (h + 1) * refWidth;
        int md = h * refWidth;
        int dw = (h - 1) * refWidth;

        // 8近傍のセルを調べる
        total += BoolToInt(refCells[up + (w - 1)]);
        total += BoolToInt(refCells[up + w]);
        total += BoolToInt(refCells[up + (w + 1)]);

        total += BoolToInt(refCells[md + (w - 1)]);
        total += BoolToInt(refCells[md + (w + 1)]);

        total += BoolToInt(refCells[dw + (w - 1)]);
        total += BoolToInt(refCells[dw + w]);
        total += BoolToInt(refCells[dw + (w + 1)]);

        return total;
    }

    private int BoolToInt(bool b)
    {
        return b ? 1 : 0;
    }

    private void RenewCells()
    {
        for (int h = 0; h < Height; h++)
        {
            for (int w = 0; w < Width; w++)
            {
                int n = h * Width + w;
                int p = (h + 1) * refWidth + (w + 1);
                cellsMR[n].material = refCells[p] ? Living : Dead;
            }
        }
    }

    private void RandomDot()
    {
        int dotsNum = (Width * Height) / 4;
        for (int n = 0; n < dotsNum; n++)
        {
            int px = Random.Range(0, Width);
            int py = Random.Range(0, Height);
            int p = (py + 1) * refWidth + (px + 1);

            refCells[p] = true;
        }
        RenewCells();
    }

    private void GliderGun()
    {
        const int xTop = 10;
        const int yTop = 10;
        string[] guns = {
            "........................O...........",
            "......................O.O...........",
            "............OO......OO............OO",
            "...........O...O....OO............OO",
            "OO........O.....O...OO..............",
            "OO........O...O.OO....O.O...........",
            "..........O.....O.......O...........",
            "...........O...O....................",
            "............OO......................"
        };
        for (int i = 0; i < refCells.Length; i++)
        {
            refCells[i] = false;
        }
        for (int y = 0; y < guns.Length; y++)
        {
            int p = (yTop + y + 1) * refWidth + (xTop + 1);
            foreach (char c in guns[y])
            {
                refCells[p++] = c == 'O' ? true : false;
            }
        }

        RenewCells();
    }
}