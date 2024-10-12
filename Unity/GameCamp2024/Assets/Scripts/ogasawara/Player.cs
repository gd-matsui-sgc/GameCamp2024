using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Base
{

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

    private Rigidbody m_rigidbody = null;

    [SerializeField] float moveSpeed = 10f;
    
	
    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
    {
        m_rigidbody = GetComponent<Rigidbody>();
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
        float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

        m_rigidbody.AddForce(x * moveSpeed, 0, z * moveSpeed);
    }

    /*
     * 削除フェイズ
     */
	private void _Kill()
    {
        // 演出があれば再生
        if(GetPhaseTime() == 0)
        {
            m_rigidbody.Sleep();
        }
        Idle();
    }

    /*
     * 待機フェイズにします
     */
	public void Idle()
    {
        m_rigidbody.Sleep();
        transform.localPosition = Vector3.zero;
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
	public void Run()
    {
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


}
