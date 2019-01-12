using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morehole : MonoBehaviour
{
    //public int holeamount;
    public Vector4 holeinfo;



    public float x_left = 1500;
    public float x_right = 3000;
    public float y_bottom = 900;
    public float y_top = 2200;


    private float height;
    private float width;
    private float depth;



    public Vector3 offset = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        holeinfo = new Vector4();

        //for (int i = 0; i < holeamount;i++){
        //    holeinfo[i] = new Vector4();
        //}
        //make_wall();
        //isolate_cabin(x_left, x_right, y_top, height);

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void make_wall()
    {

        height = gameObject.GetComponent<basicgenerate>().height;
        width = gameObject.GetComponent<basicgenerate>().width;
        depth = gameObject.GetComponent<basicgenerate>().depth;


        cutcorner();
        //if (holeamount != 0)
        //{

        //holeinfo = new Vector4(holeinfo.x - offset.x, holeinfo.y - offset.x, holeinfo.z, holeinfo.w);
            if (holeinfo.x > 0 && holeinfo.y < width)
            {

                gameObject.GetComponent<basicgenerate>().onetime = new GameObject();


                //for (int i = 0; i < holeamount; i++)
                //{
                    make_hole();
                //}
            }
            else
            {
                gameObject.GetComponent<basicgenerate>().onetime = new GameObject();

                gameObject.GetComponent<basicgenerate>().move = Vector3.zero + offset;

                gameObject.GetComponent<basicgenerate>().basic_generate();
            }

        //}else{
        //    gameObject.GetComponent<basicgenerate>().onetime = new GameObject();

        //    gameObject.GetComponent<basicgenerate>().move = Vector3.zero + offset;

        //    gameObject.GetComponent<basicgenerate>().basic_generate();
        //}


    }



    public void cutcorner(){
        //for (int i = 0; i < holeamount;i++){
            holeinfo.x -= 1000;
            holeinfo.y -= 1000;
        //}
    }






    public void make_hole()
    {



        if (holeinfo.x != holeinfo.y)
        {

            wallpart(offset.x, holeinfo.x);
            windowpart();

            //}

            //else {
            //    wallpart(holeinfo[i-1].y, holeinfo[i].x);
            //    windowpart();
            //}

            wallpart(holeinfo.y, width + offset.x);

        }
        else
        {
            wallpart(offset.x, width + offset.x);

        }
    }

    public void wallpart(float left ,float right){

        gameObject.GetComponent<basicgenerate>().move = new Vector3(left, 0, 0);//Vector3.zero + offset + new Vector3(holeinfo[i - 1].y, 0, 0);
        gameObject.GetComponent<basicgenerate>().width = right - left;
        gameObject.GetComponent<basicgenerate>().basic_generate();
        gameObject.GetComponent<basicgenerate>().width = width;

    }

    public void windowpart(){



        if (holeinfo.z > 300)
        {
            gameObject.GetComponent<basicgenerate>().move = new Vector3(holeinfo.x+offset.x, 0, 0) + offset;
            gameObject.GetComponent<basicgenerate>().width = holeinfo.y - holeinfo.x;
            gameObject.GetComponent<basicgenerate>().height = holeinfo.z;
            gameObject.GetComponent<basicgenerate>().basic_generate();
            gameObject.GetComponent<basicgenerate>().height = height;
            gameObject.GetComponent<basicgenerate>().width = width;
        }


        if (holeinfo.w <= height - 300)
        {
            gameObject.GetComponent<basicgenerate>().move = new Vector3(holeinfo.x + offset.x, 0, 0) + offset;
            gameObject.GetComponent<basicgenerate>().width = holeinfo.y - holeinfo.x;


            isolate_cabin();
            gameObject.GetComponent<basicgenerate>().cabin.transform.parent = gameObject.GetComponent<basicgenerate>().equal.transform;
            gameObject.GetComponent<basicgenerate>().width = width;


        }



    }







    public void isolate_cabin()
    {

        gameObject.GetComponent<basicgenerate>().move = Vector3.zero + offset;
        gameObject.GetComponent<basicgenerate>().width = holeinfo.y - holeinfo.x;
        gameObject.GetComponent<basicgenerate>().vectical_split(holeinfo.y - holeinfo.x);

        gameObject.GetComponent<basicgenerate>().hanging_cabin(holeinfo.x, holeinfo.y, depth, holeinfo.w,height );
        gameObject.GetComponent<basicgenerate>().width = width;
    }


}