using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPoint : MonoBehaviour
{
    

    public float _maxvalue;
    public float _value;


    protected bool _isDead = false;
    public bool IsDead { get { return _isDead; } }


    protected PlayerController _playerController = null;
 


    Transform _trans = null;
    private void Awake()
    {
        _playerController = this.GetComponent<PlayerController>();
    }
   

    public virtual void Damage(float damage)
    {
        _value = Mathf.Max(_value - damage, 0);


        if (_value <= 0.0f)
        {
            Dead();
        }

    }



    protected virtual void Dead()
    {
        //Debug.Log("부모");
    }

    public void Heal(float healpoint)
    {
        //  Debug.Log("회복전:" + _value);

        _value = Mathf.Min(_value + healpoint
            , _maxvalue);


        // Debug.Log("회복후:" + _value);
    }





}
