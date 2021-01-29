using System;
using UnityEngine;
//[CreateAssetMenu(fileName = "New Chase", menuName = "Enemies/Behaviour/Chase")]
public class SlimeChase : IHauntingable
{
    private SlimeController slimeController;
    private GameObject player;
    private Transform trans;
    public SlimeChase(GameObject gameObject)
    {
        slimeController = gameObject.GetComponent<SlimeController>();
        trans = gameObject.transform;
    }
    public void Haunting()
    {
        Debug.Log("Chase update" + trans.position);
        //rb.AddForce(Vector2.right * , ForceMode2D.Force);
        //trans.position = new Vector2(trans.position.x + 1 * Time.fixedDeltaTime, trans.position.y);
        trans.position = Vector3.MoveTowards(trans.position, slimeController.Player.transform.position, slimeController.SlimeEnemy.Force * Time.fixedDeltaTime);

    }

}
