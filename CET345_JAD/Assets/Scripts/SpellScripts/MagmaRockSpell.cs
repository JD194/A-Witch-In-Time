using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagmaRockSpell : MonoBehaviour
{
    public GameObject magamaRockPrefab;

    public CastScript castForHand;

    public int spellCost;
    public int handCode;

    [SerializeField] private InputActionAsset actionAsset;
    // Start is called before the first frame update
    void Start()
    {
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
        if (castForHand.mana + spellCost >= 0)
        {
            Instantiate(magamaRockPrefab, gameObject.transform.position, magamaRockPrefab.transform.rotation);
            castForHand.UpdateMana(spellCost);
        }
    }
}
