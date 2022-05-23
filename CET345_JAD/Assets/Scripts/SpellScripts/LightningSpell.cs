using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LightningSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject spellPoint;
    public CastScript castForHand;

    public int spellCost;
    public LineRenderer lightningLine;

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
                if(hitObject.collider.tag == "Enemy")
                {
                    StartCoroutine(LightningCast(hitObject.point));
                    hitObject.collider.SendMessageUpwards("Lightning", 0, SendMessageOptions.DontRequireReceiver);
                }
            }
            castForHand.UpdateMana(spellCost);
        }
    }

    IEnumerator LightningCast(Vector3 hitPpoint)
    {
        lightningLine.enabled = true;
        lightningLine.SetPosition(0, gameObject.transform.position);
        lightningLine.SetPosition(1, hitPpoint);
        yield return new WaitForSeconds(0.2f);
        lightningLine.enabled = false;
    }
}