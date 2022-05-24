using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaRockScript : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SendMessageUpwards("Rock", 10, SendMessageOptions.DontRequireReceiver);
            SendMessageUpwards("Fire", 10, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }
}
