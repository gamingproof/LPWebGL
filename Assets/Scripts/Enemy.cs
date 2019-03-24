using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public int HP;
    public Transform DistPoint;
    private NavMeshAgent agent;
    private GameManager gm;
    private bool died;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 2;
        agent.isStopped = false;
    }

    private void Update()
    {
        if (agent.enabled == true && agent.remainingDistance < 1f)
        {
            Die();
        }
    }

    public void SetPoint(Transform trans)
    {
        if (agent == null)
        {

            agent = GetComponent<NavMeshAgent>();
        }
        agent.SetDestination(trans.position);
    }

    /// <summary>
    /// Атака по врагу
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>true - если враг умер после атаки</returns>
    public bool TakeDamage(int damage)
    {
        HP -= damage;
        if (!died && HP < 1)
        {
            Die(false);
            return true;
        }
        return false;
    }

    private void Die(bool selfDie = true)
    {
        died = true;
        //agent.isStopped = true;
        agent.enabled = false;
        gm.RemoveEnemy(gameObject, selfDie);
        GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.gray);
        StartCoroutine(DownGround());
    }

    private IEnumerator DownGround()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 5; i++)
        {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
        yield return new WaitForSeconds(0.3f);
        }
        Destroy(gameObject);
    }

    public void SetGameManager(GameManager gameManager)
    {
        gm = gameManager;
    }
}
