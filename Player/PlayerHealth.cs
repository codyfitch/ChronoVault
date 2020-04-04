using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerHP = 100;
    public int hitCount = 0;
    public GameObject[] splatters;
    Animator fadeBlack;
    public GameObject blackScreen;

    void Start()
    {
        fadeBlack = blackScreen.GetComponent<Animator>();
	}

    public int getHealth()
    {
        return playerHP; 
    }

    private void Update()
    {
        if(hitCount == 1) 
        {
            Debug.Log("Splatter 1");
            splatters[0].SetActive(true);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            StartCoroutine(StopHaptics());
        }

        if(hitCount == 2) 
        {
            Debug.Log("Splatter 2");
            splatters[0].SetActive(false);
            splatters[1].SetActive(true);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            StartCoroutine(StopHaptics());
        }
        
        if(hitCount == 3)
        {
            Debug.Log("Splatter 3");
            splatters[1].SetActive(false);
            splatters[2].SetActive(true);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            StartCoroutine(StopHaptics());
        }

        if(hitCount == 4) 
        {
            Debug.Log("Splatter 4");
            splatters[2].SetActive(false);
            splatters[3].SetActive(true);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            StartCoroutine(StopHaptics());
        }

        if(hitCount == 5)
        {
            Debug.Log("Splatter 5");
            splatters[3].SetActive(false);
            splatters[4].SetActive(true);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            StartCoroutine(StopHaptics());
        }

        if(hitCount > 5)
        {
            //Player dead, splatter then fade
            Debug.Log("Death Splatter");
            splatters[4].SetActive(false);
            splatters[5].SetActive(true);
            fadeBlack.SetBool("FadeOut", true);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            StartCoroutine(Death());
        }
    }

    IEnumerator StopHaptics()
    {
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        //Load some scene
        //SceneManager.LoadScene(0);
    }
}
