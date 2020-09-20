using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*紙吹雪を時間経過で削除する*/

public class DeletePaper : MonoBehaviour
{
    float count = 0;

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;

        if(count > 6.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
