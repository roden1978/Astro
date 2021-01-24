using System;
using UnityEngine;
//[CreateAssetMenu(fileName = "New Chase", menuName = "Enemies/Behaviour/Chase")]
public class SlimeChase : IHauntingable
{
    private SlimeController slimeController;
    private Rigidbody2D rb;
    public SlimeChase(GameObject gameObject)
    {
        slimeController = gameObject.GetComponent<SlimeController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Haunting()
    {
        //Debug.Log("Chase" + slimeController.GetRigidBody);
        rb.AddForce(Vector2.up * slimeController.SlimeEnemy.Force, ForceMode2D.Force);
    }

}
