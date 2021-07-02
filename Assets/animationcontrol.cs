using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationcontrol : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isShootingHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isShootingHash = Animator.StringToHash("isShooting");
    }

    // Update is called once per frame
    void Update()
    {
        bool animWalking = animator.GetBool(isWalkingHash);

        bool animShooting = animator.GetBool(isShootingHash);
       
        if (!animShooting && Input.GetMouseButtonDown(0)) 
        {
            animator.SetBool(isShootingHash, true);
        }

        if (animShooting && !Input.GetMouseButtonDown(0)) 
        {
            animator.SetBool(isShootingHash, false);
        }
        
        if (!animWalking && Input.GetKey("w"))
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!animWalking && Input.GetKey("a"))
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!animWalking && Input.GetKey("s"))
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!animWalking && Input.GetKey("d"))
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (animWalking && !Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
        {
            animator.SetBool(isWalkingHash, false);
        }

       
    }
}
