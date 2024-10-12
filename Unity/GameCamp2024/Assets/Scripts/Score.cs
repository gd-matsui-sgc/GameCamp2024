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

    /**
     * 点数の加算
     * @param nAddNum     加算値
     * @param moveTime    その値の表示になるまでの時間(s)
     */
    public void AddScore(int nAddNum, float moveTime = 2.0f)
    {
        //if(m_tween.IsPlaying()){ m_tween.Cancel(); }
        int tempScore = m_score + nAddNum;
        tempScore = Mathf.Clamp(tempScore, 0, 9999999);
        m_tween.Play(m_tween.GetProgress(), Vector3.one * (tempScore), moveTime, Tween.Mode.Linear);
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

    /*
     * 停止
     */
    public void Pause()
    {
        m_tween.Cancel();
    }

    /*
     * Tween再生中か
     * @returns 判定結果
     */
    public bool IsTweenPlaying()
    {
        if(m_tween != null    &&
           m_tween.IsPlaying())
        {
            return true;
        }
        return false;
    }

    // スコアをテキストに適用
    private void ApplyScoreToTextWidget()
    {
        if(scoreText == null){ return; }
        if(m_tween != null    &&
           m_tween.IsPlaying())
        {
            int value = Mathf.RoundToInt(m_tween.GetProgress().x);
            scoreText.SetText(value.ToString());
        }
        else
        {
            scoreText.SetText(m_score.ToString());
        }
    }
}
