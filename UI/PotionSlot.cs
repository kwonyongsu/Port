using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionSlot : Slot
{
    [SerializeField]
    PlayerHealth _ph;

    public override void OnPointerDown(PointerEventData eventData)
    {
        UsePotion();
        //포션먹기
        return;
    }

    public void UsePotion()
    {
        _ph.Heal(_item.ability);
        SetSlotCount(-1);
        _ph._playerHp.value = _ph._value;
    }



    public override void ChangeSlot()
    {
        if (_item == null)
        {
            Additem(TempPotionSlot.instance._tempPotionSlot._item, TempPotionSlot.instance._tempPotionSlot._itemCount);
            TempPotionSlot.instance._tempPotionSlot.ClearSlot();
        }
        else 
        {
            Item _tempitem = _item;
            int _tempItemCount = _itemCount;


            Additem(TempPotionSlot.instance._tempPotionSlot._item, TempPotionSlot.instance._tempPotionSlot._itemCount);
            TempPotionSlot.instance._tempPotionSlot.Additem(_tempitem, _tempItemCount);

        }


    }












}
