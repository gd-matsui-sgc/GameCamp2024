using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する
using TMPro;

/*
 * ゲージ用クラス
 */
public class Gauge : Base
{
    // ゲージのベースイメージ
    [SerializeField]
    public Image     gaugeBaseImage = null;

    // ゲージのイメージ
    [SerializeField]
    public Image     gaugeImage = null;

    // 最大に溜まった時のイメージ
    [SerializeField]
    public Image     maximumImage = null;

    // ゲージ量
    private int      m_value = 0;

    // Tween
    private Tween    m_tween     = null;

    // ゲージの幅
    private float    m_width     = 100.0f;

    // 補正値
    private  float   m_collect   = 1.0f;

    // ゲージの最大値
    [SerializeField]
    public int      gaugeValueMax = 100;
    
    /**
     * 生成時に呼ばれます
     */
    protected override void OnAwake()
    {
        m_tween   = gameObject.AddComponent<Tween>();
        m_width   = gaugeBaseImage.rectTransform.sizeDelta.x;
        m_collect = ( float )m_width / ( float )gaugeValueMax;
    }

    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
    {
        ResetValue();
    }

    /*
     * 毎フレーム呼ばれる
     */
	protected override void OnUpdate()
    {
        ApplyValueToWidget();
    }

    /*
     * 点数の加算
     */
    public void AddValue(int nAddNum)
    {
        int tempScore = m_value + nAddNum;
        tempScore = Mathf.Clamp(tempScore, 0, gaugeValueMax);
        m_tween.Play(m_tween.GetProgress(), Vector3.one * (tempScore), 0.3f, Tween.Mode.Linear);
        m_value = tempScore;
    }

    /*
     * スコアのリセット
     * @param immediate    即座に行うか
     */
    public void ResetValue(bool immediate = true)
    {
        m_tween.Cancel();
        m_value = 0;
        // 炎マークの非表示
        if(maximumImage.gameObject.activeSelf)
        {
            maximumImage.gameObject.SetActive(false);
        }
        // 
        if(!immediate)
        {
            m_tween.Play(m_tween.GetProgress(), Vector3.zero, 0.3f, Tween.Mode.Linear);
        }
        else
        {
            ApplyValueToWidget();
        }
    }

    /*
     * 停止
     */
    public void Pause()
    {
        m_tween.Cancel();
    }

    //ゲージの値を返す
    public float GetValue()
    {
        return m_value;
    }

    /*
     * フィーバー中か
     * @returns 判定結果
     */
    public bool IsFever()
    {
        return (m_value == gaugeValueMax);
    }

    // 値をウィジェットに適用
    private void ApplyValueToWidget()
    {
        if(gaugeImage == null){ return; }
        if(m_tween != null    &&
           m_tween.IsPlaying())
        {
            float width = m_tween.GetProgress().x * m_collect;
            gaugeImage.rectTransform.sizeDelta = new Vector2(width, gaugeImage.rectTransform.sizeDelta.y);
        }
        else
        {
            gaugeImage.rectTransform.sizeDelta = new Vector2(m_value * m_collect, gaugeImage.rectTransform.sizeDelta.y);
        }

        if(m_value == 100)
        {
            if(!maximumImage.gameObject.activeSelf){ maximumImage.gameObject.SetActive(true); }
            if(maximumImage.gameObject.activeSelf)
            {
                if(GetPhaseTime() % 30 == 0)
                {
                    maximumImage.rectTransform.localScale = new Vector3(1, 1, 1);
                }
                else if(GetPhaseTime() % 30 == 14)
                {
                    maximumImage.rectTransform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
        else
        {
            if(maximumImage.gameObject.activeSelf){ maximumImage.gameObject.SetActive(false); }
        }
    }
}
