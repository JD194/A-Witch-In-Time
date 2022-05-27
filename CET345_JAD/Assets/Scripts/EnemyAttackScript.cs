using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAttackScript : MonoBehaviour
{
    public NavMeshAgent navAgent;

    GameObject target;

    public LineRenderer zapLine;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();

        GameObject[] targets;
        targets = GameObject.FindGameObjectsWithTag("Computer");
        Vector3 thisPosition = gameObject.transform.position;
        target = null;
        float closestDistance = Mathf.Infinity;
        float distance = 0;
        foreach (GameObject computer in targets)
        {
            Debug.Log("running through enemy loop");
            distance = Vector3.Distance(thisPosition, computer.transform.position);
            if (distance < closestDistance)
            {
                target = computer;
            }
        }

        navAgent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, target.transform.position) < 4f)
        {
            zapLine.enabled = true;
            zapLine.SetPosition(0, gameObject.transform.position);
            zapLine.SetPosition(1, target.transform.position);
            target.GetComponent<HackScript>().Attacked();
        }
        else
        {
            zapLine.enabled = false;
        }

    }
}
