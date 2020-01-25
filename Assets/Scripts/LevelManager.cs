using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Start is called before the first frame update
	void Start() {
		DontDestroyOnLoad(this.gameObject);
	}

	public void GenerateLevel(int i) {
		Debug.Log(i);
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}
}
