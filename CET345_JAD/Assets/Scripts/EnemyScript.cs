using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool lightningImmunity;
    public bool rubberised;
    public int heatSinks;

    public LineRenderer lightningLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Lightning(int links)
    {
        if(!lightningImmunity || !rubberised)
        {
            if(links < 2)
            {
                links++;
                GameObject[] enemies;
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                Vector3 thisPosition = gameObject.transform.position;
                GameObject closestEnemy;
                float closestDistance = Mathf.Infinity;
                float distance = 0;
                foreach (GameObject enemy in enemies)
                {
                    distance = Vector3.Distance(thisPosition, enemy.transform.position);
                    if(distance < closestDistance && enemy != gameObject)
                    {
                        closestEnemy = enemy;
                    }
                }

                RaycastHit hitObject;
                if (Physics.Raycast(gameObject.transform.position, Camera.main.transform.forward, out hitObject, 200))
                {
                    if (hitObject.collider.tag == "Enemy")
                    {
                        StartCoroutine(LightningCast(hitObject.point));
                        hitObject.collider.SendMessageUpwards("Lightning", links, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
            Death();
        }
    }

    IEnumerator LightningCast(Vector3 hitPpoint)
    {
        lightningLine.enabled = true;
        lightningLine.SetPosition(0, gameObject.transform.position);
        lightningLine.SetPosition(1, hitPpoint);
        yield return new WaitForSeconds(0.2f);
        lightningLine.enabled = false;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
