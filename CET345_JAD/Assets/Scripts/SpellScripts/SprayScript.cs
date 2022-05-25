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

    InputAction activate;
    // Start is called before the first frame update
    void Start()
    {

        if (handCode == 0)
        {
            activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Activate");
            activate.Enable();
            activate.performed += Cast;
        }
        else if (handCode == 1)
        {
            activate = actionAsset.FindActionMap("XRI RightHand").FindAction("Activate");
            activate.Enable();
            activate.performed += Cast;
        }
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        activate.performed += Cast;
    }

    private void OnDisable()
    {
        activate.performed -= Cast;
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
