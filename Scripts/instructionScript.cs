using UnityEngine;
using System.Collections;

public class instructionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("B_1") || Input.GetButtonDown("B_2") || Input.GetButtonDown("B_3") || Input.GetButtonDown("B_4"))
			Application.LoadLevel(0);
	}
}
