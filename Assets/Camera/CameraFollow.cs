using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;
    private bool isFacingLeft = false;

    private void FixedUpdate()
    {
        if((int)target.rotation.eulerAngles.y == 180 && !isFacingLeft)
        { 
            FlipCamera();
            
        } 
        if ((int)target.rotation.eulerAngles.y == 0 && isFacingLeft)
        {
            FlipCamera();
        }
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,  desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //print((int)target.rotation.eulerAngles.y);
    }

    private void FlipCamera()
    {
        //меняем направление движения камеры
        isFacingLeft = !isFacingLeft;
        offset.x = offset.x * -1;
        //print("offset " + isFacingLeft);
    }
}
