using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/**
 * シーン用ベースクラス
 */
public class BaseScene : Base
{
    // システムを初期化したか
    static bool s_systemInitialized = false;

    // イベントシステム
    [SerializeField]
    protected EventSystem eventSystem = null;

    // シーン上のカメラ
    [SerializeField]
    protected Camera    gameCamera = null;

    protected override void Awake()
    {
        if(!s_systemInitialized)
        {
            // FPSを60に設定
            Application.targetFrameRate = 60;

            // フェードを生成
            CreateFade();

        }

        base.Awake();
    }

    /**
     * Updateの直前に呼ばれる(Unity側)
     */
    protected override void Start()
    {
        if( Work.gameCamera == null )
        {
            Work.gameCamera = gameCamera;
        }
        else
        {
            DisableSceneCamera();
        }

        // イベントシステムは複数起動できないので１つにする
        if( eventSystem != null )
        {
            if( Work.eventSystem == null )
            {
                Work.eventSystem = eventSystem;            
            }
            else
            {
                eventSystem.gameObject.SetActive( false );
            }
        }

        base.Start();
    }

    /**
     * シーン上のカメラを有効にします
     */
    protected void EnableSceneCamera()
    {
        if(gameCamera != null)
        {
            gameCamera.gameObject.SetActive(true);
        }
    }

    /**
     * シーン上のカメラを無効にします
     */
    protected void DisableSceneCamera()
    {
        if(gameCamera != null)
        {
            gameCamera.gameObject.SetActive(false);
        }
    }

    /**
     * フェード生成
     */
    private void CreateFade()
    {
        if( Work.fade == null )
        {
            Object resFadeObject = Resources.Load("UI/Fade");
            if( resFadeObject )
            {
                GameObject fadeObject = ( GameObject )Instantiate(resFadeObject);
                if( fadeObject != null )
                {
                    Work.fade = fadeObject.GetComponent<Fade>();
                    if( Work.fade != null )
                    {
                        Work.fade.Play(Fade.FadeType.Blank);
                    }
                }
            }
        }
    }
}
