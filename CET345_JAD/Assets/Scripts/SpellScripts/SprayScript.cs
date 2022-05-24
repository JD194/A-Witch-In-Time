using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SprayScript : MonoBehaviour
{
    public GameObject spray;
    public GameObject player;
    public CastScript castForHand;

    public int spellCost;

    [SerializeField] private InputActionAsset actionAsset;

    public int handCode;

    // Start is called before the first frame update
    void Start()
    {
        

    player = GameObject.FindGameObjectWithTag("Player");

        if (handCode == 0)
        {
            InputAction activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Activate");
            activate.Enable();
            activate.performed += Cast;
        }
        else if (handCode == 1)
        {
            InputAction activate = actionAsset.FindActionMap("XRI RightHand").FindAction("Activate");
            activate.Enable();
            activate.performed += Cast;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Cast(InputAction.CallbackContext context)
    {
        if(spray.activeSelf == false)
        {
            if (castForHand.mana + spellCost >= 0)
            {
                spray.SetActive(true);
                castForHand.UpdateMana(spellCost);
            }
        }
        else if(spray.activeSelf == true)
        {
            spray.SetActive(false);
        }
    }
}
