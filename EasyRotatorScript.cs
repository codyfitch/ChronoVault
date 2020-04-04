using UnityEngine;

public class EasyRotatorScript : MonoBehaviour
{
    private int randomSpeedRange;
    public int speedMultiplier = 1;

    private void Awake()
    {
        //random number between 1 and 3
        randomSpeedRange = Random.Range(1, 4);
    }

    void Update()
    {
        // Rotate the object around its local Y axis at 1 degree per second
        transform.Rotate(Vector3.up * (Time.deltaTime * randomSpeedRange) * speedMultiplier);

        // ...also rotate around the World's Y axis
        //transform.Rotate(Vector3.up * Time.deltaTime, Space.Self);
    }
}