using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EqSlot : Slot
{

    public override void Additem(Item _additem, int _count = 1)
    {
        _item = _additem;
        _itemCount = _count;
        _itemimage.sprite = _item._itemImage;//
        _textCount.text = _itemCount.ToString();


        SetColor(1);
    }

    

}
