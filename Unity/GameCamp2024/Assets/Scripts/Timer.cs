using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI�@�\�������Ƃ��ɒǋL����
public class Timer : MonoBehaviour
{
    public Text Timertext;
    public int time =0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���E�̎c�莞�ԕ\�L
        Timertext.text = "�c�莞��(�b)" + (60 - (int)Time.time);
    }
}
