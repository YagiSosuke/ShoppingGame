using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Temp : MonoBehaviour
{
    [DllImport("UnityTestDLL")]
    public static extern int ReturnLog();

    void Start()
    {
        Debug.Log("テスト");
        Debug.Log("cnt = " + ReturnLog());
    }
}
