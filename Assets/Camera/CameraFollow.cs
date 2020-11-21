using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 currentPosition;
    public float smoothSpeed;
    public Vector3 offset;
    public float playerOffset;
    private Flip flip;
    private bool playerFlip;

    private void Start()
    {
        flip = target.GetComponent<Flip>();
        playerFlip = false;
    }

    private void FixedUpdate()
    {
        if (flip.IsFacingLeft && offset.x > 0 && !playerFlip)
        {
            currentPosition = target.position;
            playerFlip = true;
        }
        if (flip.IsFacingLeft && offset.x > 0 && currentPosition.x - target.position.x >= playerOffset) FlipCamera();
        
        if (!flip.IsFacingLeft && offset.x < 0  && !playerFlip)
        {
            currentPosition = target.position;
            playerFlip = true;
        }
        if(!flip.IsFacingLeft && offset.x < 0 && target.position.x - currentPosition.x >= playerOffset) FlipCamera();
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        

        //print((int)target.rotation.eulerAngles.y);
    }

    private void FlipCamera()
    {
        //меняем направление движения камеры
        //currentPosition = target.position;
        playerFlip = false;
        offset.x = offset.x * -1;
        //prevPosition = Vector3.zero;
        //print("offset " + isFacingLeft);
    }
}
