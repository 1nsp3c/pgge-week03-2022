using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;

    public float mWalkSpeed = 1.0f;
    public float mRotationSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");


        float speed = mWalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = mWalkSpeed * 2.0f;
        }

        if (mAnimator == null) return;

        transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);

        Vector3 forward = transform.forward;
        forward.y = 0.0f;

        mCharacterController.Move(forward * vInput * speed * Time.deltaTime);


        mAnimator.SetFloat("PosX", 0);
        mAnimator.SetFloat("PosZ", vInput * speed / (2.0f * mWalkSpeed));
    }
}
