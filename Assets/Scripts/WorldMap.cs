using UnityEngine;
using System.Collections;

public class WorldMap : MonoBehaviour {

	
	void Start () {
	
	}
    float boundary = 0.05f;
    float speedMultiplier = 200;
    float maxOrtho =14;
    float minOrtho =3;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(2))
        {
            Camera cam = Camera.main;
            Vector2 mousePos = Input.mousePosition;
            Vector3 pos = cam.ScreenToViewportPoint(mousePos);
            Vector3 camPos = cam.transform.position;
            Vector3 translate = Vector3.zero;
            if (pos.x > 1)
            {
                pos.x = 1;
            }
            if (pos.x < 0)
            {
                pos.x = 0;
            }
            if (pos.y > 1)
            {
                pos.y = 1;
            }
            if (pos.y < 0)
            {
                pos.y = 0;
            }
            if (pos.x <= boundary)
            {
                float speed = (boundary - pos.x) * speedMultiplier;
                translate = new Vector3(-1, 0, 0) * speed;
            }
            if (pos.x >= 1 - boundary)
            {
                float speed = (boundary - (1 - pos.x)) * speedMultiplier;
                translate += new Vector3(1, 0, 0) * speed;
            }
            if (pos.y <= boundary)
            {
                float speed = (boundary - pos.y) * speedMultiplier;
                translate += new Vector3(0, -1, 0) * speed;
            }
            if (pos.y >= 1 - boundary)
            {
                float speed = (boundary - (1 - pos.y)) * speedMultiplier;
                translate += new Vector3(0, 1, 0) * speed;
            }
            cam.transform.position = Vector3.Lerp(camPos, camPos + translate, Time.deltaTime);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
         {
             Camera.main.orthographicSize--;
         }
         if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
         {
             Camera.main.orthographicSize++;
         }
         Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minOrtho, maxOrtho );
	
	}
}
