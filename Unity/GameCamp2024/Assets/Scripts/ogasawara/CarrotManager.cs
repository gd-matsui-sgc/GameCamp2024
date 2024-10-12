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

	[SerializeField] GameObject[] carrotArray;
	int count;
	GameObject carrots;

	private static readonly int CARROT_POOL_MAX = 30;

	[SerializeField]
	public GameObject carrotGameObject = null;

	// 人参リスト
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

		//count = 0;
		//carrots = GameObject.Instantiate(carrotArray[count]) as GameObject;
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
	public void CarrotSet(Vector3 position)
	{
		Carrot carrot = SearchIdlingCarrots();
		if(carrot != null)
		{
			carrot.Run(position);
		}
	}

    /*
     * 再生フェイズ
     */
	private void _Play()
	{
		if(GetPhaseTime() % 300 == 0)
		{
			float x = Random.Range(0, 20) - 10.0f;
			float z = Random.Range(0, 20) - 10.0f;
			CarrotSet(new Vector3(x, 0, z));
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
}
