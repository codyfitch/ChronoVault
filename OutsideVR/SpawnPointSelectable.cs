using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EL.Dungeon; 

public class SpawnPointSelectable : SelectableObj
{
    public override void onSelect()
    {
        base.onSelect();
        this.GetComponent<Animator>().SetBool("in", true); 
    }

    public override void onUnselect()
    {
        base.onUnselect();
        this.GetComponent<Animator>().SetBool("in", false); 
    }

    public override void onUserClick()
    {
        base.onUserClick();

        if (UIManager.s.getSelectedMonster() != -1) // The user has selected a monster 
        {
            Monster monster = new Monster();
            monster.type = UIManager.s.getSelectedMonster();

            // adjust position for height difference of markers from actual spawn points 
            monster.pos = new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z);
            string json = JsonUtility.ToJson(monster);
            ServerConnectOutside.s.sendMonsterAdded(json);
            UIManager.s.deselectMonster(); 
        }
    }
}
