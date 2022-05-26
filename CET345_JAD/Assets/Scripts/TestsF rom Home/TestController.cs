using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;

    //spell modifiers

    public bool steamActive;
    public bool torchActive;
    public bool shieldActive;
    public bool waterCreateActive;

    public GameObject spellUI;

    public GameObject shield;

    public int health;
    public int maxHealth;

    public GameObject[] hearts;



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

    public void Damage(int damage)
    {
        if (shieldActive)
        {
            shield.SetActive(false);
            shieldActive = false;
        }
        else if (!shieldActive)
        {
            HealthChange(-damage);
        }
    }

    public void HealthChange(int healthChangeVal)
    {
        health += healthChangeVal;


        if (health > maxHealth)
        {
            health = maxHealth;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].SetActive(true);
            }
            else (hearts[i]).SetActive(false);
        }

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {

    }
}
