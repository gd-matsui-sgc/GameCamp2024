using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する
using TMPro;

/*
 * テロップ用クラス
 */
public class Telop : Base
{
	// フェーズ
	public enum Phase
	{
		// 待機
		Idle,

		// 再生
		Play,
	}

    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
    {

    }

    /*
     * 毎フレーム呼ばれる
     */
	protected override void OnUpdate()
    {
		switch((Phase)GetPhase())
		{
		case Phase.Idle:				break;
		case Phase.Play:	_Play();	break;
		}
	}

    /*
     * 再生フェイズ
     */
    private void _Play()
    {

    }

}
