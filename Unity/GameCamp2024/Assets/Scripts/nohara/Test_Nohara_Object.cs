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
 *  �q�b�g��
 */
    private void OnCollisionEnter(Collision collision)
    {
        //�Ԃ������Ƃ�
        if (collision.gameObject.name == "Test_Nohara_Player")
        {

            // ���ʉ�
            GetComponent<AudioSource>().Play();
            ////�F�����ɕω�������
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
}

