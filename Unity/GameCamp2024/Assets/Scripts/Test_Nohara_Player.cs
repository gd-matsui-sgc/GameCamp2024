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
    {  // ¶‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.1f, 0.0f, 0.0f);
        }
        // ‰E‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.1f, 0.0f, 0.0f);
        }
        // ‘O‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.1f);
        }
        // Œã‚ë‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.1f);
        }
    }
    //OnCollisionEnter()
    private void OnCollisionEnter(Collision collision)
    {
        //‚Ô‚Â‚©‚Á‚½‚Æ‚«
        if (collision.gameObject.name == "Test_Nohara_Carrot")
        {
               score.AddScore(10);

              ////F‚ğ•‚É•Ï‰»‚³‚¹‚é
              //GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
