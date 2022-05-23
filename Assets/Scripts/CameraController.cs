using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour {

    // Variables.
    [SerializeField] private Transform player;

    // Lifecycle.
    void Update() {
        transform.position = new Vector3(player.position.x, player.position.y + 4f, transform.position.z);
    }

}