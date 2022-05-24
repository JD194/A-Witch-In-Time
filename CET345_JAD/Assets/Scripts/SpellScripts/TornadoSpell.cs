using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TornadoSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject Tornado;
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

    private void Update()
    {

    }

    private void Cast(InputAction.CallbackContext context)
    {
        if (castForHand.mana + spellCost >= 0)
        {
            RaycastHit hitObject;
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hitObject, 200))
            {
                Instantiate(Tornado, new Vector3(hitObject.point.x, Tornado.transform.position.y, hitObject.point.z), Tornado.transform.rotation);
            }
            castForHand.UpdateMana(spellCost);
        }
    }
}
