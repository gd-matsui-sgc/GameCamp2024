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

	// 人参マネージャー
	[SerializeField]
	public CarrotManager carrotManager = null;

    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
	{
		Work.gameScore = score;
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
			player.Run();
			carrotManager.Play();
		}
		else
		{

			if(timer.IsTimeLimit())
			{
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
