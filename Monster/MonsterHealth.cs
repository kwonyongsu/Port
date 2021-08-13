using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterHealth : HealthPoint
{
    [SerializeField]
    public Monster _monsterinfo;
    [SerializeField]
    private GameObject _hpBarUI;
    [SerializeField]
    private Text _damageTxt;
    [SerializeField]
    private Slider _monsterHpSlider;

    private Camp _camp = null;
    private Animator _animator = null;
    private ActionManager _actionManager = null;
    private Movement _movement = null;
    private SkillManager _skillmanager = null;
    private QuestManager _questmanager = null;
    private Camera _mainCamera;
    float _rigidityTime = 0.3f;
    float _hpBarTime = 3.0f;
    float _damageTime = 0.5f;

    private int _respawnNum;
    public int respawnNum { get { return _respawnNum; } set { _respawnNum = value; } }

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _actionManager = this.GetComponent<ActionManager>();
        _movement = this.GetComponent<Movement>();
        _questmanager = FindObjectOfType<QuestManager>();
        
        _skillmanager = GameObject.Find("Player").GetComponent<SkillManager>();

        _camp = GameObject.Find(_monsterinfo.campName).GetComponent<Camp>();

    }

    private void OnEnable()
    {
       
        MonsterReset();
     
    }

    private void Start()
    {
        
        _value = _monsterinfo.maxHp;
        _mainCamera=Camera.main;
        _hpBarUI.SetActive(true);
        _monsterHpSlider.maxValue = _value;
        _hpBarUI.SetActive(false);

    }

    public override void Damage(float damage)
    {
        base.Damage(damage);

        StartCoroutine(Rigidity());
        StartCoroutine(HpBar());
        StartCoroutine(DamageTxt(damage));
    }


    protected override void Dead()
    {
        //Debug.Log("자식");
        if (_isDead == true) return;
        _isDead = true;
        _animator.SetTrigger("Dead");
        _actionManager.StopAction();
        _movement._agent.enabled = false;

        PlayerDataBase.instance.Experience(_monsterinfo._xp);
       

        _camp.MonsterReSpawn(this.gameObject, _respawnNum);
       
        
        
        _skillmanager.End();

        if (_questmanager._isQuest[0])
        {
            _questmanager.HuntQuest(_monsterinfo);
        }

        StopAllCoroutines();
        _hpBarUI.gameObject.SetActive(false);

    }





    void MonsterReset()
    {
        _movement._agent.enabled = true;
        _isDead = false;
        _value = _monsterinfo.maxHp;
        //_movement._agent.enabled = true;

        _hpBarUI.gameObject.SetActive(false);
        _damageTxt.gameObject.SetActive(false);
        _animator.SetTrigger("WakeUp");
    }

    IEnumerator Rigidity()
    {
        float temp = 0;
        _animator.SetTrigger("Stun");

        while (temp<_rigidityTime)
        {
            temp += Time.deltaTime;
            _actionManager.StopAction();
            _movement._agent.enabled = false;


            yield return null;
        }
       // _animator.ResetTrigger("Stun");
        //_animator.enabled
        _movement._agent.enabled = true;
        _animator.ResetTrigger("Stun");

        //  _actionManager.StartAction(_movement);
    }

    IEnumerator HpBar()
    {
        float temp2 = 0;

        while (temp2 < _hpBarTime)
        {
            temp2 += Time.deltaTime;

            _hpBarUI.gameObject.SetActive(true);
            _monsterHpSlider.value = _value;
            _hpBarUI.transform.forward = _mainCamera.transform.forward;


             yield return null;
        }
        _hpBarUI.gameObject.SetActive(false);
    }

    IEnumerator DamageTxt(float damage)
    {
        float temp3 = 0;

        while (temp3 < _damageTime)
        {
            temp3 += Time.deltaTime;

            _damageTxt.gameObject.SetActive(true);
            _damageTxt.transform.forward = _mainCamera.transform.forward;
            _damageTxt.text = damage.ToString();
            yield return null;
        }

        _damageTxt.gameObject.SetActive(false);
    }









    


}
