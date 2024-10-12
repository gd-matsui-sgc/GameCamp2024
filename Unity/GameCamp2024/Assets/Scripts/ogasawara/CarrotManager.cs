using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
	[SerializeField] GameObject[] carrotArray;
	int count;
	GameObject carrots;



	// Start is called before the first frame update
	void Start()
    {
		count = 0;
		carrots = GameObject.Instantiate(carrotArray[count]) as GameObject;
	}


    

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.G))
		{
			CarrotSet();
		}
	}
	public void CarrotSet()
	{
		Destroy(carrots);
		count++;
		carrots = GameObject.Instantiate(carrotArray[count]) as GameObject;
		if (count == 2)
		{
			count = -1;
		}
	}
}
