using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する
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
        //時間切れ
        if (Limittime < (int)Time.time)
        {
            //終了フラグをオン
            blimmitTime = true;
        }

        //終了していない場合
        if (!blimmitTime)
        {
            //世界の残り時間表記
            Timertext.text = "残り時間(秒)" + (Limittime - (int)Time.time);
        }
    
    }
}
