using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;
    public FixedJoystick mJoystick;
    public bool mFollowCameraForward = false;

    public float mWalkSpeed = 1.0f;
    public float mRotationSpeed = 50.0f;
    public float mTurnRate = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //float hInput = Input.GetAxis("Horizontal");
        //float vInput = Input.GetAxis("Vertical");
        float hInput = 2.0f * mJoystick.Horizontal;
        float vInput = 2.0f * mJoystick.Vertical;


        float speed = mWalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = mWalkSpeed * 2.0f;
        }

        if (mAnimator == null) return;

        if (mFollowCameraForward)
        {
            if (Mathf.Abs(hInput) > 0.1f)
            {
                // rotate Player towards the camera forward.
                Vector3 eu = Camera.main.transform.rotation.eulerAngles;
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.Euler(0.0f, eu.y, 0.0f),
                    mTurnRate * Time.deltaTime);
            }
        }
        else
        {
            if (Mathf.Abs(hInput) > 0.1f)
            {
                transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
            }
        }


        Vector3 forward = transform.forward;
        forward.y = 0.0f;

        mCharacterController.Move(forward * vInput * speed * Time.deltaTime);


        mAnimator.SetFloat("PosX", 0);
        mAnimator.SetFloat("PosZ", vInput * speed / (2.0f * mWalkSpeed));
    }
}
