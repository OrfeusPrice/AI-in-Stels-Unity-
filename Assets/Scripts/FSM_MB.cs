using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_MB : MonoBehaviour
{
    [SerializeField] FSM fsm;
    [SerializeField] float distance;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] List<Transform> points = new List<Transform>();
    [SerializeField] int cur_point_ind;
    [SerializeField] Transform target;




    private void Start()
    {
        fsm = new FSM();

        fsm.AddState(new Idle(fsm));
        fsm.AddState(new Patrol(fsm, agent, distance, points, agent.gameObject.GetComponent<Enemy>(), cur_point_ind));
        fsm.AddState(new Chase(fsm, agent, target, agent.gameObject.GetComponent<Enemy>()));

        fsm.SetState<Idle>();
    }

    private void Update()
    {
        fsm.Update();
    }
}
