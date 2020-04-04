using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EL.Dungeon;
using UnityEngine.UI; 

public class RoomReconstructor : MonoBehaviour
{
    public static RoomReconstructor s;

    public DungeonData data;

    public GameObject player;

    private GameObject[] spawnPoints;
    public List<GameObject> dungeonRooms;

    public GameObject spawnPointMarkerPrefab;
    public GameObject monsterMarkerPrefab; 
    public Material selectionPlaneMaterial;

    private List<GameObject> monsterTransforms; 

    private void Awake()
    {
        s = this;
        monsterTransforms = new List<GameObject>(); 
        dungeonRooms = new List<GameObject>(); 
    }

    public void reconstruct(string json)
    {
        RoomTransformInfo rooms = JsonUtility.FromJson<RoomTransformInfo>(json);
        float maxZ = int.MinValue;
        float maxX = int.MinValue;
        float minZ = int.MaxValue;
        float minX = int.MaxValue; 
        foreach (DungeonRoom room in rooms.roomTransforms)
        {
            string roomName = room.name;
            GameObject prefabToInstantiate = null; 
            for (int i = 0; i < data.sets[0].roomTemplates.Count; i++)
            {
                if (data.sets[0].roomTemplates[i].name.Equals(roomName))
                {
                    prefabToInstantiate = data.sets[0].roomTemplates[i].gameObject;
                    break; 
                }
            }
            if (prefabToInstantiate == null)
            {
                Debug.Log("tried to instantiate nonexistant room");
                return;
            }
            GameObject go = Instantiate(prefabToInstantiate);
            go.transform.position = room.pos;
            go.transform.rotation = room.rot;
            go.transform.localScale = room.scale;
            if (go.transform.position.x > maxX) maxX = go.transform.position.x;
            if (go.transform.position.z > maxZ) maxZ = go.transform.position.z;
            if (go.transform.position.x < minX) minX = go.transform.position.x;
            if (go.transform.position.z < minZ) minZ = go.transform.position.z; 
            if (!go.name.ToLower().Contains("hallway") && !go.name.ToLower().Contains("small"))
            {
                dungeonRooms.Add(go);
                createSelectionPlane(go); 
            }
            UIManager.s.changeMessageText("PLACE MONSTERS"); 
        }
        GameObject[] roofs = GameObject.FindGameObjectsWithTag("roof"); 
        foreach (GameObject g in roofs) {
            g.SetActive(false); 
        }
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint"); 
        foreach (GameObject s in spawnPoints)
        {
            GameObject spawnMarker = GameObject.Instantiate(spawnPointMarkerPrefab); 
            spawnMarker.transform.position = new Vector3(s.transform.position.x, s.transform.position.y + 10, s.transform.position.z);
        }
        CameraMove.s.setBasePosition(minX, maxX, minZ, maxZ); // also moves camera to base position 
        UIManager.s.getSpawnPoints(); 
    }

    private void createSelectionPlane(GameObject go)
    {
        RoomBound[] roomBounds = go.GetComponentsInChildren<RoomBound>();
        if (roomBounds.Length != 4)
        {
            Debug.LogError("One of your rooms is missing bounds");
            return;
        }
        GameObject selectionPlane = GameObject.CreatePrimitive(PrimitiveType.Quad);
        var mf = selectionPlane.GetComponent<MeshFilter>();

        var mesh = new Mesh();
        mf.mesh = mesh;

        Vector3[] planeVertices = { roomBounds[0].transform.position, roomBounds[1].transform.position, roomBounds[2].transform.position, roomBounds[3].transform.position };

        mesh.vertices = planeVertices;
        var tris = new int[6]
        {
           // lower left triangle
           0, 2, 1,
           // upper right triangle
           2, 3, 1
        };
        mesh.triangles = tris;
        var normals = new Vector3[4]
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };
        mesh.normals = normals;
        var uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;
        selectionPlane.transform.position = new Vector3(selectionPlane.transform.position.x, selectionPlane.transform.position.y + 20, selectionPlane.transform.position.z);
        selectionPlane.GetComponent<Renderer>().material = selectionPlaneMaterial;
        selectionPlane.AddComponent<RoomPlaneSelectable>();
        selectionPlane.AddComponent<BoxCollider>();
        selectionPlane.transform.parent = go.transform; 
    }

    public void updatePlayerPosition(string json)
    {
        CharacterTransforms character = JsonUtility.FromJson<CharacterTransforms>(json);
        player.transform.position = new Vector3(character.playerPosTransform.x, character.playerPosTransform.y + 10, character.playerPosTransform.z);
        player.GetComponent<SetStats>().updateSlider(character.playerHitPoints / 100.0f); 

        if (monsterTransforms.Count > character.monsterTransforms.Count)
        {
            for (int i = character.monsterTransforms.Count; i < monsterTransforms.Count; i++)
            {
                monsterTransforms.RemoveAt(0); 
            }
        } else if (monsterTransforms.Count < character.monsterTransforms.Count)
        {
            int count = monsterTransforms.Count; 
            for (int i = count; i < character.monsterTransforms.Count; i++)
            {
                GameObject newMonsterMarker = Instantiate(monsterMarkerPrefab);
                monsterTransforms.Add(newMonsterMarker);
            }
        }

        for (int i = 0; i < character.monsterTransforms.Count; i++)
        {
            monsterTransforms[i].transform.position = new Vector3(character.monsterTransforms[i].pos.x, character.monsterTransforms[i].pos.y + 10, character.monsterTransforms[i].pos.z);
            monsterTransforms[i].GetComponent<SetStats>().updateImage(data.sets[0].monsterHeadshots[character.monsterTransforms[i].type]);
            monsterTransforms[i].GetComponent<SetStats>().updateSlider(character.monsterTransforms[i].hitPoints / 100.0f);
        }
    }
}
