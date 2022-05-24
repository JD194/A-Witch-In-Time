using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class CastScript : MonoBehaviour
{
    public int mana;
    public int maxMana;

    public float restoreDelay;
    public float timeToRestore;

    public bool restoring;

    public GameObject player;

    public TextMeshProUGUI manaValue;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mana < maxMana && !restoring && Time.time >= timeToRestore)
        {
            restoring = true;
            StartCoroutine(RestoreMana());
        }
    }

    public void UpdateMana(int manaChangeValue)
    {
        mana += manaChangeValue;
        manaValue.SetText(mana.ToString());
        timeToRestore = Time.time + restoreDelay;
        restoring = false;
        StopAllCoroutines();
    }

    IEnumerator RestoreMana()
    {
        while(mana < maxMana)
        {
            yield return new WaitForSeconds(0.1f);
            mana++;
            manaValue.SetText(mana.ToString());
        }
        restoring = false;
    }
}
