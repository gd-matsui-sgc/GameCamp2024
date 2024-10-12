using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score =0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            score++;
            
        }

       
        scoreText.text = "" + (int)score;
    }

    //点数の加算
    public void AddScore(int nAddNum)
    {
        score += nAddNum;
    }

    //スコアのリセット
    public void ResetScore()
    {
        score = 0;
    }

    //スコアの値を返す
    public int GetScore()
    {
        return score;
    }
}
