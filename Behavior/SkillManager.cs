using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour, IEnd
{

    private HealthPoint _healthPoint = null;

    private HealthPoint _target = null;

    private GameObject _monster = null;

    private Movement _movement = null;

    private ActionManager _actionManager = null;

    private Animator _animator = null;

    [SerializeField]
    private List<Skill> LskillSlot = new List<Skill>();

    public List<Skill> _LskillSlot { get { return LskillSlot; } set { _LskillSlot = value; } }

    [SerializeField]
    private GameObject _skilleff;

    [SerializeField]
    private List<ParticleSystem> _LskillParticle = new List<ParticleSystem>();

    private float _SkillRange = 0.0f;

    private bool _isUseSkill = false;
    public bool IsUseSkill { get { return _isUseSkill; } }

    public float _radius = 20.0f; // 반경

    //[SerializeField]
    //Skill[] _skill=new Skill[7];

    private int _skillnum;

    private bool _isTouchSKill = false;
    public bool isTouchSKill { get { return _isTouchSKill; } }

    private bool _isOnBattle = false;
    public bool IsOnBattle { get { return _isOnBattle; } }

    private int _autoSkill = 1;
    public int autoSKill { get { return _autoSkill; } }

    private float _skillDamage;



    [SerializeField]
    Inventory _inventory;
    [SerializeField]
    Item _tempItem;
    [SerializeField]
    Item _tempItem2;
    [SerializeField]
    Item _tempItem3;

    [SerializeField]
    Image[] _skillImage = new Image[4];

    private float[] _coolDown;




    private void Awake()
    {
        _movement = this.GetComponent<Movement>();
        _actionManager = this.GetComponent<ActionManager>();
        _animator = this.GetComponent<Animator>();
        _healthPoint = this.GetComponent<HealthPoint>();

    }



    void Start()
    {
        _coolDown = new float[4];
        //_skillParticle =


        _skilleff.SetActive(true);

        // _particlepos = new Vector3(0, 1, 1);

        //_Bps = _baseSkillEffect.GetComponent<ParticleSystem>();

        //for (int i = 0; i < _skill.Length; i++)
        //{
        //    LskillSlot.Add(_skill[i]);
        //    Debug.Log(LskillSlot[i]);
        //}


        _inventory.EnterItem(_tempItem, 1);
        _inventory.EnterItem(_tempItem3, 1);
        _inventory.EnterItem(_tempItem2, 1);


        for (int i = 0; i < _LskillParticle.Count; i++)
        {
            _LskillParticle[i].Stop();
        }


    }

    void Update()
    {


        if (_healthPoint.IsDead) return;
        if (_isUseSkill) return;
        if (_isTouchSKill == false) return;




        if (LskillSlot[_skillnum].IsCoolTime == false)
        {
            // Debug.Log(_skillnum);
            SearchTarget();
            if (_monster == null) return;
            Begin(_monster);
            _SkillRange = LskillSlot[_skillnum].Range;
        }

        //if (_target.IsDead)
        //{
        //    _target = null;
        //}

        if (_target == null) return;

        if (IsinRange() == false)
        {
            _movement.To(_target.transform.position);
        }
        else
        {
            _movement.End();
            this.transform.LookAt(_target.transform);
            StartCoroutine(UseSkill(_skillnum));
            _animator.SetTrigger(LskillSlot[_skillnum].SkillTrigger);
            _skillDamage = LskillSlot[_skillnum]._Damage;

            _isUseSkill = true;
            _isTouchSKill = false;

            if (_skillnum < 4) StartCoroutine(CoolDown(LskillSlot[_skillnum].Delay, _skillnum));
            // _skillnum = null;



        }

    }


    public void SkillTouch(int _num, bool _value)
    {
        _skillnum = _num;
        _isTouchSKill = _value;
    }

    IEnumerator UseSkill(int _num)
    {
        LskillSlot[_num].IsCoolTime = true;
        yield return new WaitForSeconds(LskillSlot[_num].Delay);
        LskillSlot[_num].IsCoolTime = false;
    }

    private void Hit()
    {
        if (_target != null)
            _target.Damage(_skillDamage);


        // _skillParticle[LskillSlot[_skillnum].skillNum].gameObject.transform.position = this.transform.position + _particlepos;
        // _skillParticle[LskillSlot[_skillnum].skillNum].gameObject.transform.LookAt(_target.transform);

        // _LskillParticle[3].Play();
        for (int i = 0; i < LskillSlot.Count; i++)
        {
            if (LskillSlot[i].IsCoolTime == true)
            {
                // _skillEffect[i].GetComponent<ParticleSystem>().Play();
                // _skillEffect[i].transform.position = this.transform.position;
            }
        }
    }
    private void Splash()
    {
        Collider[] Monsters = Physics.OverlapSphere(this.transform.position, _LskillSlot[_skillnum].Splash, 1 << 9);

        for (int i = 0; i < Monsters.Length; i++)
        {
            Monsters[i].GetComponent<MonsterHealth>().Damage(_skillDamage);
        }

    }
    private void Spear()
    {

        Collider[] Monsters = Physics.OverlapCapsule(this.transform.position + new Vector3(0, 0, 2), this.transform.position + new Vector3(0, 0, 4), _LskillSlot[_skillnum].Splash, 1 << 9);


        for (int i = 0; i < Monsters.Length; i++)
        {
            Monsters[i].GetComponent<MonsterHealth>().Damage(_skillDamage);
        }

    }


    private void Effect()
    {
        _LskillParticle[LskillSlot[_skillnum].skillNum].Play();
    }




    private void SkillStart()
    {
        // _LskillParticle[7].gameObject.transform.position = this.transform.position;
        _LskillParticle[7].Play();
        //_Bps.Play();
        // _baseSkillEffect.transform.position = this.transform.position;
    }
    private void SkillEnd()
    {
        //_Bps.Stop();
        _isUseSkill = false;
        CoolTimeCheck();
        End();
    }

    public void CoolTimeCheck()
    {
        for (int i = 1; i < _LskillSlot.Count; i++)
        {
            if (_LskillSlot[i].IsCoolTime == false)
            {
                _autoSkill = i;
                return;
            }

        }
        _autoSkill = 0;
    }


    public void Begin(GameObject target)
    {
        _target = target.GetComponent<HealthPoint>();
        _actionManager.StartAction(this);
    }

    public void End()
    {
        _target = null;
    }

    private bool IsinRange()
    {
        Vector2 targetPoint = new Vector2(_target.transform.position.x, _target.transform.position.z);
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);

        return Vector2.Distance(targetPoint, point) < _SkillRange;//사거리
    }

    public void SearchTarget()
    {
        Collider[] Monsters = Physics.OverlapSphere(this.transform.position, _radius, 1 << 9);
        if (Monsters.Length == 0) _isTouchSKill = false;

        float ShortestTarget = Mathf.Infinity;

        //몬스터가 죽은상태면 없애 줘야함....

        //for (int i = 0; i < Monsters.Length; i++)
        //{
        //    if (Monsters[i].gameObject.GetComponent<MonsterHealth>().IsDead)
        //    {
        //        Monsters.
        //    }

        //}

        foreach (Collider Monster in Monsters)
        {
            float Distance = Vector3.Distance(this.transform.position, Monster.gameObject.transform.position);

            if (Distance < ShortestTarget)
            {
                ShortestTarget = Distance;
                _monster = Monster.gameObject;
            }
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(this.transform.position, _radius);
    //    //Gizmos.Dr
    //    //Collider[] Monsters = Physics.ov
    //}

    IEnumerator CoolDown(float coolTime, int num)
    {
        _coolDown[num] = 0;

        while (_coolDown[num] <= coolTime)
        {
            _coolDown[num] += Time.deltaTime;
            _skillImage[num].fillAmount = _coolDown[num] / coolTime;
            //if (_coolDown[num] >= coolTime)
            //{
            //    StopCoroutine(CoolDown);
            //}

            yield return null;
        }
    }



}
