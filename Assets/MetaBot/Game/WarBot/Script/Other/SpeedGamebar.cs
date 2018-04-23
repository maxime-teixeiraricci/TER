using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedGamebar : MonoBehaviour
{
    public Slider _slider;
    public Text _textvalue;
    public bool pause;
    public float currentValue;

    void Start()
    {
        currentValue = _slider.value;
    }
	void Update ()
    {
        Time.timeScale = _slider.value;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_slider.value == 0)
            {
                _slider.value = currentValue;
            }
            pause = !pause;
        }
        
        if (pause)
        {
            pause = !pause;
            _slider.value = 0;
        }
        else
        {
            
           currentValue = _slider.value;
            
        }
        _textvalue.text = "" + ((int)(_slider.value * 100))+"%" ;
    }
}
