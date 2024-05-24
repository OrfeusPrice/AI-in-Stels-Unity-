using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Range(0, 360)] public float ViewAngle = 90f;
    public float ViewDistance = 15f;
    public float DetectionDistance = 3f;
    public Transform EnemyEye;
    public Transform Target;
    public bool targetDetected;
    private FSM_MB fsm;
    private NavMeshAgent agent;
    public Light light;

    private void Start()
    {
        fsm = GetComponent<FSM_MB>();
        agent = GetComponent<NavMeshAgent>();
        targetDetected = false;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);
        if (IsInView() && !targetDetected)
        {
            targetDetected = true;
            StartCoroutine(Chasing());
        }
        DrawViewState();
    }
    private bool IsInView()
    {
        float realAngle = Vector3.Angle(EnemyEye.forward, 
                            Target.position - EnemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(EnemyEye.transform.position, 
            Target.position - EnemyEye.position, 
            out hit, ViewDistance))
        {
            if (realAngle < ViewAngle / 2f && 
                Vector3.Distance(EnemyEye.position, Target.position)
                <= ViewDistance && 
                hit.transform == Target.transform)
                return true;
        }
        return false;
    }
    private void DrawViewState()
    {
        Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Debug.DrawLine(EnemyEye.position, left, Color.yellow);
        Debug.DrawLine(EnemyEye.position, right, Color.yellow);
    }

    private IEnumerator Chasing()
    {
        while (true)
        {
            if (!IsInView())
            {
                yield return new WaitForSeconds(6);
                targetDetected = false;
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(6);
        }
    }
}
