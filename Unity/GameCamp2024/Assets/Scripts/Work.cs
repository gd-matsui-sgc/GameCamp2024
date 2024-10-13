using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

static public class Work
{
    // フェード
    static public Fade          fade        = null;
    // EventSystem
    static public EventSystem   eventSystem = null;
    // カメラ
    static public Camera        gameCamera  = null;
    // エフェクトシステム
    static public EffectSystem  effectSystem = null;
    // スコア
    static public Score         gameScore = null;
    // プレイヤー
    static public Player        player    = null;
    // ゲージ
    static public Gauge         gauge = null;
    // トータルスコア
    static public int           totalScore = 0;
    // ハイスコアスコア
    static public int           highScore = 0;
      
}
