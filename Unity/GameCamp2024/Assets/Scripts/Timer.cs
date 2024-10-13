using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する
using TMPro;

/*
 * タイマー用クラス
 */
public class Timer : Base
{
	// フェーズ
	public enum Phase
	{
		// 待機
		Idle,

		// 開始
		Start,

		// 停止
		Stop,
	}

    public int time =0;
    public int Limittime = 0;
    bool blimmitTime =false;

    private float m_nowTime = 0;

    // テキストウィジェット
    [SerializeField]
    public TextMeshProUGUI     timeText = null;

    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
    {
        ResetTime();
    }

    /*
     * 毎フレーム呼ばれる
     */
	protected override void OnUpdate()
    {
		switch((Phase)GetPhase())
		{
		case Phase.Idle:				break;
		case Phase.Start:	_Start();	break;
		}
	}

    /*
     * 再生フェイズ
     */
    private void _Start()
    {
        m_nowTime -= Time.deltaTime;
        if(m_nowTime <= 0.0f)
        {
            m_nowTime = 0.0f;
            blimmitTime = true;
        }
        //世界の残り時間表記
        ApplyTimeToTextWidget();
    }

    /*
     * 時間のリセット
     */
    public void ResetTime()
    {
        blimmitTime = false;
        m_nowTime = Limittime;
        ApplyTimeToTextWidget();
    }

    /*
     * 開始
     */
    public void CountStart()
    {
        ApplyTimeToTextWidget();
        SetPhase((int)Phase.Start);
    }

    /*
     * 停止
     */
    public void CountStop()
    {
        ApplyTimeToTextWidget();
        SetPhase((int)Phase.Idle);
    }

    /*
     * 再生中か
     */
    public bool IsRunning()
    {
        return ((Phase)GetPhase() == Phase.Start);
    }

    /*
     * 時間をテキストに適用
     */
    private void ApplyTimeToTextWidget()
    {
        if(timeText == null){ return; }

        timeText.SetText("残り時間(秒)" + ((int)m_nowTime));
    }

    /*
     * 時間が超えたかを判定
     * @returns 判定結果
     */
    public bool IsTimeLimit()
    {
        return blimmitTime;
    }

}
