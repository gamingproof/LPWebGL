using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Turret : MonoBehaviour
{
    public bool Enable;
    public TurretStats Stats;
    public NavMeshAgent Agent;
    public Enemy Target;
    public GameManager gm;
    public GoldManager goldManager;
    public GameObject attackEffect;
    private float timerAttack;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        gm = FindObjectOfType<GameManager>();
        goldManager = FindObjectOfType<GoldManager>();
        Agent.stoppingDistance = Stats.RangeAttack;
    }

    public void Update()
    {
        timerAttack += Time.deltaTime;
        if (!Enable) return;
        if (Target == null)
        {
            SearchTarget();
        }
        else
        {
            ComeToTarget();
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        if (Target != null && Agent.remainingDistance <= Stats.RangeAttack && timerAttack > Stats.DelayAttack)
        {
            timerAttack = 0;
            var go = Instantiate(attackEffect, transform.position, Quaternion.identity);
            go.GetComponent<Attack>().destination = Target.transform;
            if (Target.TakeDamage(Stats.Damge))
            {
                goldManager.AddGold(10);
                Target = null;
            }
        }
    }

    private void ComeToTarget()
    {
        if (!Agent.pathPending)
        {
            Agent.SetDestination(Target.transform.position);
        }
    }
    private void SearchTarget()
    {
        if (gm.enemies.Count() > 0)
        {
            Target = gm.enemies[0].GetComponent<Enemy>();
            var targetDist = Vector3.Distance(Target.transform.position, transform.position);
            for (int i = 0; i < gm.enemies.Count(); i++)
            {
                var dis = Vector3.Distance(gm.enemies[i].transform.position, transform.position);
                if (dis < targetDist)
                {
                    Target = gm.enemies[i].GetComponent<Enemy>();
                    targetDist = dis;
                }
            }
        }
    }
}
