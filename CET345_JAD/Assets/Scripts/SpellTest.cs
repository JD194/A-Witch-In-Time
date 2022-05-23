using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SpellTest : MonoBehaviour
{
    public GameObject lightning;
    public LineRenderer lightningZap;

    [SerializeField] private InputActionAsset actionAsset;
    // Start is called before the first frame update
    void Start()
    {
        InputAction activate = actionAsset.FindActionMap("XRI RightHand").FindAction("Activate");
        activate.Enable();
        activate.performed += Cast;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Cast(InputAction.CallbackContext context)
    {
        StartCoroutine(Lightning());
    }

    IEnumerator Lightning()
    {
        lightningZap.enabled = true;
        lightningZap.SetPosition(0, lightning.transform.position);
        lightningZap.SetPosition(1, lightning.transform.position + new Vector3(0, 0, 5));
        yield return new WaitForSeconds(0.2f);
        lightningZap.enabled = false;
    }
}
