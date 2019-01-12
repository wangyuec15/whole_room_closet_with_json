using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {


    public GameObject parent;
    public Material mat;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void findandchange(){
        //doors=gameObject.AddComponent<IList>();
        //doors = new IList<>();

        parent = new GameObject();

        foreach (GameObject door in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (door.name == "door")
            {
                door.GetComponent<MeshRenderer>().material = mat;
            }
        }


    }


}
