using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwipeToChange : MonoBehaviour 
{
	public RectTransform panel;
	public Button[] img;
	public RectTransform center;

	//Private Variables
	private float[] distance; // All button's distance to the center
	private bool dragging = false; //Only snap when we don't drag
	private int imgDistance; //Will hold the distance between the buttons
	private int minImgNum; //To hold the number of images

	void Start()
	{
		int imgLength = img.Length;
		distance = new float[imgLength]; //set distance array to be the same as img array

		//Get distance between buttons
		imgDistance = (int)Mathf.Abs(img[1].GetComponent<RectTransform>().anchoredPosition.x-img[0].GetComponent<RectTransform>().anchoredPosition.x);
	}
	void Update()
	{
		for (int i = 0; i < img.Length; i++)
		{
			distance [i] = Mathf.Abs (center.transform.position.x - img[i].transform.position.x);
		}
		float minDistance = Mathf.Min (distance);

		for (int a = 0; a < img.Length; a++)
		{
			if (minDistance == distance[a])
			{
				minImgNum = a;
			}
		}
		if (!dragging)
		{
			LerpToImg (minImgNum * -imgDistance);
		}
	}
	void LerpToImg(int position)
	{
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 10f);
		Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);

		panel.anchoredPosition = newPosition;
	}
	public void StartDrag()
	{
		dragging = true;
	}
	public void EndDrag()
	{
		dragging = false;
	}
}
