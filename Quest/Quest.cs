using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "New Quest/Quest")]
public class Quest : ScriptableObject
{

    public QuestType _questType;
    public QuestPlace _questLoc;

  //  public Transform _questPoint;
    [SerializeField]
    public Item _questItem;
    [SerializeField]
    public Monster _monster;
    [SerializeField]
    public string _qusetPlace;

    [SerializeField]
    public List<Item> _rewardItem=new List<Item>();
    [SerializeField]
    public int rewardXp;
    [SerializeField]
    public int rewardGold;

    [SerializeField]
    public int _goal;
    public int _current;

    [SerializeField]
    public string _contents;

    public enum QuestPlace
    { 
        Village,
        GraveYard
    }

    public enum QuestType
    {
        Hunt,
        Move,
        Collect
    }



}
