using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitTest : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public float speed = 1;
    private float timer;

    // Use this for initialization
    void Start ()
    {
        print("All area : " + NavMesh.AllAreas);
        print("Walkable : " + NavMesh.GetAreaFromName("Walkable"));
        print("Water : " + NavMesh.GetAreaFromName("Water"));
        print("Jump : " + NavMesh.GetAreaFromName("Jump"));
        

        this.navMeshAgent = GetComponent<NavMeshAgent>();
        this.navMeshAgent.destination = new Vector3(Random.Range(-15f, 15f), 5, Random.Range(-15f, 15f));
        print("Current : " + this.navMeshAgent.areaMask);
        Time.timeScale = 1.0f;
        timer = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!this.navMeshAgent.hasPath)
        {
            this.navMeshAgent.destination = new Vector3(Random.Range(-15f, 15f), 5, Random.Range(-15f, 15f));
        }
        Debug.DrawRay(transform.position, this.navMeshAgent.destination - transform.position);

        this.navMeshAgent.speed = speed;
	}
}
