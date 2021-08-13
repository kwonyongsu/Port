using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [SerializeField]
    private int _destination;
    private GameObject _player;
    private Movement _playerMove;
    private Pad _pad;
    private AutoMove _autoMove;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _pad = GameObject.FindObjectOfType<Pad>();

        _playerMove = _player.GetComponent<Movement>();
         _autoMove = _player.GetComponent<AutoMove>();
        _playerMove._agent.enabled = false;
        _player.gameObject.transform.position = this.gameObject.transform.position;
        _playerMove._agent.enabled = true;
        _pad._dir = Vector3.zero;
        _pad._PlayerDir = Vector3.zero;
        _pad._stick.localPosition = Vector2.zero;
         _autoMove.Destination(_destination);
    }

}
