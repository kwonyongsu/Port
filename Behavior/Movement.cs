using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour, IEnd
{

    [SerializeField]
    Transform _target;

    public NavMeshAgent _agent;
    

    Animator _animator;

    ActionManager _actionManager = null;

    private void Awake()
    {
        _actionManager = this.GetComponent<ActionManager>();
        _animator = this.GetComponentInChildren<Animator>();
        _agent = this.GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        UpDateAnimator();
    }

    private void UpDateAnimator()
    {
        // 에이전트의 속도를 가져오는데, 절대공간의 방향이자 속도.
        // InverseTransformDirection함수를 사용해서 로컬로 변경

        // 절대공간의 속도 들고오기
        Vector3 velocity = _agent.velocity;
        Vector3 local = this.transform.InverseTransformDirection(velocity);

        _animator.SetFloat("MoveSpeed", local.z);
    }


    void Update()
    {

    }


    public void Begin(Vector3 dest)
    {
        _actionManager.StartAction(this);
        To(dest);
    }

    public void To(Vector3 dest)
    {
        if (_agent != null && _agent.enabled)
        {
            _agent.destination = dest;

            _agent.isStopped = false;
        }
    }

    public void End()
    {
        if (_agent != null && _agent.enabled)
        {
            _agent.isStopped = true;
        }
    }



}
