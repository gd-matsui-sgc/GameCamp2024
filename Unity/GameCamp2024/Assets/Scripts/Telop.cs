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

    // テロップの種類
    public enum TelopType
    {
        // なし
        None,

        // ゲーム開始
        GameStart,

        // ゲーム終了
        GameEnd,
    }

    [SerializeField]
    public GameObject gameStartTelopGameObject = null;

    [SerializeField]
    public GameObject gameEndTelopGameObject   = null;

    // テロップのゲームオブジェクト
    private GameObject m_telopGameObject   = null;

    // テロップのアニメータ
    private Animator   m_telopAnimator      = null;


    // テロップの種類
    private TelopType m_telopType = TelopType.None;


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
        if(GetPhaseTime() == 0)
        {
            switch( m_telopType )
            {
            case TelopType.GameStart:
                {
                    m_telopGameObject = Instantiate(gameStartTelopGameObject, transform);
                }
                break;

            case TelopType.GameEnd:
                {
                    m_telopGameObject = Instantiate(gameEndTelopGameObject, transform);
                }
                break;
            }
            if( m_telopGameObject != null )
            {
                m_telopAnimator = m_telopGameObject.GetComponentInChildren<Animator>();
            }
        }
        else
        {
            if( m_telopAnimator!=null )
            {
                AnimatorStateInfo stateInfo = m_telopAnimator.GetCurrentAnimatorStateInfo(0);
                // 現在のアニメーションが "out" かどうかを確認
                if( stateInfo.IsName("out") )
                {
                    if( stateInfo.normalizedTime >= 1.0f )
                    {
                        if(m_telopGameObject != null)
                        {
                            Destroy(m_telopGameObject);
                            m_telopGameObject = null;
                        }
                        SetPhase((int)Phase.Idle);
                    }
                }
            }
            else
            {
                if(m_telopGameObject != null)
                {
                    Destroy(m_telopGameObject);
                    m_telopGameObject = null;
                }
                SetPhase((int)Phase.Idle);
            }
        }
    }

    /*
     * 再生
     * @param telopType    テロップの種類
     */
    public void Play(TelopType telopType)
    {
        m_telopType = telopType;
        SetPhase((int)Phase.Play);
    }

    /*
     * 再生中かを判定
     * @returns 判定結果
     */
    public bool IsPlaying()
    {
        return ((Phase)GetPhase() == Phase.Play);
    }
}
