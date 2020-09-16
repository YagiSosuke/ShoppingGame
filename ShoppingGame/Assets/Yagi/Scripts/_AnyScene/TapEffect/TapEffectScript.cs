using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*画面を押した時のエフェクト(Canvasにアタッチ)*/

public class TapEffectScript : MonoBehaviour
{
    [SerializeField] GameObject Effect;     //エフェクト

    // Update is called once per frame
    void Update()
    {
        //画面がクリックされたらエフェクトを再生
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Effect, Input.mousePosition, Quaternion.identity, this.transform);
        }
    }
}
