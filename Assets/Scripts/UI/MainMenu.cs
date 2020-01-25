using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour {

	// The panel holding the level buttons
	public GameObject levelPanel;

	public GameObject buttonPrefab;

	// Other buttons on the panel
	public GameObject leftButton, rightButton;

	// In charge of levels
	public int currentPage = 0;
	public int totalPages = 0;
	public int lastUnlockedLevel = 0;

	// Setting up initial state
	public void Start() {
		// Create the document in charge of this if it doesn't exist
		if (!File.Exists(Path.Combine(Application.persistentDataPath, "player.dat"))) {
			using (var writer = new StreamWriter(Path.Combine(Application.persistentDataPath, "player.dat"))) {
				writer.WriteLine("0");
			}
		}

		// Read the latest levels
		using (var reader = new StreamReader(Path.Combine(Application.persistentDataPath, "player.dat"))) {
			lastUnlockedLevel = System.Convert.ToInt16(reader.ReadLine());
		}

		levelPanel.SetActive(false);
		leftButton.GetComponent<Button>().interactable = false;
		if (totalPages == 0) {
			rightButton.GetComponent<Button>().interactable = false;
		}

		// Initializing the level buttons (15 per page)
		// TODO: Manipulate this code to be fit for multiple pages
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 5; j++) {
				GameObject newLevel = (GameObject)Instantiate(buttonPrefab, levelPanel.transform);
				newLevel.transform.localPosition = new Vector2(-250 + 125 * j, 120 - 120 * i);
				newLevel.transform.GetComponentInChildren<Text>().text = (i * 5 + j + 1).ToString();
				newLevel.GetComponent<Button>().onClick.AddListener(() => LoadLevel(System.Convert.ToInt32(newLevel.transform.GetComponentInChildren<Text>().text)));

				if (i * 5 + j + 1 > lastUnlockedLevel + 1) {
					newLevel.GetComponent<Button>().interactable = false;
				}
			}
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			levelPanel.SetActive(false);
		}
	}

	#region Back and Next Buttons
	public void PreviousPage() {
		currentPage--;
		if (currentPage == 0) {
			leftButton.GetComponent<Button>().interactable = false;
		}

		if (totalPages != 0) {
			rightButton.GetComponent<Button>().interactable = true;
		}
	}

	public void NextPage() {
		currentPage++;

		if (currentPage == totalPages) {
			rightButton.GetComponent<Button>().interactable = false;
		}

		leftButton.GetComponent<Button>().interactable = true;
	}
	#endregion

	public void ShowLevels() {
		levelPanel.SetActive(true);
	}

	public void LoadLevel(int i) {
		PlayerPrefs.SetInt("Level", i);
		// TODO: Probably send the user to a connections page, so they can find a room
		UnityEngine.SceneManagement.SceneManager.LoadScene("Proof_of_Concept");
	}

	public void Quit() {
		Application.Quit();
	}

	public void HideLevels() {
		levelPanel.SetActive(false);
	}

}
