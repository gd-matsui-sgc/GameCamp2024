using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Nohara_Player : MonoBehaviour
{

    public Score score;
    // Start is called before the first frame update
    void Start()
    {
        Work.gameScore = score;
    }

    // Update is called once per frame
    void Update()
    {  // 左に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-1.0f, 0.0f, 0.0f);
        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.1f, 0.0f, 0.0f);
        }
        // 前に移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.1f);
        }
        // 後ろに移動
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.1f);
        }
    }
    //OnCollisionEnter()
    private void OnCollisionEnter(Collision collision)
    {
        //ぶつかったとき
        if (collision.gameObject.name == "Test_Nohara_Carrot")
        {
               score.AddScore(10);
            // 効果音
            GetComponent<AudioSource>().Play();
            ////色を黒に変化させる
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
