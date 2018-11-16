using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class SelectCharacterController : MonoBehaviour {

    public List<GameObject> players;
    public List<GameObject> playerSpots;


    void Update()
    {
        //A, X, Y
        if (InputManager.ActiveDevice.Action1 || InputManager.ActiveDevice.Action3 || InputManager.ActiveDevice.Action4) {
            int playerId = GamepadManager.Instance.AddController(InputManager.ActiveDevice);
            if (playerId < players.Count) {
                players[playerId].gameObject.SetActive(true);
            }
            if (playerId < playerSpots.Count) {
                playerSpots[playerId].gameObject.SetActive(false);
            }
        }

        //B
        if (InputManager.ActiveDevice.Action2) {
            int playerId = GamepadManager.Instance.GetPlayerId(InputManager.ActiveDevice);
            if (playerId != -1) {
                GamepadManager.Instance.Inputs[playerId].active = false;
                if (playerId < players.Count) {
                    players[playerId].gameObject.SetActive(false);
                }
                if (playerId < playerSpots.Count) {
                    playerSpots[playerId].gameObject.SetActive(true);
                }

            }
        }

        if (InputManager.ActiveDevice.MenuWasPressed) {
            SceneManager.LoadScene("Game");
        }
    }
}
