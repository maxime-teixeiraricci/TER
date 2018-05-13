using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberPlayers : MonoBehaviour {

    public Dropdown dropdown;
    public GameObject Team1;
    public GameObject Team2;
    public GameObject Team3;
    public GameObject Team4;
    GameObject vs;
    Vector3 initialPos1, position1_2;
    Vector3 initialPos2, position2_2;
    Vector3 initialPos3, position3_3;
    Vector3 initialPlaceT4;
    float x1, y1, z1;
    string nbrPlayers;

    void Start()
    {
        vs = GameObject.Find("VS");
        float x1 = Team1.transform.position.x;
        float y1 = Team1.transform.position.y;
        float z1 = Team1.transform.position.z;
        initialPos1 = new Vector3(Team1.transform.position.x, Team1.transform.position.y, Team1.transform.position.z);
        initialPos2 = new Vector3(Team2.transform.position.x, Team2.transform.position.y, Team2.transform.position.z);
        initialPos3 = new Vector3(Team3.transform.position.x, Team3.transform.position.y, Team3.transform.position.z);

        dropdown.value = 0;
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text.Equals(GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count.ToString()))
            {
                dropdown.value = i;
            }
        }
    }

    // Met à jour la position des cadres des équipes, en fonction du nombre d'équipes présentes
    private void Update()
    {
        nbrPlayers = dropdown.captionText.text;
        if (nbrPlayers == "2")
        {
            Team1.transform.position = new Vector3(Team1.transform.position.x, vs.transform.position.y, Team1.transform.position.z);
            position1_2 = new Vector3(Team1.transform.position.x, vs.transform.position.y, Team1.transform.position.z);
            Team2.transform.position = new Vector3(Team2.transform.position.x, vs.transform.position.y, Team2.transform.position.z);
            position2_2 = new Vector3(Team2.transform.position.x, Team2.transform.position.y, Team2.transform.position.z);
        }
        
        
        if(nbrPlayers == "3")
        {
            Team1.transform.position = initialPos1;
            Team2.transform.position = initialPos2;
            Team3.transform.position = new Vector3(vs.transform.position.x, Team3.transform.position.y, Team3.transform.position.z);
            position3_3 = new Vector3(vs.transform.position.x, Team3.transform.position.y, Team3.transform.position.z);

        }

        if(nbrPlayers == "4")
        {
            Team1.transform.position = initialPos1;
            Team2.transform.position = initialPos2;
            Team3.transform.position = initialPos3;

        }
    }
}
