using UnityEngine;
using System.Collections;

public class WorldMap : MonoBehaviour {

	
	void Start () 
    {
        target = new Vector3(-1,-1,-1);
        coord = new Vector2(-10, -10);
        coords2 = Vector2.zero;
	}
    float boundary = 0.05f;
    float speedMultiplier = 200;
    float maxOrtho =14;
    float minOrtho =3;
    Vector3 target;
    Vector2 coord, coords2;
    public LayerMask layer;
    float camOffset = 14;
	
	// Update is called once per frame
	void Update () {
        Camera cam = Camera.main;
       
        Vector3 camPos = cam.transform.position;
       
         if(Input.GetMouseButtonDown(2)) // one click
        {
              Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200, layer))
            {
                if (hit.collider != null && hit.collider.name == "Plane")
                {
                    coord = Instances.gridManager.GetCoordsFromVector(hit.point);
                   
                    target = new Vector3(hit.point.x-camOffset, camPos.y, hit.point.z-camOffset);
                }
            }
            
           
        }
        else
         if (Input.GetMouseButton(2)) //held down -> move
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200, layer))
            {
                if (hit.collider != null && hit.collider.name == "Plane")
                {
                    coord = Instances.gridManager.GetCoordsFromVector(hit.point);

                    Vector3 world = new Vector3(hit.point.x - camOffset, camPos.y, hit.point.z - camOffset);
                    cam.transform.position = Vector3.Lerp(camPos, world, Time.deltaTime);
                }
            }
           //Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
          
        }
        if (Input.GetMouseButtonUp(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200, layer))
            {
                if (hit.collider != null && hit.collider.name == "Plane")
                {
                    coords2 = Instances.gridManager.GetCoordsFromVector(hit.point);
                    
                }
            }
           
           
            
        }
        //Debug.Log(coord + ", C2: " + coords2);
        if (Mathf.Abs(coords2.x - coord.x)<3 && Mathf.Abs(coords2.y - coord.y)<3)
        {
            
            cam.transform.position = Vector3.Lerp(camPos, target, Time.deltaTime);
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0 ) // forward
         {
             Camera.main.orthographicSize--;
         }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 ) // back
         {
             Camera.main.orthographicSize++;
         }
        if (Input.GetKey(KeyCode.Z))
        {
            Camera.main.orthographicSize -= 0.5f;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            Camera.main.orthographicSize += 0.5f;
        }
         Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minOrtho, maxOrtho );

         float ortho = Camera.main.orthographicSize;
         float speed = Time.deltaTime * ortho;
         Vector3 camP = Camera.main.transform.position;

         Vector3 offSet = Vector3.zero;
         
         if (Input.GetKey(KeyCode.W))
         {
             offSet += new Vector3(1.414f, 0, 1.414f);

            
         }
         else
             if (Input.GetKey(KeyCode.S))
             {
                 offSet += new Vector3(-1.414f, 0, -1.414f);

             }
         
             
         if (Input.GetKey(KeyCode.A))
         {
             offSet +=  new Vector3(-1.414f, 0, 1.414f);

         }
         else
         if (Input.GetKey(KeyCode.D))
         {
             offSet += new Vector3(1.414f, 0, -1.414f);

         }
         
         Camera.main.transform.position = Vector3.MoveTowards(camP, camP + offSet, speed);
	
	}
}
