using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : Base
{
    public enum Phase
    {
        FadeIn,
        Run,
        FadeOut,
    }

    [SerializeField]
    public Image image = null;

    // Tween
    private Tween    m_alphaTween     = null;

    /**
     * Updateの直前に呼ばれます
     */
    protected override void OnStart()
    {
        m_alphaTween = gameObject.AddComponent<Tween>();
    }

    /**
     * 毎フレーム呼ばれます
     */
    protected override void OnUpdate()
    {
        switch((Phase)GetPhase())
        {
        case Phase.FadeIn:  _FadeIn();      break;
        case Phase.Run:     _Run();         break;
        case Phase.FadeOut: _FadeOut();     break;
        }
        if(image != null)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, m_alphaTween.GetProgress().x);
        }
    }

    /*
     * フェードイン
     */
    protected void _FadeIn()
    {   
        if(GetPhaseTime() == 0)
        {
            m_alphaTween.Play(Vector3.zero, Vector3.one, 1.0f, Tween.Mode.Sub);
        }
        else if(!m_alphaTween.IsPlaying())
        {
            SetPhase((int)Phase.Run);
        }
    }

    protected void _Run()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Return))
        {
            SetPhase((int)Phase.FadeOut);
        }
    }

    /*
     * フェードアウト
     */
    protected void _FadeOut()
    {
        if(GetPhaseTime() == 0)
        {
            m_alphaTween.Play(Vector3.one, Vector3.zero, 1.0f, Tween.Mode.Add);
        }
        else if(!m_alphaTween.IsPlaying())
        {
            Exit();
        }
    }
}
