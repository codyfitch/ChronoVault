using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomTransformInfo
{
    public int numRooms;
    public List<DungeonRoom> roomTransforms = new List<DungeonRoom>();
}
