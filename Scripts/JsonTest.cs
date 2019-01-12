using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DesignResponseDataModel;
public class JsonTest : MonoBehaviour
{
    string designJson;
    DesignData designData;
    // Use this for initialization

    public int room_amount;

    public int[] wallamount;


    public Vector3[] holeamount;

    public Vector3[][] corners;

    public int windows_amount;
    public int doors_amount;

    public Vector2[][] windows;
    public Vector2[][] doors;


    public float[] z;
    public float[] w;

    void Start()
    {
        ReadJsonData();

        getroomdata();
        allgenerate();
    }

    public void allgenerate(){
        //reverse();
        gameObject.GetComponent<drawline>().eachroom();

    }




    public void ReadJsonData()
    {
        designJson = File.ReadAllText(Application.streamingAssetsPath + "/javaJsonFamily.txt");
        //Debug.Log(designJson);
        designData = DesignData.FromJson(designJson);
        //Debug.Log(designData.Meta.Name);

    }

    public void getroomdata()
    {
        
        room_amount = designData.Rooms.Count;

        wallamount = new int[room_amount];
        corners = new Vector3[room_amount][];

        for (int i = 0; i < room_amount; i++)
        {
            wallamount[i] = (int)designData.Rooms[i].Meta.Surfaces.Count;
            corners[i] = new Vector3[wallamount[i]];
        }


        for (int i = 0; i < room_amount; i++)
        {
            wallamount[i] = (int)designData.Rooms[i].Meta.Surfaces.Count;
            for (int j = 0; j < wallamount[i]; j++)
            {
                corners[i][j] = new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].P1.X, 0, (float)designData.Rooms[i].Meta.Surfaces[j].P1.Y);
            }
         }
    }


    public void getdoors(int q){
        
        doors_amount = designData.Rooms[q].Doors.Count;
        doors = new Vector2[doors_amount][];

        for (int i = 0; i < doors_amount; i++)
        {
            doors[i] = new Vector2[2];
        }
        z = new float[doors_amount];
        w = new float[doors_amount];




        for (int i = 0; i < doors_amount; i++)
        {
            doors[i][0] = new Vector2((float)designData.Rooms[q].Doors[i].P1.X, (float)designData.Rooms[q].Doors[i].P1.Y);
            doors[i][1] = new Vector2((float)designData.Rooms[q].Doors[i].P2.X, (float)designData.Rooms[q].Doors[i].P2.Y);
            z[i] = 0;//(float)designData.Rooms[q].Doors[i].BayDepth;
            w[i] = (float)designData.Rooms[q].Doors[i].Height;

        }



    }





    public void getwindows(int q)
    {
        windows_amount = designData.Rooms[q].Windows.Count;
        windows = new Vector2[windows_amount][];

        for (int i = 0; i < windows_amount; i++)
        {
            windows[i] = new Vector2[2];
        }
        z = new float[windows_amount];
        w = new float[windows_amount];




        for (int i = 0; i < windows_amount; i++)
        {
            windows[i][0] = new Vector2((float)designData.Rooms[q].Windows[i].P1.X, (float)designData.Rooms[q].Windows[i].P1.Y);
            windows[i][1] = new Vector2((float)designData.Rooms[q].Windows[i].P2.X, (float)designData.Rooms[q].Windows[i].P2.Y);
            z[i] = (float)designData.Rooms[q].Windows[i].BayDepth;
            w[i] = (float)designData.Rooms[q].Windows[i].Height;

        }
    }





    public void reverse(){
        for (int i = 0; i < room_amount; i++)
        {
            Vector3 temp;
            for (int j = 0; j < corners[i].Length / 2; j++)
            {
                temp = corners[i][j];
                corners[i][j] = corners[i][corners[i].Length - j - 1];
                corners[i][corners[i].Length - j - 1] = temp;
            }
        }
    }











    // Update is called once per frame
    void Update()
    {

    }
}
