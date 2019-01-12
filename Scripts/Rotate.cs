
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Rotate : MonoBehaviour,IDragHandler,IPointerClickHandler
{

    public GameObject target;
    private Vector3 offset;
    private Vector2 previous;
    //private bool isClickCube;
    private Vector3 targetScreenPoint;

    private GameObject todestroy;
    private float speed = 2f;


    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("GameObject");
        offset = new Vector3(0,2000,0);
        //Mathf.Pow(offset.x,2)+Mathf.Pow(offset.y,2)+Mathf.Pow

        //offset.x ^ 2 + offset.y ^ 2 + offset.z ^ 2 = 400;
    }

    // Update is called once per frame

    void move()
    {
        todestroy = new GameObject();
        foreach (GameObject closet in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (closet.name == "New Game Object" || closet.name == "corner_closet")
            {
                closet.transform.parent = todestroy.transform;
            }
        }
        //Destroy(todestroy);

    }





    void Update()
    {

        gameObject.transform.position =Vector3.Lerp(gameObject.transform.position,   target.transform.position + offset,speed*Time.deltaTime);
        //Debug.Log(gameObject.transform.position);

        //gameObject.transform.Rotate(0,1,0);
        gameObject.transform.LookAt(target.transform);
        //Debug .Log( target.transform.localRotation);

        if (Input.GetMouseButtonDown(0))
        {



            if (Input.mousePosition.x - previous.x > 0)
            {
                if (offset.x > -1970f)
                {
                    offset.x -= 30f;
                    offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);
                }
                else
                {
                    offset.x = 1960f;
                    offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);

                }
            }
            if (Input.mousePosition.x - previous.x < 0)
            {
                if (offset.x < 1970f)
                {
                    offset.x += 30f;
                    offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);
                }
                else
                {
                    offset.x = -1960f;
                    offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);

                }
            }
            if (Input.mousePosition.y - previous.y > 0)
            {
                if (offset.y > 0)
                {


                    offset.y -= 30f;
                    offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);
                }
            }
            if (Input.mousePosition.y - previous.y < 0)
            {
                if (offset.y < 1960f)
                {
                    offset.y += 30f;
                    offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);
                }
            }
            previous = Input.mousePosition;
        }
        //    if (CheckGameObject())
        //    {
        //        //记下鼠标和方块位置之间的距离，如果没有这个，就会点到方块边上的时候立刻跑到中心的位置
        //        offset = target.GetComponent<Rigidbody>().position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z));
        //    }

        //}


        //if (isClickCube)
        //{
        //    //记下鼠标在屏幕上的位置
        //    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z);
        //    //转换成在三维空间的位置
        //    Vector3 curWorldPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);
        //    //Debug.Log(curWorldPoint);
        //    gameObject.transform.Translate(-curWorldPoint - offset);

        //}
    if(Input.GetMouseButton(0)){
        CheckGameObject();
    }

        Debug.Log(offset);


}

    public void OnPointerClick(PointerEventData pointerEventData)
    {

        CheckGameObject();
        gameObject.transform.position =Vector3 .Lerp(gameObject.transform.position,      target.transform.position + offset,speed*Time.deltaTime);

        gameObject.transform.LookAt(target.transform);

    }





    public void OnDrag(PointerEventData data)
    {
        if (Input.mousePosition.x - previous.x > 0)
        {
            if (offset.x > -1970f)
            {
                offset.x -= 30f;
                offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);
            }
            else
            {
                offset.x = 1960f;
                offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);

            }
        }
        if (Input.mousePosition.x - previous.x < 0)
        {
            if (offset.x < 1970f)
            {
                offset.x += 30f;
                offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);
            }
            else
            {
                offset.x = -1960f;
                offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);

            }
        }
        if (Input.mousePosition.y - previous.y > 0)
        {
            if (offset.y > 0)
            {


                offset.y -= 30f;
                offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);
            }
        }
        if (Input.mousePosition.y - previous.y < 0)
        {
            if (offset.y < 1960f)
            {
                offset.y += 30f;
                offset.z = Mathf.Pow((4000000 - Mathf.Pow(offset.x, 2) - Mathf.Pow(offset.y, 2)), 0.5f);
            }
        }
        previous = Input.mousePosition;





        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,    target.transform.position + offset,speed*Time.deltaTime);

        gameObject.transform.LookAt(target.transform);
    }

    bool CheckGameObject()
    {
        //网上找的，好像是说从摄像机到鼠标发一下射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //可以收射线碰到的信息
        RaycastHit hitInfo;
        //如果碰到collider就会回true
        if (Physics.Raycast(ray, out hitInfo, 100000f))
        {
            Debug.Log(hitInfo);
            //isClickCube = true;
            if (target != null)
            {
                //记下选择的方块在屏幕的位置
                targetScreenPoint = Camera.main.WorldToScreenPoint(hitInfo.rigidbody.position);
            }
            target.transform.position =Vector3.Lerp(target.transform.position,hitInfo.collider.gameObject.transform.position,speed*Time.deltaTime);
            //target = hitInfo.collider.gameObject;
            //hitInfo.rigidbody.GetComponent<Renderer>().material = outline;
            //square.GetComponent<forSquarePrefab>().isSelected = true;

            return true;
        }
        //射线没有碰到collider就是没有选中
        //square.GetComponent<forSquarePrefab>().isSelected = false;

        return false;


    }











}
