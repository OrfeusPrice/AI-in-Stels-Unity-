using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    Transform target;
    NavMeshAgent agent;
    Enemy enemy;

    public Chase(FSM fsm, NavMeshAgent agent, Transform target, Enemy enemy) : base(fsm)
    {
        this.agent = agent;
        this.target = target;
        this.enemy = enemy;
    }

    public override void Enter()
    {
        Debug.Log("Chase [ENTER]");
        agent.destination = target.position;
        agent.speed = 5;
        agent.angularSpeed = 360;
        enemy.light.GetComponent<Light>().color = Color.red;
        enemy.light.GetComponent<Light>().intensity = 20f;
    }

    public override void Exit()
    {
        Debug.Log("Chase [EXIT]");
    }

    public override void Update()
    {
        Debug.Log("Chase [UPDATE]");

        if (!enemy.targetDetected) Fsm.SetState<Patrol>();
        agent.destination = target.position;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fsm.SetState<Idle>();
        }
    }
}
