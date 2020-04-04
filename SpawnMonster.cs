using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject ogre;

    private void Start()
    {
        Instantiate(ogre, transform.position, transform.rotation);
        Debug.Log("Ogre Spawn");
    }
}
