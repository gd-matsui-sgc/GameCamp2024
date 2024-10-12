using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField] GameObject carrot;
    [SerializeField] GameObject threeCarrots;
    [SerializeField] GameObject maxCarrots;
    [SerializeField] GameObject deathCarrots;

    //êléQÇ™åÕÇÍÇÈÇ‹Ç≈ÇÃéûä‘
    [SerializeField] float growLimit = 15f;

    //ëÊàÍê¨í∑
    [SerializeField] float growTime = 10f;

    //ëÊìÒê¨í∑
    [SerializeField] float maxGrowTime = 5f;

    int score = 0;

    int totalScore = 0;

    enum carrotGrow
    {
        FIRST_CARROT = 0,
        THREE_CARROT,
        MAX_CARROT,
        DEATH_CARROT
    }

    private carrotGrow growCarrots;

    // Start is called before the first frame update
    void Start()
    {
        growCarrots = carrotGrow.FIRST_CARROT;

        threeCarrots.SetActive(false);
        maxCarrots.SetActive(false);
        deathCarrots.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
        Debug.Log("total : " + totalScore);

        growLimit -= Time.deltaTime;

        if (growLimit <= growTime)
        {
            if (growCarrots == carrotGrow.FIRST_CARROT)
            {
                growCarrots = carrotGrow.THREE_CARROT;
                carrot.SetActive(false);
                threeCarrots.SetActive(true);
            }
        }


        if (growLimit <= maxGrowTime)
        {
            if (growCarrots == carrotGrow.THREE_CARROT)
            {
                growCarrots = carrotGrow.MAX_CARROT;
                threeCarrots.SetActive(false);
                maxCarrots.SetActive(true);
            }
        }


        if (growLimit <= 0)
        {
            if (growCarrots == carrotGrow.MAX_CARROT)
            {
                maxCarrots.SetActive(false);
                deathCarrots.SetActive(true);
            }
        }
    }



    private void OnTriggerStay(Collider other)
    {

        if (other.name == "Player")
        {
            Debug.Log("Touch");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Get");
                
                

                if(growCarrots == carrotGrow.FIRST_CARROT)
                {
                    score += 1;
                }
                else if (growCarrots == carrotGrow.THREE_CARROT)
                {
                    score += 3;
                }
				else if (growCarrots == carrotGrow.MAX_CARROT)
				{
                    score += 5;
				}
				else if (growCarrots == carrotGrow.DEATH_CARROT)
				{
                    score -= 1;
				}

                totalScore += score;
				Destroy(gameObject);

			}
        }
    }
}
        




