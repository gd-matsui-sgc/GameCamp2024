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

    // 移動速度
    [SerializeField]
    public float moveSpeed = 10f;

    // 回転速度
    [SerializeField] 
    public float rotationSpeed = 10.0f;
    
	
    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        transform.localPosition = Vector3.zero;
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

        // 入力に基づいた移動方向を計算
        Vector3 moveDirection = new Vector3(x, 0, z).normalized;

        // 移動方向に力を加える
        if (moveDirection != Vector3.zero) // 入力がある場合のみ実行
        {
        // 力を加えて移動
        m_rigidbody.AddForce(moveDirection * moveSpeed, ForceMode.Force);

        // 移動方向に向かせる（徐々に回転）
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //m_rigidbody.AddForce(x * moveSpeed, 0, z * moveSpeed, ForceMode.Force);
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
