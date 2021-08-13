using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IEnd
{

    HealthPoint _healthPoint = null;

    HealthPoint _target = null;

    Movement _movement = null;

    ActionManager _actionManager = null;

    Animator _animator = null;

    MonsterHealth _monsterhealth=null;

    [SerializeField]
    [Range(1.0f, 3.0f)]
    float _attackDelay = 2.0f;
    float _siceinceLastAttack = 0.0f;

    [SerializeField]
    [Range(1.0f, 5.0f)]
    float TempRange = 2.0f;


    private void Awake()
    {
        _movement = this.GetComponent<Movement>();
        _actionManager = this.GetComponent<ActionManager>();
        _animator = this.GetComponent<Animator>();
        _healthPoint = this.GetComponent<HealthPoint>();
        _monsterhealth=this.GetComponent<MonsterHealth>();
    }

    void Start()
    {
        _siceinceLastAttack = _attackDelay;//최초공격은 바로
    }


    void Update()
    {

        _siceinceLastAttack += Time.deltaTime;

        if (_healthPoint.IsDead) return;

        if (_target == null) return;
        if (_target.IsDead == true)
        {
            _animator.ResetTrigger("Attack");
            return;
        }

        if (IsinRange() == false)
        {
            _movement.To(_target.transform.position);// 어차피 최고속도로 공격해야 하므로 1.0f 준ㄷ
        }
        else
        {
            //End(); 를 지워도된다.
            _movement.End();//공격모션을 넣어야 하므로
                            // _animator.SetTrigger("Attack");
            PlayAnimation();
        }



    }

    public bool CanAttack(GameObject target)
    {
        if (target == null) return false;

        HealthPoint hp = target.GetComponent<HealthPoint>();
        return hp != null && hp.IsDead == false;

    }



    public void Begin(GameObject target)
    {
        _target = target.GetComponent<HealthPoint>();
        _actionManager.StartAction(this);
    }
    public void End()
    {
        _animator.ResetTrigger("Attack");
        _animator.SetTrigger("StopAttack");
        _target = null;
    }
    private bool IsinRange()
    {
        //높이를 무시 
        Vector2 targetPoint = new Vector2(_target.transform.position.x, _target.transform.position.z);
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);

        return Vector2.Distance(targetPoint, point) < TempRange;//사거리
    }

    private void MonsterAtk()
    {
        if (_target == null) return;

          _target.Damage(_monsterhealth._monsterinfo.atk);
    }

    private void PlayAnimation()
    {
        this.transform.LookAt(_target.transform);//오버로드 가보면 축이나옴 디폴트로 vector3 up 방향을 받고 있음


        if (_siceinceLastAttack < _attackDelay) return;
        _animator.SetTrigger("Attack");
        _siceinceLastAttack = 0.0f;
        _animator.ResetTrigger("StopAttack");
    }

}
