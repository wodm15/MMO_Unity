using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    [SerializeField] 
    Text _text;

    int _score = 0;
    public void OnButtonClick()
    {
        _score++;
        _text.text = $" score : {_score}";
    }

}
