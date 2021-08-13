using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class AutoManager : MonoBehaviour
{
    SkillManager _skillmanager = null;
    NavMeshAgent _playerNavi;

    Vector3 _originPos;
    [SerializeField]
    Pad _pad;
    [SerializeField]
    private Animator _ani;
    [SerializeField]
    PotionSlot _potion;
    PlayerHealth _playerHealth;

    private bool _isAuto = false;
    public bool isAuto { get { return _isAuto; } set { _isAuto = value; } }
    private void Awake()
    {
        _skillmanager = this.GetComponent<SkillManager>();
        _playerNavi = this.GetComponent<NavMeshAgent>();
        _playerHealth = this.GetComponent<PlayerHealth>();
    }
    void Start()
    {

    }

    void Update()
    {
        //  if (_skillmanager.isTouchSKill == false) return;
        // if (_skillmanager.IsUseSkill) return;


        if (_pad._isInput)
        {
            _isAuto = false;
            return;
        }


        if (_isAuto)
        {
            _skillmanager.SkillTouch(_skillmanager.autoSKill, true);
            if (_playerHealth._value < _playerHealth._maxvalue / 2)
            {
                if (_potion._item == null) return;
                _potion.UsePotion();
            }
           
        }
      

    }


    public void onclickAuto()
    {
        _isAuto = true;
        _skillmanager.CoolTimeCheck();
        _skillmanager.SkillTouch(_skillmanager.autoSKill, true);
    }


    public void AutoButton()
    {
        Debug.Log("오토버튼");
        if (_isAuto)
        {
            _isAuto = false;
            _ani.SetBool("Play", false);
        }
        else {
            _isAuto = true;
            _ani.SetBool("Play", true);
        }
    
    }


}
