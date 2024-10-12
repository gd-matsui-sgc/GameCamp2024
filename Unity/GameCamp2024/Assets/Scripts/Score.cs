using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する
using TMPro;

/*
 * スコア用クラス
 */
public class Score : MonoBehaviour
{
    // テキストウィジェット
    [SerializeField]
    public TextMeshProUGUI     scoreText = null;

    // スコア
    private int      m_score = 0;

    // 前のスコア
    private int      m_prevScore = 0;

    // Tween
    private Tween    m_tween     = null;
    
    /**
     * 生成時に呼ばれます
     */
    public void Awake()
    {
        m_tween = gameObject.AddComponent<Tween>();
    }

    /**
     * Updateの直前に呼ばれます
     */
    public void Start()
    {
        ResetScore();
    }

    /**
     * 毎フレーム呼ばれます
     */
    public void Update()
    {
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     m_score++;
        // }
        ApplyScoreToTextWidget();
    }

    //点数の加算
    public void AddScore(int nAddNum)
    {
        if(m_tween.IsPlaying()){ m_tween.Cancel(); }
        int tempScore = m_score + nAddNum;
        tempScore = Mathf.Clamp(tempScore, 0, 9999999);
        m_tween.Play(Vector3.one * m_score, Vector3.one * (tempScore), 60, Tween.Mode.Linear);
        m_score = tempScore;
    }

    //スコアのリセット
    public void ResetScore()
    {
        m_score = 0;
        m_prevScore = 0;
        ApplyScoreToTextWidget();
    }

    //スコアの値を返す
    public int GetScore()
    {
        return m_score;
    }

    // スコアをテキストに適用
    private void ApplyScoreToTextWidget()
    {
        if(scoreText == null){ return; }
        if(m_tween != null    &&
           m_tween.IsPlaying())
        {
            scoreText.SetText(m_tween.GetProgress().x.ToString());
        }
        else
        {
            scoreText.SetText(m_score.ToString());
        }
    }
}
