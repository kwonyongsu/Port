using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform _player;
   

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _player.transform.position;
    }
}
