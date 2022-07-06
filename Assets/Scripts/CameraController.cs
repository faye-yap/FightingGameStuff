using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    public  Transform player1; 
    public  Transform player2;
    public  Transform startLimit;
    public  Transform endLimit; 
    public  Transform leftCameraWall;
    public  Transform rightCameraWall;
    private  float midPoint;
    private  float viewportHalfWidth;
    public float maxSize = 5f;
    public float minSize = 4f;
   
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Vector3 bottomLeft =  Camera.main.ViewportToWorldPoint(new  Vector3(0, 0, 0));
        viewportHalfWidth  =  Mathf.Abs(bottomLeft.x  -  this.transform.position.x);
        midPoint = (player1.transform.position.x + player2.transform.position.x)/2; 
        
     
        
    }

    /*
    bool PlayerOOB(Transform player){
        if (player.transform.position.x < this.transform.position.x - viewportHalfWidth + 1 || player.transform.position.x > this.transform.position.x + viewportHalfWidth - 1)
        return true;
        else return false; 
    }
    */
    // Update is called once per frame
    void Update()
    {
        midPoint = (player1.transform.position.x + player2.transform.position.x)/2;
        
        float newSize = (Mathf.Abs(player1.transform.position.x - player2.transform.position.x)) * 9/32 + 1;
        if (newSize >= minSize && newSize <= maxSize){
            //Debug.Log("changing size");
            leftCameraWall.transform.position = new Vector3(leftCameraWall.transform.position.x - ((newSize - cam.orthographicSize)*16/9), leftCameraWall.transform.position.y, leftCameraWall.transform.position.z);
            rightCameraWall.transform.position = new Vector3(rightCameraWall.transform.position.x + ((newSize - cam.orthographicSize)*16/9), rightCameraWall.transform.position.y, rightCameraWall.transform.position.z);
            cam.orthographicSize = newSize;
            this.transform.position = new Vector3(this.transform.position.x, -1 * (5 - newSize), this.transform.position.z);
            viewportHalfWidth = newSize * 16/9;
            
        } 
        if (midPoint > startLimit.transform.position.x + viewportHalfWidth && midPoint < endLimit.transform.position.x - viewportHalfWidth){
            this.transform.position = new Vector3(midPoint, this.transform.position.y, this.transform.position.z);
        }
    }
}
