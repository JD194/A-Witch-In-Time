using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBulletScript : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.SendMessageUpwards("Rock", 30, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }
}
