using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreatorHud : MonoBehaviour {

	public CreatorController creator;
	public Text timerText;
	public Text moneyText;
	public Transform objPreview;

	// Use this for initialization
	void Start () {
		if (GameObject.Find("CreatorEnt"))
			creator = GameObject.Find ("CreatorEnt").GetComponent<CreatorController>();
		eraseAllText ();
	}

	public void updateTimers(string timeFromGame) {
		timerText.text = timeFromGame;
	}

	public void updateMoneyText(int moneyLeft) {
		moneyText.text = "$" + moneyLeft;
	}

	public void updateObjectPreview(int curObj) {
		//Update the image
		SpriteRenderer curSelectedSprite = creator.availableObjs [curObj].GetComponent<SpriteRenderer> ();
		Image curObjPreview = objPreview.Find ("ObjectPreviewImage").GetComponent<Image>();
		curObjPreview.sprite = curSelectedSprite.sprite;

		//Update the name
		Text curObjText = objPreview.Find ("ObjectName").GetComponent<Text>();
		curObjText.text = creator.availableObjs [curObj].name;

		//Update the description
		Text curObjDesc = objPreview.Find ("ObjectDesc").GetComponent<Text>();
		curObjDesc.text = creator.availableObjs [curObj].description;

		//Update the cost
		Text curObjCost = objPreview.Find ("ObjectCost").GetComponent<Text>();
		curObjCost.text = "$" + creator.availableObjs [curObj].cost;
	}

	// PRIVATE FUNCTIONS
	private void eraseAllText()
	{
		timerText.text = "";
		moneyText.text = "";
	}
}
