using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour {

    public GameObject[] PathArray;
    public GameObject Player;
    public List<GameObject> paths;
    private float triggerDistance;
    private float pointDistance;

	// Use this for initialization
	void Start () {

        triggerDistance = 0;
        pointDistance = 140+75;

	}
	
	// Update is called once per frame
	void Update () {
		
        if (Player.transform.position.z >= triggerDistance)
        {
            CreateNewPath();
        }
	}

    // Late Update is called after Update
    private void LateUpdate()
    {
        DeleteOldPaths();
    }

    // TODO: Implement. Will initialize the first 30 some paths so that player will 
    // not be able to see path generation happening.
    private void InitializePaths()
    {

    }

    // Generates a new path and places it on the screen
    private void CreateNewPath()
    {
        // instantiates path at Vector (0, 0, pointDistance)
        GameObject path = Instantiate(GetRandomPath(), new Vector3(0, 0, pointDistance), Quaternion.identity);
        // adds the path to the array of paths
        paths.Add(path);
        // resets the trigger distance. Need to change this once path init is implemented
        triggerDistance = path.transform.position.z;
        // resets point distance for next path
        pointDistance += path.transform.localScale.z * 10;

    }

    // finds a random int i in range of number of paths and returns that index Path
    private GameObject GetRandomPath()
    {
        int i = Random.Range(0, PathArray.Length);
        return PathArray[i];

    }

    // Deletes old created paths whose z position is behind the player
    private void DeleteOldPaths()
    {
        foreach (var path in paths)
        {
            // finds difference in z position
            float zDiff = Player.transform.position.z - path.transform.position.z;
            if (zDiff > 300)
            {
                paths.Remove(path);
            }
        }
    }
}
