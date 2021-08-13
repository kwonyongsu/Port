using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class AutoMove : MonoBehaviour
{
    //[SerializeField]
    //private GameObject _player;
    //NavMeshAgent _agent;
    private Movement _playerMove;
    private  List<Dictionary<string, object>> _placeData;
    public string _destination;
    public bool _isAutoMove;
    public CurrPlace _currPlace;

    [SerializeField]
    Pad _pad;


    public enum CurrPlace
    {
        Village,
        GraveYard,
    }


    private void Start()
    {
        //_agent = _player.GetComponent<Movement>()._agent;
        _playerMove = this.gameObject.GetComponent<Movement>(); ;
        _placeData = CSVReader.Read("Place");

        _currPlace = CurrPlace.GraveYard;
    }

    public void onclickMove()
    {
        _playerMove._agent.destination = StringToVector3(_placeData[(int)_currPlace][_destination].ToString());
    }


    public void AutoMoving(string key)
    {
        _playerMove._agent.destination = StringToVector3(_placeData[(int)_currPlace][key].ToString());

    }
    public Vector3 QuestLocation(string key)
    {
       return StringToVector3(_placeData[(int)_currPlace][key].ToString());
    }

  

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split('^');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;

    }





    private void Update()
    {
        // Temp=StringToVector3(a);
        //if (_isAutoMove)
        //{
        //    _playerMove._agent.destination = StringToVector3(_placeData[(int)_currPlace][_destination].ToString());
        //}

        if (_pad._isInput)
        {
            _isAutoMove = false;
            return;
        } 


        if (_isAutoMove)
        {
            AutoMoving(_destination);
        }



        if (Input.touchCount > 0)
        {
            _isAutoMove = false;
        }


    }


    public void Destination(int value)
    {
        switch (value)
        {
            case 0:
                _currPlace = CurrPlace.Village;
                break;
            case 1:
                _currPlace = CurrPlace.GraveYard;
                break;
            default:
                break;
        }

    }

  
}
