using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour {
  [SerializeField] GameObject virtualCam;

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("Hero")) {
      virtualCam.SetActive(true);
    }
  }

  private void OnTriggerExit2D(Collider2D col) {
    if (col.CompareTag("Hero")) {
      virtualCam.SetActive(false);
    }
  }
}
