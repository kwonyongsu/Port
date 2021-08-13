using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_Bar : MonoBehaviour
{
    [SerializeField] GameObject _hpBar;

    List<Transform> _Lobject=new List<Transform>();
    List<GameObject> _LhpBar = new List<GameObject>();

    Camera _mainCamera;
    private void Start()
    {
        Debug.Log("체력바");

        _mainCamera = Camera.main;
        GameObject[] _tagObject = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = 0; i < _tagObject.Length; i++)
        {
            _Lobject.Add(_tagObject[i].transform);
            GameObject _tagHpbar = Instantiate(_hpBar, _tagObject[i].transform.position, Quaternion.identity, transform);
            _LhpBar.Add(_tagHpbar);
        }

      
    }
 

    // Update is called once per frame
    void Update()
    {
        
        
        for (int i = 0; i < _Lobject.Count; i++)
        {
            _LhpBar[i].transform.position = _mainCamera.WorldToScreenPoint(_Lobject[i].position + new Vector3(0, 1.15f, 0));
        }
    }
}
