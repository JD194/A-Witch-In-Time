using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class CastScript : MonoBehaviour
{
    public int mana;

    public GameObject player;

    public TextMeshProUGUI manaValue;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMana(int manaChangeValue)
    {
        mana += manaChangeValue;
        //manaValue.SetText(mana.ToString());
    }
}
