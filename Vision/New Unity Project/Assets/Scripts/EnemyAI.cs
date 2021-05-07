using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
        AttackTarget,
        GoingBackToStart,
        
    }

    private State state;
    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask whatIsGround, WhatIsPlayer;

    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private float nextAttackTime;
    
    // Patroling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    private void Awake()
    {
        state = State.Roaming;
        
    }

    private void Start()
    {
        startingPosition = transform.position;
    }


    private void Update()
    {
        switch (state)
        {
            default:
                case State.Roaming:
                Roaming();
                FindTarget();
                break;
            case State.ChaseTarget:
                enemy.destination = player.position;

                float attackRange = 5f;

                if (Vector3.Distance(transform.position, player.position) < attackRange)
                    // Target within attack range
                {
                    // Control how fast the enemy attacks 
                    if (Time.time > nextAttackTime)
                    {
                        state = State.AttackTarget;
                        // attack code ...( ... () => { state = State.ChaseTarget; });

                        float attackRate = 0.5f;
                        nextAttackTime = Time.time + attackRate;
                    }
                }

                float stopChaseDistance = 25f;
                if (Vector3.Distance(transform.position, player.position) > stopChaseDistance)
                    // Too far, stop chasing
                {
                    state = State.GoingBackToStart;
                }
                break;
            case State.AttackTarget:
                break;
            case State.GoingBackToStart:
                enemy.destination = startingPosition;

                float reachedPositionDistance = 10f;
                if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance)
                    // Reached Start Position
                {
                    state = State.Roaming;
                }
                break;
        }
    }

    private void Roaming()
    {
        
        if (!walkPointSet) SearchWalkPoint() ;

        if (walkPointSet) enemy.SetDestination (walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        // Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ).normalized;

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
            
    }
    private void FindTarget()
    {
        float targetRange = 20f;

        if (Vector3.Distance(transform.position, player.position) < targetRange)
            // Player within target range
        {
            state = State.ChaseTarget;
        }
    }

}
