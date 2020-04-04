using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int swordStrength;
    public int head;
    public int torso;
    public int limbs;
    EnemyStats enemyStats;
    

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //If head hit
        if (other.tag == "Head")
        {
            //If head hitbox is added
        }

        //If torso hit
        if (other.tag == "Torso")
        {
            //If torso hitbox is added
        }

        //If limbs hit
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit enemy");
            enemyStats = other.GetComponent<EnemyStats>();
            enemyStats.hitPoints -= swordStrength;
            enemyStats.SetState(new HitState(enemyStats));
            Debug.Log("Hitpoints: " + enemyStats.hitPoints);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            StartCoroutine(StopHaptics());
        }
    }

    IEnumerator StopHaptics()
    {
        yield return new WaitForSeconds(0.2f);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
