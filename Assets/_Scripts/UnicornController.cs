using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornController : MonoBehaviour {

    public Transform player;
    public float forwardSpeed;

    private Camera mainCamera;
    private Vector3 forwardVector;

    // Use this for initialization
    void Start ()
    {

        mainCamera = Camera.main;
        forwardVector = new Vector3(0f, 0f, forwardSpeed);

    }
	
	// Update is called once per frame
	void Update ()
    {

        this.transform.position += forwardVector;

        // camera moves in X/-X direction depending on head turn
        Vector3 hmovement = GetMoveSpeed(mainCamera.transform.rotation.y);
        this.transform.position += hmovement/3;

    }

    void LateUpdate()
    {

        // unicorn avatar follows main camera movements when turning head
        float yVal = mainCamera.transform.eulerAngles.y;
        player.eulerAngles = new Vector3(0, yVal, 0);

    }

    // calculates how far turning head moves you in that direction
    private Vector3 GetMoveSpeed(float y)
    {
        float xMove = Mathf.Min(Mathf.Abs(y * 10), 3);

        if (y < 0)
        {
            xMove *= -1;
        }

        return new Vector3(xMove, 0f, 0f);
    }
}
