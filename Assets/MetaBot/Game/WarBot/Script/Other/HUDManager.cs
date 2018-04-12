using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUDManager : MonoBehaviour
{
    public GameObject _HUDObject;
    public Image _StatsButton;
    public Image _MessageButton;

    private bool _isCreated;



    public bool messageOn;
    public bool statsOn;



    void Update()
    {
        foreach (HP_HUDManager hud in Resources.FindObjectsOfTypeAll<HP_HUDManager>())
        {
            hud.transform.gameObject.SetActive(statsOn);
        }

        foreach (MessageLineScript message in Resources.FindObjectsOfTypeAll<MessageLineScript>())
        {
            message.transform.gameObject.SetActive(messageOn);
        }
    }

    public void MessageButton()
    {
        messageOn = !messageOn;
        _MessageButton.color = (messageOn) ? Color.green : Color.red;
    }

    public void StatsButton()
    {
        statsOn = !statsOn;
        _StatsButton.color = (statsOn) ? Color.green : Color.red;
    }
	

    public void CreateHUD(GameObject unit)
    {
        GameObject _hud = Instantiate(_HUDObject, transform);
        _hud.GetComponent<HP_HUDManager>()._target = unit;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }


}
