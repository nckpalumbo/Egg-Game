using UnityEngine;
using System.Collections;

public class startScript : MonoBehaviour {

    public GameObject arrow;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // we don't have an instruction screen or exit yet, so comment out movement for now
        
        if (Input.GetAxisRaw("L_YAxis_1") == 1 || Input.GetAxisRaw("L_YAxis_2") == 1 || Input.GetAxisRaw("L_YAxis_3") == 1 || Input.GetAxisRaw("L_YAxis_4") == 1)
        {
            if (arrow.transform.position.y < 2)
            {
                arrow.transform.Translate(0, 2.4f, 0);
                
            }
        }

        else if (Input.GetAxisRaw("L_YAxis_1") == -1 || Input.GetAxisRaw("L_YAxis_2") == -1 || Input.GetAxisRaw("L_YAxis_3") == -1 || Input.GetAxisRaw("L_YAxis_4") == -1)
        {
            if(arrow.transform.position.y == 2)
            {
                arrow.transform.Translate(0, -2.4f, 0);
            }
            else if(arrow.transform.position.y == -0.4000001)
            {
                arrow.transform.Translate(0, -2.4f, 0);
            }
        }
        
        if (Input.GetButtonDown("A_1") || Input.GetButtonDown("A_2") || Input.GetButtonDown("A_3") || Input.GetButtonDown("A_4"))
        {
            if(arrow.transform.position.y == 2)
            {
                Application.LoadLevel(1);
            }
            else if(arrow.transform.position.y != 2)
            {
                // load instructions scene -> application.loadlevel(scenenum
                Application.LoadLevel(3);

            }
           // else if(arrow.transform.position.y == -2.8)
            //{
               //Application.Quit(); -> doesn't quit editor, just realtime application so use for later
            //}
        }

    }
}
