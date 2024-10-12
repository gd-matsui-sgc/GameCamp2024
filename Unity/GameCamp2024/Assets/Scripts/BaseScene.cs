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
    // イベントシステム
    [SerializeField]
    protected EventSystem eventSystem = null;

    // シーン上のカメラ
    [SerializeField]
    protected Camera    gameCamera = null;

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

}
