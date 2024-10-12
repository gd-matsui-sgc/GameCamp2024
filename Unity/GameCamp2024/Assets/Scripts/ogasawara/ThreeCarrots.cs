using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeCarrots : MonoBehaviour
{
	
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	private void OnTriggerStay(Collider other)
	{
		if (other.name == "Player")
		{
			Debug.Log("Touch");
			if (Input.GetKeyDown(KeyCode.F))
			{
				Debug.Log("ThreeGet");
				Destroy(gameObject);
			}
		}
	}
	
}
