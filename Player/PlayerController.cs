using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool _isAttack = false;



    public GameObject _target = null;

    private Attack _attkack = null;

    private SkillManager _SkillManager;



    private void Awake()
    {
        _attkack = this.GetComponent<Attack>();
        _SkillManager = this.GetComponent<SkillManager>();
    }

    void Start()
    {

    }

    void Update()
    {


    }



    private void OnCollisionEnter(Collision collision)
    {



    }

    //public void SearchTarget()
    //{
    //    Collider[] Monsters = Physics.OverlapSphere(this.transform.position, _radius, 1 << 9);

    //    float ShortestTarget = Mathf.Infinity;

    //    foreach (Collider Monster in Monsters)
    //    {
    //        float Distance = Vector3.Distance(this.transform.position, Monster.gameObject.transform.position);
    //        if (Distance < ShortestTarget)
    //        {
    //            ShortestTarget = Distance;
    //            _target = Monster.gameObject;
    //        }
    //    }
    //}




}
