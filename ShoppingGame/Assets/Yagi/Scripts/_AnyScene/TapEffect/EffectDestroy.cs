using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    float count = 0.0f;     //カウントする

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;

        if(count > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
