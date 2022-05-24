using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCreateScript : MonoBehaviour
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
        StartCoroutine(waterTimer());
    }
    IEnumerator waterTimer()
    {
        yield return new WaitForSeconds(10);
        player.GetComponent<TestController>().waterCreateActive = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SendMessageUpwards("Water", SendMessageOptions.DontRequireReceiver);
        }
    }
}
