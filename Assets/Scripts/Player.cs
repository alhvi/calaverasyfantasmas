using InControl;
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

    public int playerNum;
    private Animator anim;
    public bool attacking = false;
    public bool blocking = false;
    private InputDevice joystick;
   
    public Animations currentAnimation = 0;


    void Start () {
        anim = GetComponent<Animator>();
        if (GamepadManager.Instance.Inputs[playerNum].active != false && GamepadManager.Instance.Inputs[playerNum].device != null) {
            joystick = GamepadManager.Instance.Inputs[playerNum].device;
        }
	}
	

	void Update () {
        if (joystick != null) {
            float horizontalInput = joystick.LeftStickX;
            float verticalInput = joystick.LeftStickY;


            Vector3 oldForward = transform.forward;
            Vector3 newForward = new Vector3(horizontalInput, 0, verticalInput);

            if (!attacking) {
                currentAnimation = Animations.IDLE;
                if (newForward.magnitude > 0) {
                    transform.forward = newForward;
                    currentAnimation = Animations.RUN_FORWARD;
                    blocking = false;
                }

                if (joystick.Action1) {
                    currentAnimation = Animations.ATTACK_SIMPLE;
                    attacking = true;
                }

                if (joystick.Action2) {
                    currentAnimation = Animations.BLOCK;
                    blocking = true;
                }

                if (joystick.Action3) {
                    currentAnimation = Animations.ATTACK_MEDIUM;
                    attacking = true;
                }

                if (joystick.Action4) {
                    currentAnimation = Animations.ATTACK_JUMP;
                    attacking = true;
                }


                anim.SetInteger("animation", currentAnimation.GetHashCode());

            }
        }
	}

    public void SetAttackingFalse() {
        attacking = false;
        blocking = false;
        currentAnimation = Animations.IDLE;
    }
}
