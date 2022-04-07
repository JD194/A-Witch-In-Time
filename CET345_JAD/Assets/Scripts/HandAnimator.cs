using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    public Animator thisHandAnimator;
    public GameObject pointer;
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
        Debug.Log("Hands on Trigger Worked " + other.tag);
        if (other.CompareTag("Keypad"))
        {
            Debug.Log("Hands picked up keypad with" + other.tag);
            thisHandAnimator.SetTrigger("Pointing");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Keypad"))
        {
            thisHandAnimator.SetBool("Pointed", false);
            pointer.SetActive(false);
        }
    }

    public void SetPointed()
    {
        Debug.Log("Animation Event Calling Function");
        thisHandAnimator.SetBool("Pointed", true);
        pointer.SetActive(true);
    }
}
