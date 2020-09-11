using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//一つ一つのトグルにグループを入れる(未完成)

public class Toggle_group : MonoBehaviour
{
    [SerializeField] private Toggle tg_obj;
    // Start is called before the first frame update
    void Start()
    {
        tg_obj.group.name = "Canvas"; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
