using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrushSpell : MonoBehaviour
{
    public GameObject player;
    public GameObject spellPoint;
    public CastScript castForHand;

    public int spellCost;
    public LineRenderer crushLine;

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
                if (hitObject.collider.tag == "Enemy")
                {
                    StartCoroutine(LightningCast(hitObject.point));
                    hitObject.collider.SendMessageUpwards("Crush", SendMessageOptions.DontRequireReceiver);
                }
            }
            castForHand.UpdateMana(spellCost);
        }
    }

    IEnumerator LightningCast(Vector3 hitPpoint)
    {
        crushLine.enabled = true;
        crushLine.SetPosition(0, gameObject.transform.position);
        crushLine.SetPosition(1, hitPpoint);
        yield return new WaitForSeconds(0.2f);
        crushLine.enabled = false;
    }
}
