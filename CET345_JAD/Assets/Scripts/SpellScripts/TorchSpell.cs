using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TorchSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject torch;
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
        else if(handCode == 1)
        {
            InputAction activate = actionAsset.FindActionMap("XRI RightHand").FindAction("Activate");
            activate.Enable();
            activate.performed += Cast;
        }
    }

    private void Update()
    {
        
    }

    private void Cast(InputAction.CallbackContext context)
    {
        if (!player.GetComponent<TestController>().torchActive && castForHand.mana + spellCost >= 0)
        {
            player.GetComponent<TestController>().torchActive = true;
            torch.SetActive(true);
            castForHand.UpdateMana(spellCost);
        }
    }
}
