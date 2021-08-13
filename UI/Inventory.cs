using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject _SlotParent;

    private Slot[] _slot;

    

    private void Awake()
    {
        _slot = _SlotParent.GetComponentsInChildren<Slot>();
    }


    void Update()
    {
        
    }

    public void EnterItem(Item _item, int _count )
    {
        if (Item.Itemtype.Equiment != _item._itemtype)
        {
            //Debug.Log("사용 아이템");
            for (int i = 0; i < _slot.Length; i++)
            {
                if (_slot[i]._item != null)
                {
                    // 같은 아이템 있으면 카운터만 올리기
                    if (_slot[i]._item._itemName == _item._itemName)
                    {
                    _slot[i].SetSlotCount(_count);
                    return;
                    }
                }
            }

        }

        for (int i = 0; i < _slot.Length; i++)
        {
            if (_slot[i]._item == null)
            {
                 _slot[i].Additem(_item, _count);
                 return;
            }
             
               
        }
        
    }

}
