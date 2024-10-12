using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed = 10f;
    
	
	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

        rb.AddForce(x * moveSpeed, 0, z * moveSpeed);

	}
}
