using UnityEngine;

public class FlipController : MonoBehaviour
{
    private bool isFacingLeft;
    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        isFacingLeft = false;
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Fliping();
    }

    public void Flip()
    {
        //меняем направление движения персонажа
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Fliping()
    {
        if ((pc.Direction.x > 0) && isFacingLeft)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if ((pc.Direction.x < 0) && !isFacingLeft)
            Flip();
    }
}
