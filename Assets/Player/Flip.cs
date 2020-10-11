using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
	private bool isFacingLeft;
	private PController pc;
	// Start is called before the first frame update
	void Start()
	{
		isFacingLeft = false;
		pc = GetComponent<PController>();
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
		if ((pc.Direction.x > 0) && isFacingLeft)
			//отражаем персонажа вправо
			FlipPlayer();
		//обратная ситуация. отражаем персонажа влево
		else if ((pc.Direction.x < 0) && !isFacingLeft)
			FlipPlayer();
	}
}
