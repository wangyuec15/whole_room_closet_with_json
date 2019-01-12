using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class interact : MonoBehaviour {

    public GameObject go;
    public GameObject choosedoorstyle;
    public GameObject choosematerial;
    public GameObject closetdepth;
    private GameObject todestroy;
    private int room_amount=11;


    public Material dark, lightwood, chocolate;


	// Use this for initialization
	void Start () {
        go.GetComponent<Button>().onClick.AddListener(MyAction);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MyAction(){

        todestroy = new GameObject();
        foreach (GameObject rooms in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            for (int i = 0; i < room_amount; i++)
            {
                if (rooms.name == "room"+i)
                {
                    rooms.transform.parent = todestroy.transform;
                }
            }
        }
        Destroy(todestroy);





        if (choosedoorstyle.GetComponent<Dropdown>().value == 0)
        {
            gameObject.GetComponent<basicgenerate>().mystyle = basicgenerate.door_style.swing;
        }
        else if (choosedoorstyle.GetComponent<Dropdown>().value == 1)
        {
            gameObject.GetComponent<basicgenerate>().mystyle = basicgenerate.door_style.sliding;
        }






        if(choosematerial.GetComponent<Dropdown>().value==0){
            gameObject.GetComponent<ChangeMaterial>().mat = dark;
        }else if (choosematerial.GetComponent<Dropdown>().value == 1)
        {
            gameObject.GetComponent<ChangeMaterial>().mat = lightwood;
        }else if (choosematerial.GetComponent<Dropdown>().value == 2)
        {
            gameObject.GetComponent<ChangeMaterial>().mat = chocolate;
        }

        gameObject.GetComponent<cornercloset>().d1=closetdepth.GetComponent<Slider>().value * 10;
        gameObject.GetComponent<cornercloset>().d2 = closetdepth.GetComponent<Slider>().value * 10;

        gameObject.GetComponent<basicgenerate>().depth = closetdepth.GetComponent<Slider>().value*10;






        gameObject.GetComponent<JsonTest>().allgenerate();

        //gameObject.transform.position = new Vector3(6000, 15000, 7000);
        gameObject.GetComponent<Rotate>().target.transform.position = new Vector3(6000, 15000, -7000);

    }



}
