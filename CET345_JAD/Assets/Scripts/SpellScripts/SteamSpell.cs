using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SteamSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject steam;
    public CastScript castForHand;

    public int spellCost;

    [SerializeField] private InputActionAsset actionAsset;


    public int handCode;

    InputAction activate;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

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
        if (!player.GetComponent<TestController>().steamActive && castForHand.mana + spellCost >= 0)
        {
            player.GetComponent<TestController>().steamActive = true;
            steam.SetActive(true);
            castForHand.UpdateMana(spellCost);
        }
    }
}
