using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileSpell : MonoBehaviour
{
    public GameObject projectile;

    public CastScript castForHand;

    public int spellCost;
    public int handCode;

    [SerializeField] private InputActionAsset actionAsset;
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
        if (castForHand.mana + spellCost >= 0)
        {
            Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
            castForHand.UpdateMana(spellCost);
        }
    }
}
