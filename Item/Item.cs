using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string _itemName;

    public Sprite _itemImage;

    public Itemtype _itemtype;

    public EqimentType _eqimenttype;

    public int _value;

    [SerializeField]
    private int _ability;
    public int ability {get{return _ability; }}

    public string weaponType;

    public int _weight;


    public enum EqimentType
    { 
        Sword,
        Hat,
        Armor,
        Boots,
        NONE
    }
    public enum Itemtype
    {
        Equiment,
        Potion,
        Scroll,
        ETC
    }
    
}
