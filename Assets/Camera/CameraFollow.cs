using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 prevPosition;
    public float smoothSpeed;
    public Vector3 offset;
    private bool isFacingLeft = false;

    private void FixedUpdate()
    {
       /* if((int)target.rotation.eulerAngles.y == 180 && !isFacingLeft)
        {
            prevPosition = target.position;
            isFacingLeft = !isFacingLeft;

            print("prevPosition" + prevPosition);
        } 
        if ((int)target.rotation.eulerAngles.y == 0 && isFacingLeft)
        {
            //FlipCamera();
            prevPosition = target.position;
            isFacingLeft = !isFacingLeft;
            print("prevPosition" + prevPosition);
        }

        if (prevPosition.x < 0 && (int)target.rotation.eulerAngles.y == 180)
        {
            if(target.position.x < prevPosition.x - 2)
            {
            FlipCamera();
            }
        } else if(prevPosition.x < 0 && (int)target.rotation.eulerAngles.y == 0)
        {
            if (target.position.x > prevPosition.x + 2)
            {
                FlipCamera();
            }
        }

        if (prevPosition.x > 0 && (int)target.rotation.eulerAngles.y == 180)
        {
            if (target.position.x < prevPosition.x - 2)
            {
                FlipCamera();
            }
        }
        else if (prevPosition.x > 0 && (int)target.rotation.eulerAngles.y == 0)
        {
            if (target.position.x > prevPosition.x + 2)
            {
                FlipCamera();
            }
        }*/
        //
        
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        

        //print((int)target.rotation.eulerAngles.y);
    }

    private void FlipCamera()
    {
        //меняем направление движения камеры
        //isFacingLeft = !isFacingLeft;
        offset.x = offset.x * -1;
        prevPosition = Vector3.zero;
        //print("offset " + isFacingLeft);
    }
}
