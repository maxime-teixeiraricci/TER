using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedGamebar : MonoBehaviour
{
    public Slider _slider;
    public Text _textvalue;

	void Update ()
    {
        Time.timeScale = _slider.value;
        _textvalue.text = "" + ((int)(_slider.value * 100))+"%" ;
    }
}
