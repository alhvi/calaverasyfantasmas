using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public struct GamepadDevice
{
    public bool active;
    public InputDevice device;

}

//Gamepad manager, needs to be created in the first scene of the game
[RequireComponent(typeof(InControlManager))]
public class GamepadManager : MonoBehaviour {

    
    private static GamepadManager instance;
    [SerializeField]
    private int maxPlayers;

    private GamepadDevice[] inputs;

    public static GamepadManager Instance {
        get {
            return instance;
        }
    }

    public GamepadDevice[] Inputs {
        get {
            return inputs;
        }
    }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        inputs = new GamepadDevice[maxPlayers];
        ResetInputs();
        InputManager.OnDeviceDetached += OnDeviceDetached;
    }

    public void ResetInputs() {
        for (int i = 0; i < inputs.Length; i++) {
            inputs[i].active = false;
            inputs[i].device = null;
        }
    }
	

    public int AddController(InputDevice device) {
        bool found = false;
        int controllerId = -1;
        for (int i = 0; i < inputs.Length; i++) {
            if (inputs[i].device == device) {
                found = true;
                inputs[i].active = true;
                controllerId = i;
                break;
            }
        }

        if (!found) {
            for (int i = 0; i < inputs.Length; i++) {
                if (inputs[i].device == null) {
                    inputs[i].device = device;
                    inputs[i].active = true;
                    controllerId = i;
                    break;
                }
            }
        }

        return controllerId;
    }

    public int GetPlayerId(InputDevice device) {
        int controllerId = -1;
        for (int i = 0; i < inputs.Length; i++) {
            if (inputs[i].device == device) {
                controllerId = i;
                break;
            }
        }
        return controllerId;
    }

    void OnDeviceDetached(InputDevice device) {
        for (int i = 0; i < inputs.Length; i++) {
            if (inputs[i].device == device) {
                inputs[i].device = null;
                inputs[i].active = false;
                //Remove or disable player from scene?
                break;
            }
        }
    }
}
