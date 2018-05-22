using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSettingMenu : MonoBehaviour
{
    [Header("Value")]
    public string _unitName;
    public int _number;
    public int _minNumber;
    public int _maxNumber;

    [Header("Link")]
    public Text _unitCounter;
    public Text _unitNameText;
    public Button _addButton;
    public Button _subButton;


    // Use this for initialization
    void Start ()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm._gameSettings._initStartUnit != null && gm._gameSettings._initStartUnit.ContainsKey(_unitName))
        {
            _number = GameObject.Find("GameManager").GetComponent<GameManager>()._gameSettings._initStartUnit[_unitName];
        }
        _unitCounter.text = _number.ToString("00");
        _unitNameText.text = _unitName;
    }

    // Use this for initialization
    public void Add()
    {
        _number = Mathf.Min(_maxNumber, _number + 1);
        _unitCounter.text = _number.ToString("00");
    }

    public void Sub()
    {
        _number = Mathf.Max(_minNumber, _number - 1);
        _unitCounter.text = _number.ToString("00");
    }


}
