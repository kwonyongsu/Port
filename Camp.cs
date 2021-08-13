using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    [SerializeField]
    private int _poolSize = 9;

    [SerializeField]
    private float _reSpawnTime = 5.0f;
    [SerializeField]
    private float _DeadTime = 2.0f;

    private float _curTime;

    // private List<GameObject> _enemyObjectPool;
    // public List<GameObject> Objectpool { get { return _enemyObjectPool; } }


    [SerializeField]
    private GameObject _monster;

    private Transform[] _spawnPoints;

    private Vector3[] _originPos;

    private MonsterHealth[] _monsterHealth;


    //=======================================================================//
    private void Awake()
    {
        //_monsterHealth = _monster.GetComponents<MonsterHealth>();
      

    }


    private void Start()
    {
        _spawnPoints = new Transform[_poolSize];
        _originPos = new Vector3[_poolSize];
        _monsterHealth = new MonsterHealth[_poolSize];

        for (int i = 0; i < _poolSize; i++)
        {
            _spawnPoints[i] = this.gameObject.transform.GetChild(i);

            //float a = Random.Range(-30f, 30f);
           // Quaternion qRotation = Quaternion.Euler(0f, 0f, a);

            GameObject Monsters = Instantiate(_monster, _spawnPoints[i].position,Quaternion.identity);
           // Monsters.transform.position = _spawnPoints[i].position;

            _monsterHealth[i] = Monsters.GetComponent<MonsterHealth>();
            _monsterHealth[i].respawnNum = i;
        }
    }



    private void Update()
    {
      
    }

    public void MonsterReSpawn(GameObject monster, int respawnNum)
    {

        StartCoroutine(MonsterRespawn(monster, respawnNum));

    }


    public IEnumerator MonsterRespawn(GameObject monster, int respawnNum)
    {
        yield return new WaitForSeconds(_DeadTime);
        monster.SetActive(false);

        yield return new WaitForSeconds(_reSpawnTime);
        monster.transform.position = _spawnPoints[respawnNum].position;
        monster.SetActive(true);
    }



}
