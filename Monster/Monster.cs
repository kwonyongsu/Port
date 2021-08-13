using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "New Monster/Monster")]
public class Monster : ScriptableObject
{
   

    [SerializeField]
    private float _maxhp;
    public float maxHp { get { return _maxhp; } }
    [SerializeField]
    private float _atk;
    public float atk { get { return _atk; } }
    [SerializeField]
    private float _lenth;
    public float lenth { get { return _lenth; } }
    [SerializeField]
    private string _Name;
    [SerializeField]
    private string _campName;

    public int _xp;
    public string campName { get { return _campName; } }

   // private int _spawnNum
   // public int SpawnNum { get { return _spawnNum; }  }

    private bool _isDead;
    //public  bool isDead { get { return _isDead; } }

    private int _MonsterNum;
    public int MonsterNum { get { return _MonsterNum; } }





}
