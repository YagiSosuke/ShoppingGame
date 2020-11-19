using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private AndroidJavaObject _javaClass = null;

    [SerializeField] Text t;

    // Start is called before the first frame update
    void Start()
    {
        _javaClass = new AndroidJavaObject("com.example.mylibrary.NativeCalculaotor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(_javaClass != null)
        {
            float f = _javaClass.Call<float>("Add", 9);
            t.text = "" + f;
        }
    }

    public void aaa()
    {
        if (_javaClass != null)
        {
            _javaClass.CallStatic("ShowToast");
        }
    }



    private void OnDestroy()
    {
        // 解放
        _javaClass.Dispose();
        _javaClass = null;
    }
}
