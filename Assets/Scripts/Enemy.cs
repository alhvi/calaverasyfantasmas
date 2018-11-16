using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 1.5f;

	
	void Start () {
        SetRandomLocation();
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(transform.position.x) > 25f || Mathf.Abs(transform.position.z) > 25f) {
            transform.forward = new Vector3(transform.forward.x*-1, 0, transform.forward.z*-1);
        }


        transform.Translate(transform.forward * speed * Time.deltaTime);
        
        
	}

    void SetRandomLocation() {
        Quaternion randomLocation = Random.rotation;
        Vector3 newTransform = randomLocation.eulerAngles;
        transform.forward = new Vector3(newTransform.x, 0, newTransform.z);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag =="Sword") {
            
            Player player = other.gameObject.GetComponentInParent<Player>();
            if (player != null) {
                if (player.attacking) {
                    Destroy(gameObject);
                }
            }
        } else if (other.gameObject.tag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null) {
                if (player.attacking && (player.currentAnimation == Animations.ATTACK_JUMP)) {
                    Destroy(gameObject);
                }
                Debug.Log("Bajar vida a player");
            }
        }
    }

    
}
