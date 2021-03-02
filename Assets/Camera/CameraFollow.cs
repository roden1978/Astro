using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Объект за которым следит камера")]
    private Transform target;
    [SerializeField]
    [Tooltip("Скорость перемещения камеры")]
    private float smoothSpeed;
    [SerializeField]
    [Tooltip("Смещение камеры")]
    private Vector3 offset;
    [SerializeField]
    [Tooltip("Дистанция на которую должен сметиться объект для переворота камеры")]
    private float playerOffset;
    
    private Vector3 currentPosition;
    private Player player;
    private bool playerFlip;

    private void Start()
    {
        player = target.GetComponent<Player>();
        playerFlip = false;
        currentPosition = transform.position;
    }

    private void FixedUpdate()
    {
        /*if (flip.IsFacingLeft && offset.x > 0 && !playerFlip)
        {
            currentPosition = target.position;
            playerFlip = true;
        }
        if (!flip.IsFacingLeft && offset.x < 0  && !playerFlip)
        {
            currentPosition = target.position;
            playerFlip = true;
        }
        float distance = Vector3.Distance(currentPosition, target.position);
        
        if (flip.IsFacingLeft && offset.x > 0 && distance >= playerOffset) FlipCamera();
        if (!flip.IsFacingLeft && offset.x < 0 && distance >= playerOffset) FlipCamera();*/
        
        if (!player.MovingRight && offset.x > 0 && !playerFlip)
        {
            currentPosition = target.position;
            playerFlip = true;
        }
        if (player.MovingRight && offset.x < 0  && !playerFlip)
        {
            currentPosition = target.position;
            playerFlip = true;
        }
        float distance = Vector3.Distance(currentPosition, target.position);
        
        if (!player.MovingRight && offset.x > 0 && distance >= playerOffset) FlipCamera();
        if (player.MovingRight && offset.x < 0 && distance >= playerOffset) FlipCamera();
        
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void FlipCamera()
    {
        //меняем направление движения камеры
        playerFlip = false;
        offset.x = offset.x * -1;
    }
}
