using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHaptics : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HurtBox")
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            StartCoroutine(StopHaptics());
        }
    }

    IEnumerator StopHaptics()
    {
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
