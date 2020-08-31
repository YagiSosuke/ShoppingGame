using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//次のシーンに進むか前のシーンに戻るかの処理

public class Selection_List_Move_Scene : MonoBehaviour
{
    [SerializeField] private List_Instanceate list_Instanceate;
    GameObject myList_Child;
    Toggle ListToggle;
    TextMeshProUGUI Listnametext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()//選択したリストのデータを保持したまま次のシーンへ行く
    {
        
        foreach(var s in list_Instanceate.myList)//参照しているものが違う？
        {
            Debug.Log("リスト表示:" + s.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
            myList_Child = s.transform.GetChild(1).gameObject;
            ListToggle = myList_Child.GetComponent<Toggle>();
            if(ListToggle.isOn == true)
            {
                Debug.Log("選んだリスト:"+ s.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
            }
            
        }
        Debug.Log("次のシーンへ");
        SceneManager.LoadScene("Selection_List");
    }

    public void BackScene()//タイトルに戻る
    {
        SceneManager.LoadScene("Title");
    }
}
