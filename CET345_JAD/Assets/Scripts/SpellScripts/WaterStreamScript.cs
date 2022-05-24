using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStreamScript : MonoBehaviour
{
    public float manaDelay;
    public float timeToDeductmana;

    public CastScript castForHand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToDeductmana)
        {
            castForHand.UpdateMana(-5);
            timeToDeductmana = Time.time + manaDelay;
        }
    }

    private void OnEnable()
    {
        timeToDeductmana = Time.time + manaDelay;
    }

    private void  OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SendMessageUpwards("Water", SendMessageOptions.DontRequireReceiver);
        }
    }
}
