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
            _healthText.text = "" + unitStats.GetHealth() + "/" + unitStats.GetMaxHealth();
            _itemText.text = "" + unitStats.GetComponent<Inventory>()._actualSize + "/" + unitStats.GetComponent<Inventory>()._maxSize;
            _headingText.text = "" + (int)unitStats.GetHeading() + "°";
            headingShower.angle = unitStats.GetHeading();

            _healthFillImage.fillAmount = 1.0f * unitStats.GetHealth() / unitStats.GetMaxHealth();
            _bagFillImage.fillAmount = 1.0f * unitStats.GetComponent<Inventory>()._actualSize / unitStats.GetComponent<Inventory>()._maxSize;
        }
    }
}
