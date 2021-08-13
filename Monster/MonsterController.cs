using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 10.0f)]
    float _serchRange = 5.0f;

    GameObject _player = null;

    Attack _attack = null;

    HealthPoint _healthPoint = null;

    Vector3 _initPosition;

    Movement _movement = null;

    ActionManager _actionManager = null;

    [SerializeField]
    float _waitTime = 5.0f;
    float _playLastTime = Mathf.Infinity;//가장큰값


    [SerializeField]
    float _delayTime = 2.0f;
    float _arrivedAtWaypoint = Mathf.Infinity;

    [SerializeField]
    [Range(0, 1)]
    float _patrolspeedFraction = 0.2f;


    NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _attack = this.GetComponent<Attack>();
        _movement = this.GetComponent<Movement>();
        _actionManager = this.GetComponent<ActionManager>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //다른 오브젝트를 가져올때는 스타트로 가져오는게 좋다
        _player = GameObject.FindWithTag("Player");
        _healthPoint = this.GetComponent<HealthPoint>();
        _initPosition = this.transform.position;

    }


    void Update()
    {

        if (_healthPoint.IsDead) return;

        if (IsinRange() && _attack.CanAttack(_player))
        {
            Attack();
        }
        else if (_playLastTime < _waitTime)
        {
            _actionManager.StopAction();
        }
        else
        {
            _movement.Begin(_initPosition);
        }

        _playLastTime += Time.deltaTime;
    }







    private void Attack()
    {
        _playLastTime = 0.0f;
        _attack.Begin(_player);
    }

    //private void Return()
    //{
    //    _movement.Begin(_initPosition); //공격 범위 벗어나면 돌아가는 함수
    //}


    //private void Movement()
    //{
    //    this.transform.position = Vector3.Lerp(this.transform.position, _player.transform.position, Time.deltaTime * 3);
    //}


    private bool IsinRange()
    {
        Vector2 targetpoint = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);
        _distance = Vector2.Distance(targetpoint, point);
        return Vector2.Distance(targetpoint, point) < _serchRange;
    }

    float _distance = 0.0f;


    private void WakeUpStart()
    {
        _movement._agent.enabled=false;
    }
    private void WakeUpEnd()
    {
        _movement._agent.enabled = true;
    }



}
