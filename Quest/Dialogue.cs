using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dialogue : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image _dialogueBox;
    [SerializeField] Text _dialogueText;
    [SerializeField] Image _spirit;

    private bool _isDialogue;

    private int _count = 0;


    private List<string> _Lscript;

    List<Dictionary<string, object>> _data;

    private QuestManager _questManager=null;


    private void Awake()
    {
        _questManager = this.gameObject.GetComponent<QuestManager>();
    }

    private void Start()
    {
        _Lscript = new List<string>();
        _data = CSVReader.Read("dialogue");
    }

    public void Showdialogue(int Questnum)
    {
        UpdateDialogue(Questnum);
        StartCoroutine(DialogueCor());
        _dialogueBox.gameObject.SetActive(true);
        _spirit.gameObject.SetActive(true);
        _isDialogue = true;
        _count = 0;

        NextDialogue();
    }

    private void HideDialogue()
    {
        _dialogueBox.gameObject.SetActive(false);
        _spirit.gameObject.SetActive(false);
        _isDialogue = false;
        _Lscript.Clear();

    }
    public void UpdateDialogue(int _questNum)
    {
        string temp = _questNum.ToString();

        for (int i = 0; i < _data.Count; i++)
        {
            _Lscript.Add(_data[i][temp].ToString());

            //_dialogue[i].script = _data[i][temp].ToString();
            //Debug.Log("다이얼" + _dialogue[i].script);
            //List<Dictionary<string, object>> _data 여기에서 스트링과, 오브젝트는 키과 벨류고
            // _data[i][temp] 여기에 [][] 들어가는거는 인덱스다, [인덱스][키값]으로 찾아온다.
        }


    }


    private void NextDialogue()
    {
        _dialogueText.text = _Lscript[_count];
        _count++;

    }

    IEnumerator DialogueCor()
    {
        while (_isDialogue==false)
        {
            yield return null;
        }
      //  StopCoroutine(Dialogue());
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isDialogue)
        {
            if (_count < _Lscript.Count)
            {
                NextDialogue();
            }
            else
            {
                HideDialogue();
                _questManager.To();
            }

        }

    }
}
