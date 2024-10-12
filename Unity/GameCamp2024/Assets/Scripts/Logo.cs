using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : Base
{
    public enum Phase
    {
        FadeIn,
        Run,
        FadeOut,
    }


    // Start is called before the first frame update
    protected override void OnStart()
    {
        
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        switch((Phase)GetPhase())
        {
        case Phase.FadeIn:  _FadeIn();      break;
        case Phase.Run:     _Run();         break;
        case Phase.FadeOut: _FadeOut();     break;
        }
    }

    /**
    *
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

    protected void _Run()
    {
        if(GetPhaseTime() == 60*10 )
        {
            SetPhase((int)Phase.FadeOut);
        }
    }

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
