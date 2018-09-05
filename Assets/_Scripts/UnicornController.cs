using UnityEngine;

public class UnicornController : MonoBehaviour {

    public Transform player;
    public Collider uniCollider;
    public float jumpVal;
    public float dashVal;
    public float forwardSpeed;
    public float angleSensitivity;
    public RaycastHit hit;
    public Animator uniAnim;

    private Camera mainCamera;
    private bool hasJumpedOnce = false;
    private bool hasJumpedTwice = false;
    private bool hasDashed = true;
    private bool canJumpAgain = true;

    // Use this for initialization
    void Start ()
    {

        mainCamera = Camera.main;

    }
	
	// Update is called once per frame
	void Update ()
    {

        // moves playerbox in direction camera is facing
        this.transform.Translate(mainCamera.transform.forward * Time.deltaTime * forwardSpeed, Space.World);
    }

    void LateUpdate()
    {

        // unicorn avatar follows main camera movements when turning head

        //TODO: Something going wrong, need to check with headset config
        float yVal = mainCamera.transform.rotation.y;
        player.transform.Rotate(new Vector3(0, yVal, 0));
        //player.rotation.Set(0, yVal, 0, 0);
        //player.eulerAngles = new Vector3(0, yVal, 0);

        animControl(JumpCheck(), DashCheck());
    }

    // monitors jump controls
    /* if player is touching the ground, he can jump
        he can jump in mid air one time, if head rotation has come almost back to center
        after that he must be touching the ground again to jump 
        returns true if jump occurred. else false.*/
    private bool JumpCheck()
    {

        float camRot = mainCamera.transform.rotation.x;
        bool jumped = false;

        if (camRot < -1*angleSensitivity)
        {
            if (canJumpAgain)
            {
                this.transform.position += new Vector3(0f, jumpVal, 0f);

                jumped = true;
                canJumpAgain = false;

                if (!hasJumpedOnce)
                {
                    hasJumpedOnce = true;
                }
                else if (hasJumpedOnce && !hasJumpedTwice)
                {
                    hasJumpedTwice = true;
                }
                else
                {
                    Debug.Log("Error, booleans are wrong.");
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, -Vector3.up, 0.5f))
                {
                    hasJumpedTwice = false;
                    this.transform.position += new Vector3(0f, jumpVal, 0f);

                    jumped = true;
                    canJumpAgain = false;
                    hasJumpedOnce = true;
                }
            }
        }

        if (camRot >= -0.15 && !hasJumpedTwice && !canJumpAgain)
        {
            canJumpAgain = true;
        }

        return jumped;
    }

    // monitors dash controls
    /* if player looks down he can dash
        he can dash infinitely but not in a row. he must come back to a center head position
        before he can dash again.
        returns true if dash occurred. else false.*/
    private bool DashCheck()
    {
        bool dashed = false;
        float camRot = mainCamera.transform.rotation.x;

        if (camRot > angleSensitivity && !hasDashed)
        {
            this.transform.position += new Vector3(0f, 0f, dashVal);
            hasDashed = true;
            dashed = true;
        }

        if (camRot <= 0.15)
        {
            hasDashed = false;
        }

        return dashed;
    }

    // applies animation based on whether a jump or dash occured.
    private void animControl(bool jumpCheck, bool dashCheck)
    {
        if (jumpCheck)
        {
            uniAnim.SetTrigger("Jump");
        }
        if (dashCheck)
        {
            uniAnim.SetTrigger("Dash");
        }
    }

}
