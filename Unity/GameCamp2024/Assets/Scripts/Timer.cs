using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI�@�\�������Ƃ��ɒǋL����
public class Timer : MonoBehaviour
{
    public Text Timertext;
    public int time =0;
    public int Limittime = 0;
    bool blimmitTime =false;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԑ؂�
        if (Limittime < (int)Time.time)
        {
            //�I���t���O���I��
            blimmitTime = true;
        }

        //�I�����Ă��Ȃ��ꍇ
        if (!blimmitTime)
        {
            //���E�̎c�莞�ԕ\�L
            Timertext.text = "�c�莞��(�b)" + (Limittime - (int)Time.time);
        }
    
    }
}
