using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthPoint
{
    [SerializeField]
    public Slider _playerHp;


    public override void Damage(float damage)
    {
        base.Damage(damage);
        _playerHp.value = _value;
    }

   


    public void Levelup(int MaxValue)
    {
        _playerHp.maxValue = MaxValue;
        _value = MaxValue;
        _playerHp.value = _value;

    }



}
