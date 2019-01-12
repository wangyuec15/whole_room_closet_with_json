using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DesignResponseDataModel;

public class newJsonText : MonoBehaviour
{
    public int room_count;
    public int[] wall_count;


    public Vector3[][] walls;
    public float[][] wall_length;
    public Vector4[][] holes;
    private Vector3[][][] edge;
    private float[][][] sillheight;


    string designJson;
    DesignData designData;
    // Use this for initialization

    void Start()
    {
        ReadJsonData();
        find();
        reverse();
        count();
        //reverse2();
        gameObject.GetComponent<newDrawLine>().eachroom();
    }

    public void ReadJsonData()
    {
        designJson = File.ReadAllText(Application.streamingAssetsPath + "/javaJsonFamily.txt");
        //Debug.Log(designJson);
        designData = DesignData.FromJson(designJson);
        //Debug.Log(designData.Meta.Name);
    }

    void find()
    {

        room_count = designData.Rooms.Count;
        walls = new Vector3[room_count][];
        wall_length = new float[room_count][];
        wall_count = new int[room_count];
        holes = new Vector4[room_count][];
        edge = new Vector3[room_count][][];
        sillheight = new float[room_count][][];
        for (int i = 0; i < room_count; i++)
        {
            wall_count[i] = designData.Rooms[i].Meta.Surfaces.Count;
            walls[i] = new Vector3[wall_count[i]];
            wall_length[i] = new float[wall_count[i]];
            holes[i] = new Vector4[wall_count[i]];
            edge[i] = new Vector3[wall_count[i]][];
            sillheight[i] = new float[wall_count[i]][];
        }
        for (int i = 0; i < room_count; i++)
        {
            for (int j = 0; j < wall_count[i]; j++)
            {
                edge[i][j] = new Vector3[2];
                sillheight[i][j] = new float[2];
                walls[i][j] = new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].P1.X, 0, (float)-designData.Rooms[i].Meta.Surfaces[j].P1.Y);
                if (j != wall_count[i] - 1)
                {
                    wall_length[i][j] = Vector3.Distance(walls[i][j], walls[i][j + 1]);
                }else{
                    wall_length[i][j] = Vector3.Distance(walls[i][j], walls[i][0]);
                }
                if (designData.Rooms[i].Meta.Surfaces[j].Holes.Count == 1)
                {
                    edge[i][j][0] = new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P1.X, 0, -(float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P1.Y);
                    edge[i][j][1] = new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P2.X, 0, -(float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P2.Y);
                    sillheight[i][j][0] = (float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].SillHeight;
                    sillheight[i][j][1] = (float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].Height;
                }else{
                    edge[i][j][0] = new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].P1.X,0,-(float)designData.Rooms[i].Meta.Surfaces[j].P1.Y);
                    edge[i][j][1] = edge[i][j][0];
                    sillheight[i][j][0] = 0;
                    sillheight[i][j][1] = 1;
                }
            }



        }

    }

    void reverse()
    {
        for (int i = 0; i < room_count; i++)
        {
            Vector3 temp;
            Vector3[] edgetemp;
            edgetemp = new Vector3[2];
            float[] siltemp;
            siltemp = new float[2];

            for (int j = 0; j < wall_count[i] / 2; j++)
            {
                temp = walls[i][j];
                walls[i][j] = walls[i][wall_count[i] - 1 - j];
                walls[i][wall_count[i] - 1 - j] = temp;

                edgetemp = edge[i][j];
                edge[i][j] = edge[i][wall_count[i] - 1 - j];
                edge[i][wall_count[i] - 1 - j] = edgetemp;

                siltemp = sillheight[i][j];
                sillheight[i][j] = sillheight[i][wall_count[i] - 1 - j];
                sillheight[i][wall_count[i] - 1 - j] = siltemp;

            }

        }

    }

    void count(){

        for (int i = 0; i < room_count; i++)
        {
            for (int j = 0; j < wall_count[i]; j++)
            {
                if (j != wall_count[i] - 1)
                {
                    wall_length[i][j] = Vector3.Distance(walls[i][j + 1], walls[i][j]);
                
                }else{
                    wall_length[i][j] = Vector3.Distance(walls[i][0], walls[i][j]);
                }

                if (j != wall_count[i] - 1)
                {

                    float d1 = Vector3.Distance(edge[i][j + 1][1], walls[i][j]);
                    float d2 = Vector3.Distance(edge[i][j + 1][0], walls[i][j]);

                    holes[i][j] = new Vector4(d1, d2, sillheight[i][j + 1][0], sillheight[i][j + 1][1]+sillheight[i][j+1][0]);
                    if(edge[i][j][0]!=Vector3.zero)
                        Debug.DrawLine(walls[i][j] + (walls[i][j + 1] - walls[i][j]) / wall_length[i][j] * d1+ new Vector3(0, holes[i][j].z, 0), walls[i][j] + (walls[i][j + 1] - walls[i][j]) / wall_length[i][j] * d2+new Vector3(0,holes[i][j].w,0), Color.cyan, 2000);
                }
                else
                {
                    float d1 = Vector3.Distance(edge[i][0][1], walls[i][j]);
                    float d2 = Vector3.Distance(edge[i][0][0], walls[i][j]);

                    holes[i][j] = new Vector4(d1, d2, sillheight[i][0][0], sillheight[i][0][1]+sillheight[i][0][0]);
                    if(edge[i][j][0]!=Vector3.zero)
                        Debug.DrawLine(walls[i][j] + (walls[i][0] - walls[i][j]) / wall_length[i][j] * d1+ new Vector3(0, holes[i][j].z, 0), walls[i][j] + (walls[i][0] - walls[i][j]) / wall_length[i][j] * d2+new Vector3(0,holes[i][j].w,0), Color.cyan, 2000);
               
                }
                //if (edge[i][j][0] != Vector3.zero)
                //{
                //    Debug.DrawLine(edge[i][j][0] + new Vector3(0, 3000, 0), walls[i][j] + new Vector3(0, 3000, 0), Color.red, 2000);
                //    Debug.DrawLine(edge[i][j][1] + new Vector3(0, 2900, 0), walls[i][j] + new Vector3(0, 2900, 0), Color.yellow, 2000);
                //    if (j != wall_count[i] - 1 && j != 0)
                //    {
                //        Debug.DrawLine(walls[i][j] + new Vector3(0, 2950, 0), walls[i][j + 1] + new Vector3(0, 2950, 0), Color.cyan, 2000);
                //        Debug.DrawLine(walls[i][j] + new Vector3(0, 2950, 0), walls[i][j - 1] + new Vector3(0, 2950, 0), Color.white, 2000);
                //    }
                //}



                Debug.Log("exist");
                //}
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < room_count; i++)
        {
            for (int j = 0; j < wall_count[i]; j++)
            {
                if (j != wall_count[i] - 1)
                {
                    Debug.DrawLine(walls[i][j], walls[i][j + 1], Color.green, 20f);
                }
                else
                {
                    Debug.DrawLine(walls[i][j], walls[i][0], Color.green, 20f);
                }
            }
        }
        for (int i = 0; i < room_count; i++)
        {
            for (int j = 0; j < wall_count[i]; j++)
            {

                if (j != wall_count[i] - 1)
                {
                    Debug.DrawLine(walls[i][j]+Vector3.down, walls[i][j + 1]+Vector3.down, Color.green, 20f);

                   
                }
                else
                {
                    Debug.DrawLine(walls[i][j]+Vector3.down, walls[i][0]+Vector3.down, Color.green, 20f);
                }
                if (designData.Rooms[i].Meta.Surfaces[j].Holes.Count != 0)
                {
                    Debug.DrawLine(new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P1.X, -2, -(float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P1.Y), new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P2.X, -2, -(float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P2.Y), Color.blue, 2f);
                    Debug.DrawLine(new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P1.X, -2, -(float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P1.Y), new Vector3((float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P2.X, -2, -(float)designData.Rooms[i].Meta.Surfaces[j].Holes[0].P2.Y), Color.blue, 2f);
                }

            }
        }
    
    }
}
