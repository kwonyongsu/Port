using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pad : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField]
    private GameObject _player;

    public RectTransform _pad;
    public RectTransform _stick;
    public Vector3 _dir;
    public Vector3 _PlayerDir;
    public bool _isInput;

   // public Transform _player;
    Movement _move;
    SkillManager _skillManager;

    private void Awake()
    {
        _move = _player.GetComponent<Movement>();
        _skillManager = _player.GetComponent<SkillManager>();
    }
    void Update()
    {
        if (_skillManager.IsUseSkill) return;
        if (_isInput)
        {
            _PlayerDir = new Vector3(_dir.x + _player.transform.position.x, _player.transform.position.y, _player.transform.position.z + _dir.y);
            _move.Begin(_PlayerDir);
        }
    }
    //오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    //하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않는다


    public void OnDrag(PointerEventData eventData)
    {
        _stick.position = eventData.position;

        _stick.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)_pad.position, _pad.rect.width * 0.5f);
        _isInput = true;

        _dir = eventData.position;
        _dir.Normalize();
      
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pad.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _stick.localPosition = Vector2.zero;
        _isInput = false;

        _dir = Vector3.zero;
        _PlayerDir = Vector3.zero;
    }

  



}
