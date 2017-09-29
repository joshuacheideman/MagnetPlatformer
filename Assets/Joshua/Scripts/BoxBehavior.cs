using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour {
	private GameObject Player;
	float boxmovement;
	void BoxControl(Transform Box,bool isPull)
	{
		float strength,offset;
		Player = GameObject.FindGameObjectWithTag ("Player");
		offset = Player.gameObject.transform.position.x - Box.position.x;
		if (offset < 1.0f&&offset>-1.0f)//makes it so speed goes from 0 to 1
			return;
		strength = 1 / offset;
		if (offset < 0)
			strength = -strength;
		boxmovement = Mathf.Lerp (0, 1, strength/10);
		if (isPull)
			TranslateBoxes (Box, offset);
		else
			TranslateBoxes (Box, -offset);
	}
	public void BoxPull(Transform Box)
	{
		BoxControl (Box, true);
	}
	public void BoxPush(Transform Box)
	{
		BoxControl (Box,false);
	}
	void TranslateBoxes(Transform Box,float offset)
	{
		if (offset < 0)
			Box.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-boxmovement * 100, 0.0f);
		else if (offset > 0)
			Box.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (boxmovement * 100, 0.0f);
		}
}
