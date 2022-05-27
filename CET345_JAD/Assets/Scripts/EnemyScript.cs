using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public bool lightningImmunity;
    public bool shielded;
    public bool wet;
    public bool dead;

    public GameObject heatBar;
    public GameObject healthBar;
    public GameObject wetMarker;
    public GameObject blindMarker;

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI heatText;

    public Material lightningImmuneMat;
    public GameObject shieldObject;

    public int currentHeat;
    public int maxHeat;
    public int heatSinks;

    public int health;
    public int maxHealth;

    public float timeToCool;
    public float coolDelay;
    public bool cooling;

    public GameObject crushParticles;

    public LineRenderer lightningLine;
    public GameObject[] heatSinkObs;

    public EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        if (currentHeat > 0 && !cooling && Time.time >= timeToCool)
        {
            cooling = true;
            StartCoroutine(Cool());
        }
    }

    public void SetUpEnemy(int heatSinksPassed, bool lightningImmune, bool shield)
    {
        heatSinks = heatSinksPassed;
        lightningImmunity = lightningImmune;
        shielded = shield;

        for (int i = 0; i < heatSinks; i++)
        {
            heatSinkObs[i].SetActive(true);
        }

        if (lightningImmunity)
        {
            gameObject.GetComponent<MeshRenderer>().material = lightningImmuneMat;
        }

        if (shielded)
        {
            shieldObject.SetActive(true);
        }

        RectTransform heatBarRect = (RectTransform)heatBar.transform;
        heatBarRect.sizeDelta = new Vector2(currentHeat, heatBarRect.sizeDelta.y);
    }

    // Update is called once per frame

    public void Lightning(int links)
    {

        if(!lightningImmunity && !dead)
        {
            if(links < 6)
            {
                if (!wet)
                {
                    links += 2;
                }
                else
                {
                    links++;
                }
                zapNearestEnemy(links);
            }
            spawner.totalKilled++;
            spawner.killedLightning++;
            StartCoroutine(Death());
        }

        else if (wet)
        {
            links++;
            zapNearestEnemy(links);
        }

        if (heatSinks > 0)
        {
            HealthChange(5 * heatSinks);
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
            Debug.Log("enemy nou null");
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

    public void Fire(int heatVal)
    {
        StopCoroutine(SetHeatText(0));

        if (shielded)
        {
            heatVal *= 2;
        }

        int actualHeatVal = heatVal - (5 * heatSinks);
        currentHeat += actualHeatVal;

        StartCoroutine(SetHeatText(actualHeatVal));

        if (currentHeat >= maxHeat)
        {
            if(heatSinks > 0)
            {
                heatSinks--;
                currentHeat = 0;
            }
            else
            {
                spawner.totalKilled++;
                spawner.killedFire++;
                StartCoroutine(Death());
            }
        }

        RectTransform heatBarRect = (RectTransform)heatBar.transform;
        heatBarRect.sizeDelta = new Vector2(currentHeat, heatBarRect.sizeDelta.y);

        StopCoroutine(Cool());
        timeToCool = Time.time + coolDelay;
    }

    public void Air(int damage)
    {
        if (!shielded)
        {
            HealthChange(damage);
        }
    }

    public void Rock(int damage)
    {
        if (shielded)
        {
            damage = damage / 2;
        }
        HealthChange(damage);
    }

    public void Water()
    {
        if (!cooling)
        {
            cooling = true;
            StartCoroutine(Cool());
        }
        wetMarker.SetActive(true);
        wet = true;
    }

    public void HealthChange(int healthVal)
    {
        StopCoroutine(SetDamageText(0));

        health -= healthVal;

        StartCoroutine(SetDamageText(healthVal));

        if(health > maxHealth)
        {
            health = maxHealth;
        }

        RectTransform healthBarRect = (RectTransform)healthBar.transform;
        healthBarRect.sizeDelta = new Vector2(health, healthBarRect.sizeDelta.y);

        if (health <= 0)
        {
            spawner.totalKilled++;
            spawner.killedPhysical++;
            StartCoroutine(Death());
        }
    }

    public void Crush()
    {
        StartCoroutine(CrushSystem());
    }

    IEnumerator CrushSystem()
    {
        crushParticles.SetActive(true);
        Vector3 objectScaleReduction;
        objectScaleReduction = gameObject.transform.localScale / 10f;
        for(int i = 0; i < 10; i++)
        {
            gameObject.transform.localScale -= objectScaleReduction;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Death());
    }

    IEnumerator Cool()
    {
        while (currentHeat > 0)
        {
            yield return new WaitForSeconds(0.1f);
            currentHeat--;

            RectTransform heatBarRect = (RectTransform)heatBar.transform;
            heatBarRect.sizeDelta = new Vector2(currentHeat, heatBarRect.sizeDelta.y);
        }

        cooling = false;
    }

    IEnumerator Death()
    {
        dead = true;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    IEnumerator SetHeatText(int heatVal)
    {
        heatText.SetText("+" + heatVal);
        yield return new WaitForSeconds(0.2f);
        heatText.SetText("");
    }

    IEnumerator SetDamageText(int damageVal)
    {
        damageText.SetText("-" + damageVal);
        yield return new WaitForSeconds(0.2f);
        damageText.SetText("");
    }


}
