using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : BaseScene
{
	// フェーズ
	public enum Phase
	{
		// フェードイン
		FadeIn,

		// ゲームスタート
		GameStart,

		// チュートリアル
		Tutorial,

		// ゲーム中
		Run,

		// タイプアップ
		TimeUp,

		// フェードアウト
		FadeOut,
	}

	// スコアウィジェット
	[SerializeField]
	public Score  score = null;

	// タイマー
	[SerializeField]
	public Timer  timer = null;

	// プレイヤー
	[SerializeField]
	public Player player = null;

	// テロップ
	[SerializeField]
	public Telop telop = null;

	// ゲージ
	[SerializeField]
	public Gauge gauge = null;

	// チュートリアル
	[SerializeField]
	public Tutorial tutorial = null;

	// 人参マネージャー
	[SerializeField]
	public CarrotManager carrotManager = null;

	// 仮）フィーバータイムカウンタ
	private float m_feverTimeCount = 0;

	private Tutorial m_tutorial = null;

    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
	{
		Work.gameScore = score;
		Work.gauge     = gauge;
		Work.player    = player;
		Work.totalScore = 0;
		timer.ResetTime();
	}

    /*
     * 毎フレーム呼ばれる
     */
	protected override void OnUpdate()
	{
		switch ((Phase)GetPhase())
		{
			case Phase.FadeIn: 		_FadeIn();		break;
			case Phase.Tutorial:	_Tutorial();	break;
			case Phase.GameStart:	_GameStart();	break;
			case Phase.Run:			_Run();			break;
			case Phase.TimeUp:		_TimeUp();		break;
			case Phase.FadeOut: 	_FadeOut();		break;
		}
	}

    /*
     * フェードイン
     */
	protected void _FadeIn()
	{
		if (GetPhaseTime() == 0)
		{
			Work.fade.Play(Fade.FadeType.In, Fade.ColorType.Black, Fade.FadeSpeed.Norma);
		}
		else if (!Work.fade.IsPlaying())
		{
			SetPhase((int)Phase.Tutorial);
		}
	}

    /*
     * チュートリアル
     */
	protected void _Tutorial()
	{
		if (GetPhaseTime() == 0 &&
			tutorial != null    )
		{
			GameObject gameObject = Instantiate(tutorial.gameObject);
			m_tutorial = gameObject.GetComponent<Tutorial>();
		}
		else if (m_tutorial != null &&
				 m_tutorial.IsExited())
		{
			GameObject.Destroy(m_tutorial.gameObject);
			m_tutorial = null;
			SetPhase((int)Phase.GameStart);
		}
		else if( m_tutorial == null)
		{
			SetPhase((int)Phase.GameStart);
		}
	}
    /*
     * ゲームスタート
     */
	protected void _GameStart()
	{
		if( GetPhaseTime() == 0)
		{
			telop.Play(Telop.TelopType.GameStart);
		}
		else if( GetPhaseTime() > 0)
		{
			if(!telop.IsPlaying())
			{
				SetPhase((int)Phase.Run);
			}
		}
	}

    /*
     * 実行
     */
	protected void _Run()
	{
		if(GetPhaseTime() == 0)
		{
			timer.CountStart();
			Work.player.Run();
			carrotManager.Play();
		}
		else
		{
			// 仮）フィーバー中は５秒間で元に戻るなど。
			if(Work.gauge.IsFever())
			{
				m_feverTimeCount += Time.deltaTime;
				if(m_feverTimeCount >= 5)
				{
					Work.gauge.ResetValue(false);
					m_feverTimeCount = 0.0f;
				}
			}


			// サンプル
			if(Input.GetKeyDown(KeyCode.X))
			{
				Work.gauge.AddValue(50);
			}

			// タイムアップ処理
			// いろいろなものを停止させる
			if(timer.IsTimeLimit())
			{
				Work.totalScore = Work.gameScore.GetScore();
				Work.player.Idle();
				Work.gameScore.Pause();
				Work.gauge.Pause();
				carrotManager.Stop();
				SetPhase((int)Phase.TimeUp);
			}
		}
	}

    /*
     * タイムアップ
     */
	protected void _TimeUp()
	{
		if( GetPhaseTime() == 0)
		{
			telop.Play(Telop.TelopType.GameEnd);
		}
		else if( GetPhaseTime() > 0)
		{
			if(!telop.IsPlaying())
			{
				SetPhase((int)Phase.FadeOut);
			}
		}
	}

    /*
     * フェードアウト
     */
	protected void _FadeOut()
	{
		if (GetPhaseTime() == 0)
		{
			Work.fade.Play(Fade.FadeType.Out, Fade.ColorType.Black, Fade.FadeSpeed.Norma);
		}
		else if (!Work.fade.IsPlaying())
		{
			Exit();
		}
	}
}
