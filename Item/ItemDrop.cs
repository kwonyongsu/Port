using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField]
    private List<Item> _Litem = new List<Item>();

    public int _total = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _Litem.Count; i++)
        {
            _total += _Litem[i]._weight;
        }
    }

    public Item Randomitem()
    {
        int _selectNum = 0;
        int _weight = 0;

        _selectNum = Mathf.RoundToInt(_total * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < _Litem.Count; i++)
        {
            _weight += _Litem[i]._weight;
            if (_selectNum <= _weight)
            {
                // Item _tempItem = new Item(_Litem[i]);
                // return _tempItem;
                return _Litem[i];
            }
        }

        return null;
       // return _Litem[Random.Range(0, _Litem.Count)];
    }



}
