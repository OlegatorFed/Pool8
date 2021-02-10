using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float PanSpeed = 20f;
    public float PanBorder = 15f;
    public Vector2 PanLimit;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //too bad...
        float UpButton = Input.GetKey("up") || Input.mousePosition.y >= Screen.height - PanBorder ? 1 : 0;
        float DownButton = Input.GetKey("down") || Input.mousePosition.y <= PanBorder ? -1 : 0;
        float RightButton = Input.GetKey("right") || Input.mousePosition.x >= Screen.width - PanBorder ? 1 : 0;
        float LeftButton = Input.GetKey("left") || Input.mousePosition.x <= PanBorder ? -1 : 0;

        pos += new Vector3(RightButton + LeftButton, 0, UpButton + DownButton) * PanSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -PanLimit.x, PanLimit.x);
        pos.z = Mathf.Clamp(pos.z, -PanLimit.y, PanLimit.y);

        transform.position = pos;
    }
}
