using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(steamTimer());
    }
    IEnumerator steamTimer()
    {
        yield return new WaitForSeconds(10);
        player.GetComponent<TestController>().steamActive = false;
        gameObject.SetActive(false);
    }
}
