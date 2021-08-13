using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public InputField _id;
    public InputField _password;

    public Text _notify;

    public void SaveUserData()
    {
        if (CheckInput(_id.text, _password.text) == false) return;

        if (PlayerPrefs.HasKey(_id.text) == false)
        {
            PlayerPrefs.SetString(_id.text, _password.text);
            _notify.text = "아이디 생성 완료.";
        }
        else
        {
            _notify.text = "이미 존재하는 아이디 입니다.";
        }
    }

    public void CheckUserData()
    {
        if (CheckInput(_id.text, _password.text) == false) return;

        string pass = PlayerPrefs.GetString(_id.text);

        if (_password.text == pass)
        {
            LoadingSceneManager.LoadScene("GraveYard");
        }
        else
        {
            _notify.text = "패드워드가 일치하지 않습니다.";
        }
    }

    bool CheckInput(string id, string pwd)
    {
        if (id == "" || pwd == "")
        {
            _notify.text = "아이디, 패스워드를 입력해 주세요";
            return false;
        }
        else
        {
            return true;
        }
    }

}
