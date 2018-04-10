using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUDStatScript : MonoBehaviour
{
    public GameObject unit;

    public Text _healthText;
    public Image _healthFillImage;
    public Image _bagFillImage;
    public Text _itemText;
    public Text _headingText;
    public HeadingShower headingShower;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        unit = Camera.main.GetComponent<SubjectiveCamera>().unit;
        if (unit != null)
        {
            Stats unitStats = unit.GetComponent<Stats>();
            _healthText.text = "" + unitStats._health + "/" + unitStats._maxHealth;
            _itemText.text = "" + unitStats.GetComponent<Inventory>()._actualSize + "/" + unitStats.GetComponent<Inventory>()._maxSize;
            _headingText.text = "" + (int)unitStats._heading + "°";
            headingShower.angle = unitStats._heading;

            _healthFillImage.fillAmount = 1.0f * unitStats._health / unitStats._maxHealth;
            _bagFillImage.fillAmount = 1.0f * unitStats.GetComponent<Inventory>()._actualSize / unitStats.GetComponent<Inventory>()._maxSize;
        }
    }
}
