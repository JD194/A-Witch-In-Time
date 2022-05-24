using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool lightningImmunity;
    public bool rubberised;
    public bool wet;

    public int currentHeat;
    public int maxHeat;
    public int heatSinks;

    public int health;
    public int maxHealth;

    public float timeToCool;
    public float coolDelay;
    public bool cooling;

    public LineRenderer lightningLine;
    public GameObject[] heatSinkObs;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < heatSinks; i++)
        {
            heatSinkObs[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHeat > 0 && !cooling && Time.time >= timeToCool)
        {

        }
    }

    void Lightning(int links)
    {
        if(!lightningImmunity || !rubberised)
        {
            if(links < 2)
            {
                if (!wet)
                {
                    links++;
                }
                zapNearestEnemy(links);
            }
            StartCoroutine(Death());
        }
        else if (wet)
        {
            zapNearestEnemy(links);
        }
    }

    void zapNearestEnemy(int prevlinks)
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 thisPosition = gameObject.transform.position;
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        float distance = 0;
        foreach (GameObject enemy in enemies)
        {
            Debug.Log("running through enemy loop");
            distance = Vector3.Distance(thisPosition, enemy.transform.position);
            if (distance < closestDistance && enemy != gameObject)
            {
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            Debug.Log("enemy nout null");
            Vector3 direction;
            direction = closestEnemy.transform.position - transform.position;
            RaycastHit hitObject;
            if (Physics.Raycast(gameObject.transform.position, direction, out hitObject, 200))
            {
                if (hitObject.collider.tag == "Enemy")
                {
                    StartCoroutine(LightningCast(hitObject.point));
                    hitObject.collider.SendMessageUpwards("Lightning", prevlinks, SendMessageOptions.DontRequireReceiver);
                }
            }
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

    void Fire(int heatVal)
    {
        currentHeat += heatVal;
        if(currentHeat >= maxHeat)
        {
            if(heatSinks > 0)
            {
                heatSinks--;
                currentHeat = 0;
            }
            else
            {
                StartCoroutine(Death());
            }
        }
        StopCoroutine(Cool());
        timeToCool = Time.time + coolDelay;
    }

    void Rock(int damage)
    {
        HealthChange(-damage);
    }

    void Water()
    {
        wet = true;
    }

    void HealthChange(int healthVal)
    {
        health += healthVal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Cool()
    {
        while (currentHeat > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentHeat--;
        }
        cooling = false;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
