using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SetStats : MonoBehaviour
{
    public Image monsterImage;
    public Slider slider; 
    
    public void updateImage(Sprite s)
    {
        monsterImage.sprite = s; 
    }

    public void updateSlider(float v)
    {
        slider.value = v; 
    }
}
