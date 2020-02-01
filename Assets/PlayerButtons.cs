using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtons : MonoBehaviour {

	public bool pressed = false;

	float CalculateHeight() {
		// Do shit here, bitchs
		return 0;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.transform.name == "Player" && collision.transform.position.y > CalculateHeight()) {
			StartCoroutine(Press());
			pressed = true;
		}
	}

	private void OnCollisionExit(Collision collision) {
		if (pressed) {
			pressed = false;
			StopCoroutine(Press());
			StartCoroutine(Release());
		}
	}

	IEnumerator Press() {
		yield return new WaitForEndOfFrame();
	}

	IEnumerator Release() {
		yield return new WaitForEndOfFrame();
	}

}
