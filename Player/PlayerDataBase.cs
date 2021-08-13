using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataBase : MonoBehaviour
{

    public static PlayerDataBase instance = null;

    private int _maxXp;
    public int maxHp { get { return _maxXp; } }
    private int _xp;
    public int Xp { get { return _xp; }set { _xp = value; } }
    private int _maxHp;
    public int MaxHp { get { return _maxHp; } }
    private int _hp;
    public int Hp { get { return _hp; } }
    private int _atk;
    public int Atk { get { return _atk; } }
    private int _def;
    public int Def { get { return _def; } }
    private int _gold;
    public int Gold { get { return _gold; }set {  _gold=value; } }
    private int _skillPoint;
    public int SkillPoint { get { return _skillPoint; } }
    private int _level=1;
    public int Level { get { return _level; } }
    [SerializeField]
    GameObject _levelup;
    [SerializeField]
    Text _levelText;

    public int starting = 0;

    List<Dictionary<string, object>> _data;
    private PlayerHealth _playerHealth = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        _playerHealth= this.gameObject.GetComponent<PlayerHealth>();
        _data = CSVReader.Read("PlayerStats");

        _maxHp = (int)_data[0]["MaxHp"];
        _maxXp = (int)_data[0]["MaxXp"];
        _atk = (int)_data[0]["Atk"];
        _def = (int)_data[0]["Def"];

        _playerHealth._maxvalue = _maxHp;
        _playerHealth._value = _maxHp;
        _playerHealth.Levelup(_maxHp);

        _gold = 1000;
    }

    public void Experience(int xp)
    {
        _xp += xp;


        if (_xp >= _maxXp)
        {
            _level++;
            _maxHp = (int)_data[_level]["MaxHp"];
            _maxXp = (int)_data[_level]["MaxXp"];
            _atk = (int)_data[_level]["Atk"];
            _def = (int)_data[_level]["Def"];
            _levelup.SetActive(true);
            _levelText.text = _level.ToString();
            _xp = 0;
            _playerHealth.Levelup(_maxHp);
        }
    
    }


}
