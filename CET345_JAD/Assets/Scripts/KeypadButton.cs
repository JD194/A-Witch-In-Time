using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public KeypadMain attachedPad;
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFinger"))
        {
            attachedPad.EnterNumber(number);
        }
    }
}
