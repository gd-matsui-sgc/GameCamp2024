using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI�@�\�������Ƃ��ɒǋL����

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

    //�_���̉��Z
    public void AddScore(int nAddNum)
    {
        score += nAddNum;
    }

    //�X�R�A�̃��Z�b�g
    public void ResetScore()
    {
        score = 0;
    }

    //�X�R�A�̒l��Ԃ�
    public int GetScore()
    {
        return score;
    }
}
