using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : Base
{
    // エフェクトの種類
    public enum EffectType
    {
        // アイテム(人参)入手
        ItemGet,
        // 掘る
        Dig,
        // スコアA
        ScoreA,
        // スコアB
        ScoreB,
        // スコアC
        ScoreC,
        // スコアD
        ScoreD,
    }

    [SerializeField]
    public ParticleSystem itemGetParticleSystem = null;

    [SerializeField]
    public ParticleSystem digParticleSystem = null;

    [SerializeField]
    public ParticleSystem scoreAParticleSystem = null;

    [SerializeField]
    public ParticleSystem scoreBParticleSystem = null;

    [SerializeField]
    public ParticleSystem scoreCParticleSystem = null;

    [SerializeField]
    public ParticleSystem scoreDParticleSystem = null;


    public class EffectInfo
    {
        public ParticleSystem particleSystem = null;
        public GameObject     gameObject     = null;
    }

    // エフェクト情報のリスト
    private List<EffectInfo> m_effectInfoList = new List<EffectInfo>();

    /**
     * Updateの直前に呼ばれます
     */
    protected override void OnStart()
    {
        m_effectInfoList.Clear();
    }

    /**
     * 毎フレーム呼ばれます
     */
    protected override void OnUpdate()
    {   
        bool removeEffect = false;
        for(int i = 0; i < m_effectInfoList.Count; i++)
        {
            if(m_effectInfoList[i].particleSystem != null)
            {
                if(!m_effectInfoList[i].particleSystem.isPlaying)
                {
                    Destroy(m_effectInfoList[i].gameObject);
                    m_effectInfoList[i].particleSystem = null;
                    m_effectInfoList[i].gameObject = null;
                    removeEffect = true;
                }
            }
            else if(m_effectInfoList[i].gameObject != null)
            {
                Destroy(m_effectInfoList[i].gameObject);
                m_effectInfoList[i].particleSystem = null;
                m_effectInfoList[i].gameObject = null;
                removeEffect = true;
            }
        }
        if(removeEffect)
        {
            m_effectInfoList.RemoveAll( e => e.gameObject == null);
        }
    }

    public void StopAll()
    {
        for(int i = 0; i < m_effectInfoList.Count; i++)
        {
            if(m_effectInfoList[i].gameObject != null )
            {
                Destroy(m_effectInfoList[i].gameObject);
                m_effectInfoList[i].gameObject = null;
            }
        }
        m_effectInfoList.Clear();
    }

    /*
     * 再生
     * @param effectType    エフェクトの種類
     * @param position      座標
     * @param parent        親
     */
    public void Play(EffectType effectType, Vector3 position, Transform parent = null)
    {
        switch(effectType)
        {
        case EffectType.ItemGet:
            {
                EffectInfo info = new EffectInfo();
                info.gameObject = GameObject.Instantiate(itemGetParticleSystem.gameObject, position, new Quaternion(), parent);
                info.particleSystem = info.gameObject.GetComponent<ParticleSystem>();
                m_effectInfoList.Add(info);
            }
            break;

        case EffectType.Dig:
            {
                EffectInfo info = new EffectInfo();
                info.gameObject = GameObject.Instantiate(digParticleSystem.gameObject, position, new Quaternion(), parent);
                info.particleSystem = info.gameObject.GetComponent<ParticleSystem>();
                m_effectInfoList.Add(info);
            }
            break;

        case EffectType.ScoreA:
            {
                EffectInfo info = new EffectInfo();
                info.gameObject = GameObject.Instantiate(scoreAParticleSystem.gameObject, position, new Quaternion(), parent);
                info.particleSystem = info.gameObject.GetComponent<ParticleSystem>();
                m_effectInfoList.Add(info);
            }
            break;

        case EffectType.ScoreB:
            {
                EffectInfo info = new EffectInfo();
                info.gameObject = GameObject.Instantiate(scoreBParticleSystem.gameObject, position, new Quaternion(), parent);
                info.particleSystem = info.gameObject.GetComponent<ParticleSystem>();
                m_effectInfoList.Add(info);
            }
            break;

        case EffectType.ScoreC:
            {
                EffectInfo info = new EffectInfo();
                info.gameObject = GameObject.Instantiate(scoreCParticleSystem.gameObject, position, new Quaternion(), parent);
                info.particleSystem = info.gameObject.GetComponent<ParticleSystem>();
                m_effectInfoList.Add(info);
            }
            break;

        case EffectType.ScoreD:
            {
                EffectInfo info = new EffectInfo();
                info.gameObject = GameObject.Instantiate(scoreDParticleSystem.gameObject, position, new Quaternion(), parent);
                info.particleSystem = info.gameObject.GetComponent<ParticleSystem>();
                m_effectInfoList.Add(info);
            }
            break;

        }
    }

}
