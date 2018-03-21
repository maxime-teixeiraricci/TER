using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HUDManager : MonoBehaviour
{
    public GameObject _HUDObject;
    private bool _isCreated;

	// Update is called once per frame
	void Update ()
    {
		
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
