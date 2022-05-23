using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;

    //spell modifiers

    public bool steamActive;
    public bool torchActive;



    public float speed;
    public float rotationSpeed;


    public Camera thisCam;

    public GameObject spellUI;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        InputAction activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Menu");
        activate.Enable();
        activate.performed += SpellMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    void SpellMenu(InputAction.CallbackContext context)
    {
        if (spellUI.activeInHierarchy)
        {
            spellUI.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            spellUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
