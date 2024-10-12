using System.Collections;
using System.Collections.Generic;
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

    // フェイズ
    private enum Phase
    {
        // 待機
        Idle,

        // 実行
        Run,

        // 削除
        Kill,
    }

    public enum CarrotGrow
    {
        FIRST_CARROT = 0,
        THREE_CARROT,
        MAX_CARROT,
        DEATH_CARROT
    }

    private CarrotGrow m_growCarrots;

    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
    {
        m_growCarrots = CarrotGrow.FIRST_CARROT;

        threeCarrots.SetActive(false);
        maxCarrots.SetActive(false);
        deathCarrots.SetActive(false);

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
        }
    }

    /*
     * 実行フェイズ
     */
	private void _Run()
    {
        m_growLimit -= Time.deltaTime;

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


        if (m_growLimit <= 0)
        {
            if (m_growCarrots == CarrotGrow.MAX_CARROT)
            {
                maxCarrots.SetActive(false);
                deathCarrots.SetActive(true);
            }
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
     * 待機フェイズにします
     */
	public void Idle()
    {
        m_growLimit = growLimit;

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
	public void Run(Vector3 position)
    {
        transform.localPosition = new Vector3(position.x, position.y, position.z);
        m_growLimit = growLimit;
        carrot.SetActive(true);

        SetPhase((int)Phase.Run);
    }

    /*
     * 実行フェイズかを判定
     * @returns 判定結果
     */
    public bool IsRunning()
    {
        return ((Phase)GetPhase() == Phase.Run);
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

            // ポイントを加算
            int point = 0;

            if(m_growCarrots == CarrotGrow.FIRST_CARROT)
            {
                point = 1;
            }
            else if (m_growCarrots == CarrotGrow.THREE_CARROT)
            {
                point = 3;
            }
            else if (m_growCarrots == CarrotGrow.MAX_CARROT)
            {
                point = 5;
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
        




