using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TeamPlayManagerScript : MonoBehaviour
{
    public int teamIndex;
    int id = 0;
    // Use this for initialization


    void Start ()
    {
        UpdateUnit();

        if(!(teamIndex < GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count))
        {
            Destroy(gameObject);
        }
        
    }
	

    public void UpdateUnit()
    {
        id = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            
            Stats statsChild = child.GetComponent<Stats>();
            statsChild._teamIndex = teamIndex;
            child.GetComponent<ColorChange>().ChangeColor();
            child.GetComponent<Brain>().LoadBehaviour();
            child.name = "[" + (teamIndex+1) + "]" + statsChild._unitType + " #" + id;
            id++;

        }
    }



#if UNITY_EDITOR
    void Update()
    {
        gameObject.transform.position = Vector3.zero;
    }
#endif



}
