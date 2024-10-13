using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Main : BaseScene
{
    // フェイズ
    public enum Phase
    {
        Logo,
        Title,
        Game,
        Result,
        Test_Ogasawara,
        Test_Nohara,
        Test_Matsui,       
    };

    // シーン関連
    private Logo   m_logo       = null;     // ロゴ
    private Title   m_title     = null;     // タイトル
    private Game    m_game      = null;     // ゲーム
    private Result  m_result    = null;     // リザルト
    private Test_Ogasawara  m_testOgasawara = null;
    private Test_Nohara     m_testNohara = null;
    private Test_Matsui     m_testMatsui = null;

    [SerializeField]
    private Phase m_startupPhase = Phase.Logo;  // 開始フェーズ

    /**
     * 生成時に呼ばれる(Unity側)
     */
    protected override void OnAwake()
    {

    }

    /**
     * Updateの直前に呼ばれる
     */
    protected override void OnStart()
    {
        SetPhase((int)m_startupPhase);
    }

    /*
     * 毎フレーム呼ばれる
     */
    protected override void OnUpdate()
    {
        if(GetPhaseTime() == 0 )
        {
            Debug.Log("CurrenPhase = " + (Phase)GetPhase());
        }
        switch((Phase)GetPhase())
        {
        case Phase.Logo:    _Logo();    break;
        case Phase.Title:   _Title();   break;
        case Phase.Game:    _Game();    break;
        case Phase.Result:  _Result();  break;
        case Phase.Test_Ogasawara:  _TestOgasawara();  break;
        case Phase.Test_Nohara:     _TestNohara();  break;
        case Phase.Test_Matsui:     _TestMatsui();  break;
        }
    }

    /**
     * ロゴ
     */
    private void _Logo()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateLogo");
        }
        else if( m_logo != null)
        {
            if( m_logo.IsExited())
            {
                m_logo = null;
                SceneManager.UnloadSceneAsync("Logo");
                SetPhase(  ( int )Phase.Title );
            }
        }
    }

    /**
     * タイトル
     */
    private void _Title()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateTitle");
        }
        else if( m_title != null)
        {
            if( m_title.IsExited())
            {
                if(m_title.GetExitCode() == 0)
                {
                    m_title = null;
                    SceneManager.UnloadSceneAsync("Title");
                    SetPhase(  ( int )Phase.Game );
                }
                else
                {
                    m_title = null;
                    Application.Quit();
                }
            }
        }
    }

    /**
     * ゲーム
     */
    private void _Game()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateGame");
        }
        else if( m_game != null)
        {
            if( m_game.IsExited())
            {
                m_game = null;
                SceneManager.UnloadSceneAsync("Game");
                SetPhase(  ( int )Phase.Result );
            }
        }
    }

    /**
     * リザルト
     */
    private void _Result()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateResult");
        }
        else if( m_result != null)
        {
            if( m_result.IsExited())
            {
                m_result = null;
                SceneManager.UnloadSceneAsync("Result");
                SetPhase(  ( int )Phase.Title );
            }
        }
    }

    /**
     * 小笠原さん用テスト
     */
    private void _TestOgasawara()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateTestOgasawara");
        }
        else if( m_testOgasawara != null)
        {
            if( m_testOgasawara.IsExited())
            {
                m_testOgasawara = null;
                SceneManager.UnloadSceneAsync("Test_Ogasawara");
                SetPhase(  ( int )Phase.Title );
            }
        }
    }

    /**
     * 野原さん用テスト
     */
    private void _TestNohara()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateTestNohara");
        }
        else if( m_testNohara != null)
        {
            if( m_testNohara.IsExited())
            {
                m_testNohara = null;
                SceneManager.UnloadSceneAsync("Test_Nohara");
                SetPhase(  ( int )Phase.Title );
            }
        }
    }

    /**
     * 松井用テスト
     */
    private void _TestMatsui()
    {
        if( GetPhaseTime() == 0 )
        {
            StartCoroutine("CreateTestMatsui");
        }
        else if( m_testMatsui != null)
        {
            if( m_testMatsui.IsExited())
            {
                m_testMatsui = null;
                SceneManager.UnloadSceneAsync("Test_Matsui");
                SetPhase(  ( int )Phase.Title );
            }
        }
    }


    /**
     * ロゴ生成のコルーチン
     */
    IEnumerator CreateLogo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "Logo", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject logo = GameObject.Find("Logo");
        m_logo = logo?.GetComponent<Logo>();
    }

    /**
     * タイトル生成のコルーチン
     */
    IEnumerator CreateTitle()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "Title", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject title = GameObject.Find("Title");
        m_title = title?.GetComponent<Title>();
    }

    /**
     * ゲーム生成のコルーチン
     */
    IEnumerator CreateGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "Game", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject game = GameObject.Find("Game");
        m_game = game?.GetComponent<Game>();
    }

    /**
     * リザルト生成のコルーチン
     */
    IEnumerator CreateResult()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "Result", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject result = GameObject.Find("Result");
        m_result = result?.GetComponent<Result>();
    }

    /**
     * 小笠原さん用テスト生成のコルーチン
     */
    IEnumerator CreateTestOgasawara()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "Test_Ogasawara", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject testOgasawara = GameObject.Find("Test_Ogasawara");
        m_testOgasawara = testOgasawara?.GetComponent<Test_Ogasawara>();
    }

    /**
     * 野原さん用テスト生成のコルーチン
     */
    IEnumerator CreateTestNohara()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "Test_Nohara", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject testNohara = GameObject.Find("Test_Nohara");
        m_testNohara = testNohara?.GetComponent<Test_Nohara>();
    }

    /**
     * 松井用テスト生成のコルーチン
     */
    IEnumerator CreateTestMatsui()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync( "Test_Matsui", LoadSceneMode.Additive );
        yield return null;
        while(!operation.isDone)
        {
            yield return null;
        }

        GameObject testMatsui = GameObject.Find("Test_Matsui");
        m_testMatsui = testMatsui?.GetComponent<Test_Matsui>();
    }



}





