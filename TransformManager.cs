using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EL.Dungeon; 

public class TransformManager : MonoBehaviour
{
    public static TransformManager s;
    public DungeonData data;
    private CharacterTransforms chars;
    private RoomTransformInfo rooms;

    public GameObject player;

    private List<GameObject> monstersInGame;
    private List<GameObject> allMonsters; 

    private float sendCount; 
    private void Start()
    {
        s = this;
        chars = new CharacterTransforms();
        rooms = new RoomTransformInfo();
        monstersInGame = new List<GameObject>();
        allMonsters = data.sets[0].VRMonsters;
        sendCount = 0; 
    }

    private void Update()
    {
        sendCount += Time.deltaTime; 
        if (sendCount > 1f)
        {
            sendCount = 0;
            updatePlayerPos(player.transform.position);
            updateMonsterPos(); 
        }
    }

    public void serializeRooms(DungeonGenerator level)
    {
        List<DungeonRoom> roomTransforms = new List<DungeonRoom>(); 
        foreach (Transform child in level.gameObject.transform) {
            DungeonRoom room = new DungeonRoom();
            room.name = child.gameObject.name.Split('(')[0];
            if (room.name.ToLower().Contains("door")) continue; 
            room.pos = child.transform.position;
            room.rot = child.transform.rotation;
            room.scale = child.transform.localScale;
            roomTransforms.Add(room); 
        }
        rooms.roomTransforms = roomTransforms; 
        sendRooms();
    }

    public void updatePlayerPos(Vector3 pos)
    {
        chars.playerPosTransform = pos;
        chars.playerHitPoints = player.GetComponent<PlayerHealth>().getHealth(); 
        sendCharacters(); 
    }

    private void updateMonsterPos()
    {
        chars.monsterTransforms = new List<Monster>(); 
        foreach (GameObject monster in monstersInGame)
        {
            Monster newMonster = new Monster();
            newMonster.type = monster.GetComponent<EnemyStats>().type;
            newMonster.pos = monster.transform.position;
            newMonster.hitPoints = monster.GetComponent<EnemyStats>().hitPoints; 
            chars.monsterTransforms.Add(newMonster); 
        }
    }

    public void addMonster(string json)
    {
        Monster monster = JsonUtility.FromJson<Monster>(json);

        // Instantiate monster 
        GameObject monsterGO = Instantiate(allMonsters[monster.type], monster.pos, Quaternion.identity);
        monstersInGame.Add(monsterGO); 

        chars.monsterTransforms.Add(monster); 
    }

    public void removeMonster(GameObject monster)
    {
        monstersInGame.Remove(monster); 
    }

    // Sending info to server 
    public void sendCharacters()
    {
        string json = JsonUtility.ToJson(chars);
        ServerConnect.s.sendCharInfo(json); 
    }

    public void sendRooms()
    {
        string json = JsonUtility.ToJson(rooms); 
        ServerConnect.s.sendRoomInfo(json); 
    }
}
