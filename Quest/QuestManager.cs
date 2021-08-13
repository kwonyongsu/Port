using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestManager : MonoBehaviour
{
    private int _questIndex=0;
    public int QuestIndex { get { return _questIndex; } }
    private QuestState _questState;
    public bool[] _isQuest;
    private bool _isClear;

    private Movement _playermove;
  //  private Vector2[] _questdest; // _questdest, _questdest[0] ~ _questdest[5]
  //  private Vector2 _playerPos;
    private float _questDistance;
    private AutoManager _auto;
    private AutoMove _autoMove;

    private bool _autoQuest = true;

    [SerializeField]
    GameObject _player;
    [SerializeField]
    private List<Quest> _questList = new List<Quest>();
    [SerializeField]
    private Text _textQuestContents;
    [SerializeField]
    private Text _textGoal;
    [SerializeField]
    private Image _clearWindow;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private List<Image> _rewardImage=new List<Image>();
    [SerializeField]
    private List<Text> _rewardText = new List<Text>();
    [SerializeField]
    private GameObject _questClear;
    [SerializeField]
    private GameObject _questMark;
    [SerializeField]
    private Pad _pad;


    Vector3 _questDis=new Vector3();
    enum QuestState
    {
        Begin,
        To,
        End
    }

    private Dialogue _dialogue;


    private void Awake()
    {
        _dialogue = this.gameObject.GetComponent<Dialogue>();
        _playermove = _player.GetComponent<Movement>();
        _isQuest = new bool[3];
        _auto = _player.GetComponent<AutoManager>();
        _autoMove = _player.GetComponent<AutoMove>();
    }
    private void Start()
    {
        //_questdest = new Vector2[_questList.Count];

        //for (int i = 0; i < _questList.Count; i++)
        //{
        //    _questdest[i] = new Vector2(_questList[i]._questPoint.transform.position.x, _questList[i]._questPoint.transform.position.z); 
        //}

        //_playerPos = new Vector2();
    }


    public void Begin(int questIndex)
    {
        _dialogue.Showdialogue(questIndex);
    }

    public void To()
    {

        _questState = QuestState.To;
        _textQuestContents.text = _questList[_questIndex]._contents;
      

        switch (_questList[_questIndex]._questType)
        {
            case Quest.QuestType.Hunt:
                _isQuest[0] = true;
                _textGoal.text = _questList[_questIndex]._current + "/" + _questList[_questIndex]._goal;
                break;
            case Quest.QuestType.Move:
                _isQuest[1] = true;
                _textQuestContents.text = _questList[_questIndex]._contents;
                break;
            case Quest.QuestType.Collect:
                _isQuest[2] = true;
                _textGoal.text = _questList[_questIndex]._current + "/" + _questList[_questIndex]._goal;
                break;
            default:
                break;
        }


       
    }
    public void End(int questIndex)
    {
        _questState = QuestState.End;
        //메인퀘스트 텍스트 on
    }

    public void OnclickQuestBar()
    {
        switch (_questState)
        {
            case QuestState.Begin:

                Begin(_questIndex);

                break;
            case QuestState.To:

                if (_isClear)
                {
                    //클리어창띄우고
                    _questClear.SetActive(true);
                    End(_questIndex);

                    for (int i = 0; i < _isQuest.Length; i++)
                    {
                        _isQuest[i] = false;
                    }

                    for (int i = 0; i < _questList[_questIndex]._rewardItem.Count; i++)
                    {
                        _inventory.EnterItem(_questList[_questIndex]._rewardItem[i], 1);
                        _rewardImage[i].sprite = _questList[_questIndex]._rewardItem[i]._itemImage;
                        _rewardText[i].text = _questList[_questIndex]._rewardItem[i]._itemName;
                    }

                    PlayerDataBase.instance.Xp += _questList[_questIndex].rewardXp;

                    _isClear = false;
                    _questMark.SetActive(false);
                    _questIndex++;
                    _questState = QuestState.Begin;

                }
                else
                {
                    StartCoroutine(AutoQuest());
                }


                break;
            case QuestState.End:
              


                break;
            default:
                break;
        }


    }


    public void HuntQuest(Monster monster)
    {
        //if (_questList[_questIndex]._questType != Quest.QuestType.Hunt) return;
        if (monster == _questList[_questIndex]._monster)
        {
            _questList[_questIndex]._current++;
            if (_questList[_questIndex]._current >= _questList[_questIndex]._goal)
            {
                _isClear = true;
                _questMark.SetActive(true);
            }
        }
            _textQuestContents.text = _questList[_questIndex]._contents;
            _textGoal.text = _questList[_questIndex]._current + "/" + _questList[_questIndex]._goal;
    }

    public void Collect(Item item)
    {
        if (_questList[_questIndex]._questType != Quest.QuestType.Collect) return;
        if (item== _questList[_questIndex]._questItem)
        {
            _questList[_questIndex]._current++;
        }
            _textQuestContents.text = _questList[_questIndex]._contents;
            _textGoal.text = _questList[_questIndex]._current + "/" + _questList[_questIndex]._goal;

    }

    public void MoveQuest()
    {
        _textQuestContents.text = _questList[_questIndex]._contents;


       // return _questList[_questIndex]._questPoint.transform;
    }

    IEnumerator AutoQuest()
    {
        while (true)
        {
            if (_pad._isInput) StopAllCoroutines();


            if ((int)_autoMove._currPlace == (int)_questList[_questIndex]._questLoc)
            {
                _questDis = _autoMove.QuestLocation(_questList[QuestIndex]._qusetPlace);
                Vector3 playerpoint = new Vector3(_playermove._agent.transform.position.x,_playermove._agent.transform.position.y ,_playermove._agent.transform.position.z);
                
                _questDistance = Vector3.Distance(_questDis, playerpoint);//사거리

                if (_questDistance <= 5.0f)
                {
                    if (_isQuest[1])
                    {
                        _isClear = true;
                        _questMark.SetActive(true);
                    }
                    else {

                        _auto.isAuto = true;
                    }
                   
                    StopAllCoroutines();
                }


            }

            //_playermove._agent.destination=_questList[_questIndex]._questPoint.transform.position;  //위치 매니저로 바꾸자
            _autoMove._destination = _questList[_questIndex]._qusetPlace;
            _autoMove._isAutoMove = true;
            //_autoMove.AutoMoving(_questList[_questIndex]._qusetPlace);

            // _questDistance= Vector2.Distance(_playerPos,_questdest[_questIndex]);


          //  Vector2 questPoint = new Vector2(_questList[_questIndex]._questPoint.transform.position.x, _questList[_questIndex]._questPoint.transform.position.z);
          //  Vector2 playerpoint = new Vector2(_playermove._agent.transform.position.x, _playermove._agent.transform.position.z);
           
            yield return null;
        }

    }

   
}
