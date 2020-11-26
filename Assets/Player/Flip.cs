using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
	private bool isFacingLeft;
	private PlayerController playerController;
	// Start is called before the first frame update
	void Start()
	{
		isFacingLeft = false;
		playerController = GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		Fliping();
	}

	public void FlipPlayer()
	{
		//меняем направление движения персонажа
		isFacingLeft = !isFacingLeft;
		transform.Rotate(0f, 180f, 0f);
	}

	public void Fliping()
	{
		if ((playerController.Direction.x > 0) && isFacingLeft)
			//отражаем персонажа вправо
			FlipPlayer();
		//обратная ситуация. отражаем персонажа влево
		else if ((playerController.Direction.x < 0) && !isFacingLeft)
			FlipPlayer();
	}

	public bool IsFacingLeft => isFacingLeft;
}
