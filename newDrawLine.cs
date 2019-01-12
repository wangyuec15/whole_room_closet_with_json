using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newDrawLine : MonoBehaviour {
    
    public Vector3[] room_outline;
    public int room_amount;

   // public int n;
    //private float[] length;
    public float[][] closet_length;
    public float[] angel;
    public float[] turn;

    private Vector3[] offset;
    private float w1;
    private float depth;
    private Vector4[] hole;


    //private int room_amount;
    //private float depth;
    //private float w1;
    private int[] wall_count;

    private Vector3[][] walls;
    private float[][] wall_length;
    private Vector4[][] holes;


    // Use this for initialization
    void Start()
    {

    }


    public void eachroom()
    {

        room_amount = gameObject.GetComponent<newJsonText>().room_count;
        depth = gameObject.GetComponent<basicgenerate>().depth;
        w1 = gameObject.GetComponent<cornercloset>().w1;
        wall_count = gameObject.GetComponent<newJsonText>().wall_count;

        wall_length = gameObject.GetComponent<newJsonText>().wall_length;
        holes = gameObject.GetComponent<newJsonText>().holes;
        closet_length = wall_length;
        walls = gameObject.GetComponent<newJsonText>().walls;

        for (int i = 0; i < room_amount; i++)
        {
            GameObject thisroom = new GameObject("room" + i);
            gameObject.GetComponent<basicgenerate>().aroom = thisroom;
            gameObject.GetComponent<cornercloset>().aroom = thisroom;


            set(i);
            setaroom(i);

            set_turn(i);
            //line();
            //sethole();

            int q = 0;

            for (int j = 0; j < wall_count[i]; j++)
            {
                if (wall_length[i][j] > 10)
                    q += 1;
                //if (holes[i][j] != Vector4.zero)
                //{
                //    Debug.Log(i);
                //    Debug.Log(j);
                //    Debug.Log(holes[i][j]);
                //}
            }

                generate(i);

        }
        //
        //gameObject.GetComponent<ChangeMaterial>().findandchange();



    }


    public void set(int i)
    {

        room_outline = walls[i];//  new Vector3[wall_count[i]];

        for (int j = 0; j < wall_count[i]; j++)
        {
            room_outline[j] = walls[i][j];//gameObject.GetComponent<JsonTest>().corners[i][j];
        }
    }

    void setaroom(int i)
    {

        offset = new Vector3[wall_count[i]];
        //length = new float[wall_count[i]];
        //closet_length = new float[wall_count[i]];
        turn = new float[wall_count[i]];
        angel = new float[wall_count[i]];
        hole = new Vector4[wall_count[i]];
    }



    public void SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n, int i)
    {
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
        float signed_angle = angle * sign;
        turn[i] = (signed_angle <= 0) ? 360 + signed_angle : signed_angle;
        //270直角 90优角
    }


    public void set_turn(int i)
    {
        for (int j = 0; j < wall_count[i]; j++)
        {
            if (j == 0)
            {
                SignedAngleBetween(room_outline[j] - room_outline[wall_count[i] - 1], room_outline[j+ 1] - room_outline[j], Vector3.up, j);
            }
            else if (j == wall_count[i] - 1)
            {
                SignedAngleBetween(room_outline[j] - room_outline[j - 1], room_outline[0] - room_outline[j], Vector3.up, j);
            }
            else
            {
                SignedAngleBetween(room_outline[j] - room_outline[j - 1], room_outline[j + 1] - room_outline[j], Vector3.up, j);

            }


        }
    }



    public void set_closet_length(int i)
    {
        


        for (int j = 0; j < wall_count[i]; j++)
        {
            
            //if (turn[j] - 270 < 1 && turn[j] - 270 > -1)
            //{
            //    offset[j] = new Vector3(w1, 0, 0);
            //    closet_length[i][j] -= w1;

            //    if (j == wall_count[i]-1)
            //    {
            //        closet_length[i][0] -= w1;
            //    }
            //    else
            //    {
            //        closet_length[i][j + 1] -= w1;
            //    }
            //}
            //else if (turn[j] - 90 < 1 && 90 - turn[j] < 1)
            //{
            //    offset[j] = new Vector3(0, 0, 0);

            //    if (j == wall_count[i]-1)
            //    {
            //        closet_length[i][0] += depth;
            //    }
            //    else
            //    {
            //        closet_length[i][j + 1] += depth;
            //    }


            //}
            if (turn[j] - 270 < 1 && turn[j] - 270 > -1)
            {
                offset[j] = new Vector3(w1, 0, 0);
                closet_length[i][j] -= w1;

                if (j == 0)
                {
                    closet_length[i][wall_count[i] - 1] -= w1;
                }
                else
                {
                    closet_length[i][j - 1] -= w1;
                }
            }
            else if (turn[j] - 90 < 1 && 90 - turn[j] < 1)
            {
                offset[j] = new Vector3(0, 0, 0);

                if (j == 0)
                {
                    closet_length[i][wall_count[i] - 1] += depth;
                }
                else
                {
                    closet_length[i][j - 1] += depth;
                }


            }
            //Debug.Log(turn[i]);

        }

        //if (turn[wall_count[i] - 1] - 270 < 1 && turn[wall_count[i] - 1] - 270 > -1)
        //{
        //    offset[wall_count[i] - 1] = new Vector3(w1, 0, 0);
        //    closet_length[i][wall_count[i] - 1] -= w1;

        //    if (wall_count[i] - 1 == 0)
        //    {
        //        closet_length[i][wall_count[wall_count[i] - 1] - 1] -= w1;
        //    }
        //    else
        //    {
        //        closet_length[i][wall_count[i] - 1 - 1] -= w1;
        //    }
        //}
        //else if (turn[wall_count[i] - 1] - 90 < 1 && 90 - turn[wall_count[i] - 1] < 1)
        //{
        //    offset[wall_count[i] - 1] = new Vector3(0, 0, 0);

        //    if (i == 0)
        //    {
        //        closet_length[i][wall_count[i] - 1] += depth;
        //    }
        //    else
        //    {
        //        closet_length[i][wall_count[i] - 1 - 1] += depth;
        //    }


        //}


    }


    public void closet_rotate(int i)
    {
        set_closet_length(i);


        float sum = 0;

        for (int j = 0; j < wall_count[i]; j++)
        {
            sum += turn[j];
            angel[j] = sum;
        }
    }



    public void generate(int i)
    {

        closet_rotate(i);

        for (int j = 0; j < wall_count[i]; j++)
        {
            if (closet_length[i][j] > 450)
            {
                gameObject.GetComponent<basicgenerate>().width = closet_length[i][j];

                gameObject.GetComponent<hole>().offset = offset[j];

                //if (holes[i][j].w != 2600)
                //{
                    gameObject.GetComponent<hole>().x_left =Mathf.Min( Mathf.Max( holes[i][j].x-offset[j].x,0),closet_length[i][j]);
                    gameObject.GetComponent<hole>().x_right = Mathf.Min( holes[i][j].y- offset[j].x,closet_length[i][j]);
                    gameObject.GetComponent<hole>().y_bottom = holes[i][j].z;
                    gameObject.GetComponent<hole>().y_top = holes[i][j].w;
                //}
                //else
                //{
                //    gameObject.GetComponent<hole>().x_left = 0;
                //    gameObject.GetComponent<hole>().x_right = 0;
                //    gameObject.GetComponent<hole>().y_bottom = 0;
                //    gameObject.GetComponent<hole>().y_top = 0;
                //}
                //
                Debug.Log(j);
                Debug.Log(closet_length[i][j]);
                Debug.Log(new Vector4( gameObject.GetComponent<hole>().x_left,gameObject.GetComponent<hole>().x_right,gameObject.GetComponent<hole>().y_bottom,gameObject.GetComponent<hole>().y_top));

                    gameObject.GetComponent<hole>().make_wall();

                    //Vector3 pos = gameObject.GetComponent<basicgenerate>().onetime.transform.position;



                    //if (j != room_outline.Length-1)
                    //{
                    //    gameObject.GetComponent<basicgenerate>().onetime.transform.Translate(room_outline[j+1]);
                    //}else{
                    //    gameObject.GetComponent<basicgenerate>().onetime.transform.Translate(room_outline[0]);

                    //}
                    gameObject.GetComponent<basicgenerate>().onetime.transform.Translate(room_outline[j]);
                    gameObject.GetComponent<basicgenerate>().onetime.transform.Rotate(new Vector3(0, angel[j] + 180, 0));

            }

        }

        corner(i);
    }



    public void corner(int i)
    {
        for (int j = 0; j < wall_count[i]-1; j++)
        {

            if (turn[j] - 270 < 1 && 270 - turn[j] < 1/*&&wall_length[i][j]>=0&&wall_length[i][j+1]>=0*/)
            {

                gameObject.GetComponent<cornercloset>().create(room_outline[j] + offset[j], new Vector3(0, angel[j]+180, 0));

            }

        }

        if (turn[wall_count[i] - 1] - 270 < 1 && 270 - turn[wall_count[i] - 1] < 1 /*&& wall_length[i][wall_count[i] - 1] >=0 && wall_length[i][0] >=0*/)
        {

            gameObject.GetComponent<cornercloset>().create(room_outline[wall_count[i] - 1] + offset[wall_count[i] - 1], new Vector3(0, angel[wall_count[i] - 1]+180, 0));

        }




    }



    void Update()
    {
        
    }
}


