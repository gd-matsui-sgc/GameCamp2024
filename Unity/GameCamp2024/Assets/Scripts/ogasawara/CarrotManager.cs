using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotManager : Base
{

	// フェーズ
	public enum Phase
	{
		// 待機
		Idle,

		// 再生
		Play,

		// 停止
		Stop,
	}

	private static readonly int CARROT_POOL_MAX = 30;

	[SerializeField]
	public GameObject carrotGameObject = null;

	[SerializeField]
	public List<GameObject> holeHookObjectList = new List<GameObject>();

	// 穴の情報
	public class HoleInfo
	{
		// コンストラクタ
		public HoleInfo(int _index)
		{
			index = _index;
		}

		// 穴の番号（変更予定ないので書き込み禁止）
		public readonly int index    = 0;
		// 穴の位置
		public Vector3  position = Vector3.zero;
		// 人参が空っぽか
		public bool     isEmpty = true;
		// 生成カウンタ
		public int      counter = 0;
	}

	// 穴の位置情報のリスト
	private List<HoleInfo>	m_holeInfoList = new List<HoleInfo>();

	// 人参リスト（プール）
	List<Carrot>    m_carrotList = new List<Carrot>();

    /**
     * Updateの直前に呼ばれる
     */
	protected override void OnStart()
    {
		// 人参のモデルを生成しておく
		for(int i = 0 ; i < CARROT_POOL_MAX; i++)
		{
			GameObject carrotObject = GameObject.Instantiate(carrotGameObject, transform) as GameObject;
			Carrot     carrot = carrotObject.GetComponent<Carrot>();
			m_carrotList.Add(carrot);
		}

		// ToDo
		// Gameシーンに穴のGameObjectを配置して、ここで取得すると座標も個数もわかってよさそう
		// 今はいったん強制的にランダムで入れてます。
		for(int i = 0; i < holeHookObjectList.Count; i++)
		{
			HoleInfo holeInfo = new HoleInfo(i);
			holeInfo.isEmpty = false;
			holeInfo.counter = Random.Range(1 * 60, 5 * 60);
			holeInfo.position = new Vector3(holeHookObjectList[i].transform.localPosition.x, 0.0f, holeHookObjectList[i].transform.localPosition.z);
			m_holeInfoList.Add(holeInfo);
		}
	}

    /*
     * 毎フレーム呼ばれる
     */
	protected override void OnUpdate()
    {
		switch((Phase)GetPhase())
		{
		case Phase.Idle:				break;
		case Phase.Play:	_Play();	break;
		}
	}

    /*
     * 人参をセット
     */
	public void CarrotSet(int holeIndex, Vector3 position)
	{
		Carrot carrot = SearchIdlingCarrots();
		if(carrot != null)
		{
			if(!Work.gauge.IsFever())
			{
				carrot.Run(holeIndex, position);
			}
			else
			{
				carrot.Fever(holeIndex, position);
			}
			Work.effectSystem.Play(EffectSystem.EffectType.Dig,position);
		}
	}

    /*
     * 再生フェイズ
     */
	private void _Play()
	{
		// ToDo:生成タイミングを何かで制御したらよさそう
		if(GetPhaseTime() % 30 == 0)
		{
			HoleInfo holeInfo = SearchEmptyHoleInfo(true);
			if(holeInfo != null)
			{
				m_holeInfoList[holeInfo.index].isEmpty = false;
				CarrotSet(holeInfo.index, new Vector3(holeInfo.position.x, 0, holeInfo.position.z));
			}
		}

		// 重い。美しくない書き方。でも時間がないのでやむをえず。
		foreach(HoleInfo holeInfo in m_holeInfoList)
		{
			if(!holeInfo.isEmpty)
			{
				// 存在していない場合は空っぽにする
				Carrot carrot = SearchCarrotsByHoleIndex(holeInfo.index);
				if(!carrot)
				{
					holeInfo.counter++;
					if(holeInfo.counter >= 5 * 60)
					{
						holeInfo.counter = 0;
						holeInfo.isEmpty = true;
					}
				}
			}
		}
	}

    /*
     * 再生
     */
	public void Play()
	{
		SetPhase((int)Phase.Play);
	}

    /*
     * 停止
     */
	public void Stop()
	{
		SetPhase((int)Phase.Idle);
	}

    /*
     * 待機中の人参を検索
	 * @returns 見つかった人参
     */
	public Carrot SearchIdlingCarrots()
	{
		for(int i = 0 ; i < CARROT_POOL_MAX; i++)
		{
			if(m_carrotList[i].IsIdle())
			{
				return m_carrotList[i];
			}
		}
		return null;
	}

    /*
     * 人参を穴のインデックスで検索
	 * @param holeIndex    穴のインデックス
	 * @returns 見つかった人参
     */
	public Carrot SearchCarrotsByHoleIndex(int holeIndex)
	{
		for(int i = 0 ; i < CARROT_POOL_MAX; i++)
		{
			if(m_carrotList[i].GetHoleIndex() == holeIndex )
			{
				return m_carrotList[i];
			}
		}
		return null;
	}


    /*
     * 空っぽの穴を検索
	 * @param   isRandom    ランダムで抽選するか
	 * @returns 見つかった穴情報
     */
	public HoleInfo SearchEmptyHoleInfo(bool isRandom)
	{
		if(isRandom)
		{
			List<HoleInfo> emptyHoleInfoList = new List<HoleInfo>();
			for(int i = 0 ; i < m_holeInfoList.Count; i++)
			{
				if(m_holeInfoList[i].isEmpty)
				{
					emptyHoleInfoList.Add(m_holeInfoList[i]);
				}
			}
			if(emptyHoleInfoList.Count > 0 )
			{
				int index = Random.Range(0, emptyHoleInfoList.Count);
				return emptyHoleInfoList[index];
			}
		}
		else
		{
			for(int i = 0 ; i < m_holeInfoList.Count; i++)
			{
				if(m_holeInfoList[i].isEmpty)
				{
					return m_holeInfoList[i];
				}
			}
		}
		return null;		
	}
}
