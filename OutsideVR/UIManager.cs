using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EL.Dungeon;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    public GameObject menuPrefab; 
    public static UIManager s;
    public Text buttonText;
    public Image monsterImage;
    public Text monsterSpeedText;
    public Text monsterDamageText; 
    private List<GameObject> monsters;
    private List<Sprite> monsterImages;
    public Text messageText;
    public GameObject bottomUI; 

    private GameObject curMonsterGO = null; 
    private int curMonsterIndex;
    private int selectedMonster = -1;

    public SpawnPointSelectable[] spawnPoints;

    private string oldMessageText = ""; 
    
    void Start()
    {
        s = this;
        DungeonData data = RoomReconstructor.s.data;
        spawnPoints = new SpawnPointSelectable[0];
        monsters = data.sets[0].monsters;
        monsterImages = data.sets[0].monsterHeadshots; 
        changeMonsterIndex(0);
        changeMessageText("WAITING FOR CONNECTION");
        hideZoomedInUI(); 
    }

    public void showZoomedInUI()
    {
        bottomUI.GetComponent<Animator>().SetBool("in", true);
    }

    public void hideZoomedInUI()
    {
        deselectMonster();
        bottomUI.GetComponent<Animator>().SetBool("in", false);
    }

    public void changeMessageText(string s)
    {
        messageText.text = s; 
    }

    public void changeMessageText(string s, float duration)
    {
        oldMessageText = messageText.text;
        messageText.text = s;
        messageText.color = Color.red; 
        Invoke("returnMessageText", duration); 
    }

    public void returnMessageText()
    {
        messageText.text = oldMessageText;
        messageText.color = Color.white; 
    }

    public void openMenu(GameObject parent) // Obsolete 
    {
        GameObject menu = Instantiate(menuPrefab, parent.transform);
        menu.transform.localPosition = Vector3.zero; 
    }

    public void changeLeft()
    {
        if (curMonsterIndex <= 0)
        {
            changeMonsterIndex(monsters.Count - 1); 
        } else
        {
            changeMonsterIndex(curMonsterIndex - 1); 
        }
    }
    public void changeRight()
    {
        if (curMonsterIndex >= (monsters.Count - 1))
        {
            changeMonsterIndex(0); 
        } else
        {
            changeMonsterIndex(curMonsterIndex + 1); 
        }
    }

    public void createMonster()
    {
        if (selectedMonster == -1)
        {
            GameObject newMonster = Instantiate(monsters[curMonsterIndex]);
            newMonster.transform.rotation = Quaternion.Euler(0, 180, 0);
            newMonster.AddComponent<MouseFollow>();
            selectedMonster = curMonsterIndex;
            curMonsterGO = newMonster;
            showSpawnPoints(); 
        } else
        {
            deselectMonster(); 
        }
        
    }

    public void deselectMonster()
    {
        Destroy(curMonsterGO);
        hideSpawnPoints(); 
        selectedMonster = -1; 
    }

    public int getSelectedMonster()
    {
        return selectedMonster; 
    }

    private void changeMonsterIndex(int index)
    {
        curMonsterIndex = index;
        monsterImage.sprite = monsterImages[index]; 
        buttonText.text = RoomReconstructor.s.data.sets[0].VRMonsters[index].name.ToUpper();
        monsterSpeedText.text = RoomReconstructor.s.data.sets[0].VRMonsters[index].GetComponent<EnemyStats>().speed.ToString();
        monsterDamageText.text = RoomReconstructor.s.data.sets[0].VRMonsters[index].GetComponent<EnemyStats>().damage.ToString();
    }

    public void getSpawnPoints()
    {
        spawnPoints = Object.FindObjectsOfType<SpawnPointSelectable>();
        hideSpawnPoints(); 
    }

    private void hideSpawnPoints()
    {
        foreach (SpawnPointSelectable s in spawnPoints)
        {
           s.gameObject.SetActive(false); 
        }
    }

    private void showSpawnPoints()
    {
        foreach (SpawnPointSelectable s in spawnPoints)
        {
            s.gameObject.SetActive(true);
        }
    }
}
