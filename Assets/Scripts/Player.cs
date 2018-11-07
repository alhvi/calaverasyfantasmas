using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Animations
{
    IDLE,
    RUN_FORWARD,
    RUN_BACKWARDS,
    TURN_RIGHT,
    TURN_LEFT,
    ATTACK_MEDIUM,
    ATTACK_JUMP,
    BLOCK,
    ATTACK_SIMPLE
}

public class Player : MonoBehaviour {

    private Animator anim;
    public bool attacking = false;
    public bool blocking = false;
   
    public Animations currentAnimation = 0;


    void Start () {
        anim = GetComponent<Animator>();		
	}
	

	void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        

        Vector3 oldForward = transform.forward;
        Vector3 newForward = new Vector3(horizontalInput, 0, verticalInput);

        if (!attacking) {
            currentAnimation = Animations.IDLE;
            if (newForward.magnitude > 0) {
                transform.forward = newForward;
                currentAnimation = Animations.RUN_FORWARD;
                blocking = false;
            }

            if (Input.GetButtonDown("Fire1")) {
                currentAnimation = Animations.ATTACK_SIMPLE;
                Debug.Log("Fire1 Pressed");
                attacking = true;
            }

            if (Input.GetButtonDown("Fire2")) {
                currentAnimation = Animations.BLOCK;
                blocking = true;
                Debug.Log("Fire2 Pressed");
                
            }

            if (Input.GetButtonDown("Fire3")) {
                currentAnimation = Animations.ATTACK_MEDIUM;
                attacking = true;
                Debug.Log("Fire3 Pressed");
            }

            if (Input.GetButtonDown("Jump")) {
                currentAnimation = Animations.ATTACK_JUMP;
                Debug.Log("Jump Pressed");
                attacking = true;
            }

            
            anim.SetInteger("animation", currentAnimation.GetHashCode());
          
        }
	}

    public void SetAttackingFalse() {
        attacking = false;
        blocking = false;
        currentAnimation = Animations.IDLE;
    }
}
