using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloudSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject cloud;
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
        if (castForHand.mana + spellCost >= 0)
        {
            RaycastHit hitObject;
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hitObject, 200))
            {
                Instantiate(cloud, hitObject.point + new Vector3(0, 0, 0), cloud.transform.rotation);
            }
            castForHand.UpdateMana(spellCost);
        }
    }
}
