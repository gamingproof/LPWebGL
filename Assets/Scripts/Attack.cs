using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform destination;
    private float timerAlive = 0;
    public float TimeForALive = 3;
    public float Speed = 3;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (destination != null)
        {
            transform.position = Vector3.Slerp(transform.position, destination.position, Time.deltaTime * Speed);
            if (transform.position == destination.position)
            {
                Destroy(gameObject);
            }
        }
        timerAlive += Time.deltaTime;
        if (timerAlive > TimeForALive)
        {
            Debug.Log(TimeForALive);
            Destroy(gameObject);
        }
    }
}
