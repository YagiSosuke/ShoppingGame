﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*前のシーンに戻るスクリプト*/

public class BackSceneScript_Order : MonoBehaviour
{
    //前のシーンに戻る
    public void BackScene()
    {
        SceneManager.LoadScene("OrderScene");
    }
}
