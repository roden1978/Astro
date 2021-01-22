using UnityEngine;
//[CreateAssetMenu(fileName = "New Chase", menuName = "Enemies/Behaviour/Chase")]
public class SlimeChase : IHauntingable
{
	public void Haunting()
    {
        Debug.Log("Chase");
    }
}
