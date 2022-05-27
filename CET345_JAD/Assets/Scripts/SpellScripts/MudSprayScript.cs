using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSprayScript : MonoBehaviour
{
    public float manaDelay;
    public float timeToDeductmana;

    public float timeToHit;
    public float hitDelay;

    public CastScript castForHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToDeductmana)
        {
            castForHand.UpdateMana(-5);
            timeToDeductmana = Time.time + manaDelay;
        }
    }

    private void OnEnable()
    {
        timeToDeductmana = Time.time + manaDelay;
    }

    private void OnTriggerStay(Collider other)
    {
        if(Time.time > timeToHit)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("MUD IS HITTING");
                SendMessageUpwards("Rock", 2, SendMessageOptions.DontRequireReceiver);
                timeToHit = Time.time + hitDelay;
            }
        }
    }
}
