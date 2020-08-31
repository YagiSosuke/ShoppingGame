using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//指定したプレハブを作る（バグありで、これは考えなくてもいい、使わない）

public class ScrollController : MonoBehaviour
{

    [SerializeField]
    RectTransform prefab = null;

    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            var item = GameObject.Instantiate(prefab) as RectTransform;
            item.SetParent(transform, false);

            var text = item.GetComponentInChildren<Text>();
            text.text = "item:" + i.ToString();
        }
    }
}