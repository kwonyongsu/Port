using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform _target;

    void Update()
    {
        this.transform.forward = _target.forward;
    }
}
