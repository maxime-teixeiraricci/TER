using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSliderVal : MonoBehaviour {


	public int valSliderExplorer;
	public int valSliderHeavy;
	public int valSliderLight;	
	
	public Slider exploSlider;
	public Slider heavySlider;
	public Slider lightSlider;
	// Use this for initialization
	void Start () {

		valSliderExplorer = (int)exploSlider.value;
		valSliderHeavy = (int)heavySlider.value;
		valSliderLight = (int)lightSlider.value;
		
	}
}
