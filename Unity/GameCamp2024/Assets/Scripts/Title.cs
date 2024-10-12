using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : BaseScene
{
    public enum Phase
    {
        FadeIn,
        Run,
        FadeOut,
    }

    /**
     * Updateの直前に呼ばれます
     */
    protected override void OnStart()
    {
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
    }

    /*
     * フェードイン
     */
    protected void _FadeIn()
    {   
        if(GetPhaseTime() == 0)
        {
            Work.fade.Play(Fade.FadeType.In, Fade.ColorType.Black, Fade.FadeSpeed.Norma );
        }
        else if(!Work.fade.IsPlaying())
        {
            SetPhase((int)Phase.Run);
        }
    }

    /*
     * 実行中（演出により分散）
     */
    protected void _Run()
    {
        if (Input.GetKey(KeyCode.Space))
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
            Work.fade.Play(Fade.FadeType.Out, Fade.ColorType.Black, Fade.FadeSpeed.Norma );
        }
        else if(!Work.fade.IsPlaying())
        {
            Exit();
        }
    }
    
}
