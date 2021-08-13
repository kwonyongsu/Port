using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Shop : MonoBehaviour,IPointerDownHandler
{
    private UImanager _uimanager;

   // Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _uimanager = FindObjectOfType<UImanager>();
      //  _mainCamera = Camera.main;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("상점클릭");
        _uimanager.OnclickShop();
    }

    private void Update()
    {
      //  this.transform.forward = _mainCamera.transform.forward;
    }

}
