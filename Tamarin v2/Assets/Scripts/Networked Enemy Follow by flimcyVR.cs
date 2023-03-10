using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class EnemyFollow : MonoBehaviourPunCallbacks
{
    [Header("THIS SCRIPT WAS MADE BY FLIMCYVR. IT IS NOT YOURS.")]
    [Header("Distributing This Script Will Lead To A Permanent Ban and MORE!")]
    [Header("If you make a video on this script")]
    [Header("credit me with my discord and youtube")]
    public float detectRange = 10f;
    public float attackRange = 1.5f;
    public float speed = 3f;

    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = null;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (target == null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange);
                float closestDistance = Mathf.Infinity;

                
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        float distance = Vector3.Distance(transform.position, collider.transform.position);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            target = collider.transform;
                        }
                    }
                }
            }
            else
            {
                agent.SetDestination(target.position);
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget > detectRange)
                {
                    target = null;
                }
            }
        }
    }
}