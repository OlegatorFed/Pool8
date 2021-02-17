using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Ball ball;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, ball.transform.position);

        lineRenderer.enabled = Input.GetMouseButton(0);

        if (plane.Raycast(ray, out hit)) 
        {
            lineRenderer.SetPosition(1, ball.transform.position);
            lineRenderer.SetPosition(0, ray.GetPoint(hit) + new Vector3(0, ball.transform.position.y, 0));
        }

        if (Input.GetMouseButtonUp(0))
        {
            ball.GetComponent<Rigidbody>().velocity = (ball.transform.position - ray.GetPoint(hit)) * 3;
        }
    }
}
