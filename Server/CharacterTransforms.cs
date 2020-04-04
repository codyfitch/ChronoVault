using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterTransforms
{
    public List<Monster> monsterTransforms = new List<Monster>(); 
    public Vector3 playerPosTransform;
    public int playerHitPoints;
}

[System.Serializable]
public class Monster
{
    public int type;
    public Vector3 pos;
    public int hitPoints; 
    public bool isFighting; 
}
