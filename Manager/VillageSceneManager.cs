using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageSceneManager : MonoBehaviour
{
  //  [SerializeField]
  //  Canvas _canvas;

    private UImanager _uimanager;
   // Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _uimanager = FindObjectOfType<UImanager>();
       
     //   _canvas.worldCamera = _mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        //_mainCamera = Camera.main;
       // this.transform.forward = _mainCamera.transform.forward;
    }

    public void OnclickShop()
    {
        Debug.Log("상점클릭");
        _uimanager.OnclickShop();
    }

}
