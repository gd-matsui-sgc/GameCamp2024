using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRabbit : BaseScene
{
    public enum Phase
    {
        Idle,
        Run,
    }

    // Tween
    private Tween    m_positionTween     = null;

    // Tween
    private Tween    m_rotationTween     = null;


    /**
     * Updateの直前に呼ばれます
     */
    protected override void OnStart()
    {
        m_positionTween = gameObject.AddComponent<Tween>();
        m_rotationTween = gameObject.AddComponent<Tween>();

    }

    /**
     * 毎フレーム呼ばれます
     */
    protected override void OnUpdate()
    {
        switch((Phase)GetPhase())
        {
        case Phase.Idle:    _Idle();        break;
        case Phase.Run:     _Run();         break;
        }
        UpdatePositionAndRotation();
    }

    /*
     * 待機
     */
    protected void _Idle()
    {  
    }

    /*
     * 実行中
     */
    protected void _Run()
    {
        // if(GetPhaseTime() == 0 )
        // {
        //     m_rotationTween.Play(transform.localEulerAngles, new Vector3(0.0f, 0.0f, 0.0f), 5.0f, Tween.Mode.Linear);
        //     m_positionTween.Play(transform.localPosition,    new Vector3(-transform.localPosition.x * 1.5f, transform.localPosition.y, transform.localPosition.z), 2.0f, Tween.Mode.Linear);
        // }
    }

    public void Run()
    {
        SetPhase((int)Phase.Run);
    }

    private void UpdatePositionAndRotation()
    {
        if(m_positionTween.IsPlaying())
        {
            transform.localPosition = m_positionTween.GetProgress();
        }

        if(m_rotationTween.IsPlaying())
        {
            transform.localEulerAngles = m_rotationTween.GetProgress();
        }
    }
}
