using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

 /*
 *  ヒット時
 */
    private void OnCollisionEnter(Collision collision)
    {
        //ぶつかったとき
        if (collision.gameObject.name == "Test_Nohara_Player")
        {

            // 効果音
            GetComponent<AudioSource>().Play();
            ////色を黒に変化させる
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
}

