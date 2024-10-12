using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する
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
        //世界の残り時間表記
        Timertext.text = "残り時間(秒)" + (60 - (int)Time.time);
    }
}
