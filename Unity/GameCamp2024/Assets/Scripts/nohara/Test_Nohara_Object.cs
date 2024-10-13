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
 *  ƒqƒbƒg
 */
    private void OnCollisionEnter(Collision collision)
    {
        //‚Ô‚Â‚©‚Á‚½‚Æ‚«
        if (collision.gameObject.name == "Test_Nohara_Player")
        {

            // Œø‰Ê‰¹
            GetComponent<AudioSource>().Play();
            ////F‚ğ•‚É•Ï‰»‚³‚¹‚é
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
}

