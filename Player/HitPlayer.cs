using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    private bool delay = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!delay) {
                delay = true;
                Debug.Log("Player Hit");
                playerHealth.hitCount++;
                playerHealth.playerHP -= 5;  
                Debug.Log("Player Health: " + playerHealth.playerHP);
                Debug.Log("Hit Count: " + playerHealth.hitCount);
                StartCoroutine(Delayed());
            }
        }
    }

    IEnumerator Delayed() 
    {
        yield return new WaitForSeconds(0.5f);
        delay = false;
	}
}
