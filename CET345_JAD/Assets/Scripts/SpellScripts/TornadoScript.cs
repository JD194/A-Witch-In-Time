using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    public float timeTilGone;
    public float goneDelay;

    public float timeToHit;
    public float hitDelay;

    public CastScript castForHand;
    // Start is called before the first frame update
    void Start()
    {
        timeTilGone = Time.time + goneDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeTilGone)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (Time.time > timeToHit)
        {
            if (other.CompareTag("Enemy"))
            {
                SendMessageUpwards("HealthChange", -10, SendMessageOptions.DontRequireReceiver);
            }
            timeToHit = Time.time + hitDelay;
        }
    }
}
