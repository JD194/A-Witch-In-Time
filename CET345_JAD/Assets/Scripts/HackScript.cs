using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HackScript : MonoBehaviour
{
    public bool attacked;

    public float hackDelay;
    public float timeToHack;

    public int hackCount;

    public GameObject hackNum;

    public TextMeshProUGUI percent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToHack)
        {
            hackCount++;
            timeToHack = Time.time + hackDelay;
            if(hackCount >= 100)
            {
                hackCount = 100;
                hackNum.SetActive(true);
            }

            percent.SetText(hackCount + "%");
        }
    }

    public void Attacked()
    {
        timeToHack = Time.time + hackDelay;
    }
}
