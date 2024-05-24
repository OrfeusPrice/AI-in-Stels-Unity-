using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    List<Transform> points = new List<Transform>();
    protected int cur_point_ind;
    protected float distance;
    NavMeshAgent agent;
    Enemy enemy;

    public Patrol(FSM fsm, NavMeshAgent agent, float d, List<Transform> points, Enemy enemy, int cur_p = 0) : base(fsm)
    {
        this.agent = agent;
        distance = d;
        this.points = points;
        this.enemy = enemy;
        cur_point_ind = cur_p;
    }

    public override void Enter()
    {
        Debug.Log("Patrol [ENTER]");
        agent.destination = points[cur_point_ind].position;
        agent.speed = 2;
        agent.angularSpeed = 180;
        enemy.light.GetComponent<Light>().color = Color.yellow;
        enemy.light.GetComponent<Light>().intensity = 15f;
    }

    public override void Exit()
    {
        Debug.Log("Patrol [EXIT]");
    }

    public override void Update()
    {
        Debug.Log("Patrol [UPDATE]");

        if (agent.remainingDistance <= distance)
        {
            cur_point_ind = cur_point_ind < points.Count - 1 ? cur_point_ind + 1 : 0;
            agent.destination = points[cur_point_ind].position;
        }

        if (enemy.targetDetected) Fsm.SetState<Chase>();
    }
}
