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
    {  // ���Ɉړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.1f, 0.0f, 0.0f);
        }
        // �E�Ɉړ�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.1f, 0.0f, 0.0f);
        }
        // �O�Ɉړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.1f);
        }
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.1f);
        }
    }
    //OnCollisionEnter()
    private void OnCollisionEnter(Collision collision)
    {
        //�Ԃ������Ƃ�
        if (collision.gameObject.name == "Test_Nohara_Carrot")
        {
               score.AddScore(10);

              ////�F�����ɕω�������
              //GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
