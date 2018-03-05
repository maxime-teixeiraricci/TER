using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour {

	GameObject currentObject = ManageDragAndDrop.currentObject;

    public void destroyItem()
    {
        if(currentObject != null)
        {
            Destroy(currentObject);
            Debug.Log("CURRENTITEM = " + currentObject);
        }
    }

}
