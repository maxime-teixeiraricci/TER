using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTeam : MonoBehaviour
{
    public GameObject window;


    public void NameInput()
    {
        window.SetActive(true);
    }

    public void disableWindow()
    {
        window.SetActive(false);
    }

    public void validateName()
    {
        window.SetActive(false);

    }
}


