using UnityEngine;
using System.Collections;

public class MapInfo : MonoBehaviour {

	public float timeToFinish;
	public int mapMoney;
	public Transform startLocation;
	public GameObject endFlag;

	void Awake()
	{
		if (!startLocation)
			startLocation = transform.FindChild ("StartLocation");
		if (!endFlag)
			endFlag = transform.FindChild ("EndFlag").gameObject;
	}
}
