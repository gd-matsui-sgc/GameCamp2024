using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Carrot : Base
{
    [SerializeField] GameObject carrot;
    [SerializeField] GameObject threeCarrots;
    [SerializeField] GameObject maxCarrots;
    [SerializeField] GameObject deathCarrots;

    //人参が枯れるまでの時間
    [SerializeField] float growLimit = 15f;

    //第一成長
    [SerializeField] float growTime = 10f;

    //第二成長
    [SerializeField] float maxGrowTime = 5f;

    // 枯れるまでの制限（カウント用）
    private float m_growLimit = 15.0f;
    private float growAccelerate = 1;

    bool nowFever = false;

    // フェイズ
    private enum Phase
    {
        // 待機
        Idle,

        // 実行
        Run,

        // 削除
        Kill,

        // 仮)フィーバー
        Fever,
    }

    public enum CarrotGrow
    {
        FIRST_CARROT = 0,
        THREE_CARROT,
        MAX_CARROT,
        DEATH_CARROT
    }

    private CarrotGrow m_growCarrots;

    // 穴のインデックス番号(-1初期値)
    private int        m_holeIndex = -1;

    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
    {
        m_growCarrots = CarrotGrow.FIRST_CARROT;

        threeCarrots.SetActive(false);
        maxCarrots.SetActive(false);
        deathCarrots.SetActive(false);

        Idle();

    }

    /*
     * 毎フレーム呼ばれる
     */
	protected override void OnUpdate()
    {
        switch((Phase)GetPhase())
        {
        case Phase.Idle:                break;
        case Phase.Run:     _Run();     break;
        case Phase.Kill:    _Kill();    break;
        case Phase.Fever:   _Fever();   break;
        }
    }

    /*
     * 実行フェイズ
     */
	private void _Run()
    {
        UpdateGrow();
		// 仮）フィーバー中はキャラに吸い寄せられるなど。
		if (Work.gauge.IsFever())
        {
            SetPhase((int)Phase.Fever);
        }
    }

    /*
     * 削除フェイズ
     */
	private void _Kill()
    {
        // 演出があれば再生
        if(GetPhaseTime() == 0)
        {
            carrot.SetActive(false);
            threeCarrots.SetActive(false);
            maxCarrots.SetActive(false);
            deathCarrots.SetActive(false);
        }
        Idle();
    }

    /*
     * フィーバーフェイズ
     */
    private void _Fever()
    {
        // 仮）ここで属性かえてもよさそう
        if(GetPhaseTime() == 0)
        {
            
        }

		if (!Work.gauge.IsFever())
        {
            growAccelerate = 1;
            SetPhase((int)Phase.Run);
            return;
        }
		UpdateGrow();

		float followSpeed = 10.0f;

        //// ターゲットの方向を計算
        //Vector3 direction = (Work.player.transform.position - transform.position).normalized;

        //// オブジェクトをターゲットの方向に移動
        //transform.position = Vector3.MoveTowards(transform.position, Work.player.transform.position, followSpeed * Time.deltaTime);

        //// ターゲットの方向に向ける
        //transform.LookAt(Work.player.transform);

        // フィーバー中の人参の成長速度を上げる
        growAccelerate = 1.5f;
    }

	/*
     * フィーバーフェイズかを判定
     * @returns 判定結果
     */
	public bool IsFever()
	{
		return ((Phase)GetPhase() == Phase.Fever);
	}

	/*
     * 待機フェイズにします
     */
	public void Idle()
    {
        m_growLimit = growLimit;
        m_holeIndex  = -1;
        transform.localPosition = Vector3.one * 1000.0f;

        threeCarrots.SetActive(false);
        maxCarrots.SetActive(false);
        deathCarrots.SetActive(false);

        SetPhase((int)Phase.Idle);
    }

    /*
     * 待機フェイズかを判定
     * @returns 判定結果
     */
    public bool IsIdle()
    {
        return ((Phase)GetPhase() == Phase.Idle);
    }

    /*
     * 実行フェイズにします
     */
	public void Run(int holeIndex, Vector3 position)
    {
        transform.localPosition = new Vector3(position.x, position.y, position.z);
        m_growLimit = growLimit;
        carrot.SetActive(true);
        m_holeIndex = holeIndex;

        SetPhase((int)Phase.Run);
    }

    /*
     * 実行フェイズかを判定
     * @returns 判定結果
     */
    public bool IsRunning()
    {
        return ((Phase)GetPhase() == Phase.Run ||
                (Phase)GetPhase() == Phase.Fever );
    }

    /*
     * 削除フェイズにします
     */
	public void Kill()
    {
        SetPhase((int)Phase.Kill);
    }

    /*
     * 削除フェイズかを判定
     * @returns 判定結果
     */
    public bool IsKill()
    {
        return ((Phase)GetPhase() == Phase.Kill);
    }

    /*
     * 穴のインデックス番号を取得
     * @returns インデックス番号
     */
    public int GetHoleIndex()
    {
        return m_holeIndex;
    }

    private void UpdateGrow()
    {
		m_growLimit -= Time.deltaTime * growAccelerate;

		if (m_growLimit <= growTime)
		{
			if (m_growCarrots == CarrotGrow.FIRST_CARROT)
			{
				m_growCarrots = CarrotGrow.THREE_CARROT;
				carrot.SetActive(false);
				threeCarrots.SetActive(true);
			}
		}


		if (m_growLimit <= maxGrowTime)
		{
			if (m_growCarrots == CarrotGrow.THREE_CARROT)
			{
				m_growCarrots = CarrotGrow.MAX_CARROT;
				threeCarrots.SetActive(false);
				maxCarrots.SetActive(true);
			}
		}

        if (!IsFever())
        {
            if (m_growLimit <= 0)
            {
                if (m_growCarrots == CarrotGrow.MAX_CARROT)
                {
                    maxCarrots.SetActive(false);
                    deathCarrots.SetActive(true);
                }
            }
        }
	}


	/**
     * Collider が他のトリガーと当たり続けている毎フレーム呼び出される
     */
	private void OnTriggerStay(Collider other)
    {
        if(!IsRunning()){ return; }

        if (other.name.Equals("Player"))
        {
            // 削除する
            Kill();

            // エフェクト再生
            Work.effectSystem.Play(EffectSystem.EffectType.ItemGet, transform.position);

            // ポイントを加算
            int point = 0;

            if(m_growCarrots == CarrotGrow.FIRST_CARROT)
            {
                point = 1;
                Work.gauge.AddValue(5);
            }
            else if (m_growCarrots == CarrotGrow.THREE_CARROT)
            {
                point = 5;
                Work.gauge.AddValue(10);
            }
            else if (m_growCarrots == CarrotGrow.MAX_CARROT)
            {
                point = 10;
                Work.gauge.AddValue(20);
            }
            else if (m_growCarrots == CarrotGrow.DEATH_CARROT)
            {
                point = -1;
            }

            if(Work.gameScore != null)
            {
                Work.gameScore.AddScore(point);
            }
        }
    }
}
        




