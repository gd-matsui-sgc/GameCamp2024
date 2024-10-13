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

    [SerializeField]
    public TitleRabbit titleRabbit = null;

    [SerializeField]
    public GameObject button1 = null;

    [SerializeField]
    public GameObject button2 = null;

    // 選択されたボタンのインデックス
    private int m_selectButtonIndex = 0;

    /**
     * Updateの直前に呼ばれます
     */
    protected override void OnStart()
    {
        AcvtiveButton(0, true);
        AcvtiveButton(1, false);
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(m_selectButtonIndex != 0)
            {
                m_selectButtonIndex = 0;
                AcvtiveButton(0, true);
                AcvtiveButton(1, false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(m_selectButtonIndex != 1)
            {
                m_selectButtonIndex = 1;
                AcvtiveButton(0, false);
                AcvtiveButton(1, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Return))
        {
            SetPhase((int)Phase.FadeOut);
        }
    }
    
    void AcvtiveButton(int index, bool isActive)
    {
        if(index == 0)
        {
            foreach (UIFocus obj in button1.GetComponentsInChildren<UIFocus>(true))
            {
                obj.gameObject.SetActive(isActive);
            }
        }
        else
        {
            foreach (UIFocus obj in button2.GetComponentsInChildren<UIFocus>(true))
            {
                obj.gameObject.SetActive(isActive);
            }
        }
    }

    /*
     * フェードアウト
     */
    protected void _FadeOut()
    {
        if(GetPhaseTime() == 70)
        {
            Work.fade.Play(Fade.FadeType.Out, Fade.ColorType.Black, Fade.FadeSpeed.Fast );
        }
        else if(GetPhaseTime() > 70)
        {
            if(!Work.fade.IsPlaying())
            {
                SetExitCode(m_selectButtonIndex);
                Exit();
            }
        }
    }
    
}
