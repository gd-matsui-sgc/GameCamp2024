using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Main : BaseScene
{
    public enum Phase
    {
        Logo,
        Title,
        Game,
        Result,
    };

    private Logo   m_logo = null;

    private Title   m_title = null;
    private Game    m_game = null;
    private Result  m_result = null;





    // Start is called before the first frame update
    protected override void OnStart()
    {
        SetPhase((int)Phase.Title);
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        Debug.Log(" " + GetPhaseTime());
        switch((Phase)GetPhase())
        {
        case Phase.Logo:    _Logo();    break;
        case Phase.Title:   _Title();   break;
        case Phase.Game:    _Game();    break;
        case Phase.Result:  _Result();  break;
        }
    }

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
                m_title = null;
                SceneManager.UnloadSceneAsync("Title");
                SetPhase(  ( int )Phase.Game );
            }
        }
    }

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
        if(m_logo != null)
        {
        }
        Debug.Log("LOGOOOO");
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

}





