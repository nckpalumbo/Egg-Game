using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneScript : MonoBehaviour
{
    public List<GameObject> children;
    public List<GameObject> childrenPrefab;
    private List<Vector3> pos;
    private int isDead = 0;
    private int players = 3; // will change to a variable that keeps track of how many controllers put in input on start screen and selected an egg -> brings into this script for reference 
                             // of how many child game objects to keep track of
	// Use this for initialization
	void Start ()
    {
        // dynamically add children based on how many players selected an egg on start screen to game
        // keep track how many are alive and how many die
        // end game when amount of players - 1 have died

        pos = new List<Vector3>();
        Vector3 pos1 = new Vector3(5, 0, 0);
        Vector3 pos2 = new Vector3(1, 0, 7);
        Vector3 pos3 = new Vector3(2, 0, -1);
        pos.Add(pos1);
        pos.Add(pos2);
        pos.Add(pos3);

        //Instantiate(childrenPrefab[0], pos[0], Quaternion.identity);

        for (int i = 0; i < players; i++)
        {
            GameObject egg = (GameObject)Instantiate(childrenPrefab[i], pos[i], Quaternion.identity);
            children.Add(egg);
            egg.transform.parent = gameObject.transform;
        }

        // method to place eggs on a location in the nest based on how many eggs there are has to be made

        // start menu holds an array of eggs with their customization, this script holds the same array
        // when a player selects that egg as their egg, that player's egg prefab will be that egg -> it will be instantiated as a child of "Players"
        // method to be made to place eggs based on how many players there are in the game. 
    }

    // Update is called once per frame
    void Update ()
    {
        if (isDead >= players - 1)
        {
            Application.LoadLevel(0);
        }

        for (int i = 0; i < children.Count; i++)
        {
            if(children[i].transform.position.y <= -50)
            {
                isDead++;
                Destroy(children[i]);
                children.RemoveAt(i);
            }
        }
    }
}
