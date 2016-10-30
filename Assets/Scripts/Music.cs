using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public AudioClip music;
	private AudioSource source;

	public void Awake () {
		source = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		source.PlayOneShot (music, 0.25f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}
