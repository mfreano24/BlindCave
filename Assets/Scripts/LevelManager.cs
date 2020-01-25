using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlindCave;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public List<TileBase> tiles;
	
	// Prefabs
	public GameObject playerPrefab;
	public GameObject gridPrefab;

	// Start is called before the first frame update
	void Awake() {
		GenerateLevel(PlayerPrefs.GetInt("Level"));
		PlayerPrefs.DeleteKey("Level");
	}

	public void GenerateLevel(int i) {
		Debug.Log(i);
		Levels thisLevel = new Levels(i);

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera")) {
			if (thisLevel.CamMoves) {
				// TO-DO: Set camera as child of player (or use camera controller to follow player)
			} else {
				g.transform.position = thisLevel.CamPos;
			}
		}

		GameObject p1 = (GameObject)Instantiate(playerPrefab, thisLevel.P1Start, Quaternion.identity);
		GameObject p2 = (GameObject)Instantiate(playerPrefab, thisLevel.P2Start, Quaternion.identity);
		GameObject grid = (GameObject)Instantiate(gridPrefab, Vector3.zero, Quaternion.identity);

		for (int k = 0; k < 4; k++) {
			grid.transform.GetChild(k).GetComponent<Tilemap>().ClearAllTiles();
		}

		// Generating the levels
		for (int x = 0; x < thisLevel.Height; x++) {
			for (int y = 0; y < thisLevel.Width; y++) {
				BlindCave.Tile curr = thisLevel.Grid[x, y];
				//thisLevel.Grid[x, y] --> (P1Data, P2Data) ;
				if (curr.P1Data > 0) {
					grid.transform.GetChild(0).GetComponent<Tilemap>().SetTile(thisLevel.TopLeftTile + new Vector3Int(y, -x, 0), tiles[curr.P1Data]);
				} else if (curr.P1Data < 0) {
					grid.transform.GetChild(1).GetComponent<Tilemap>().SetTile(thisLevel.TopLeftTile + new Vector3Int(y, -x, 0), tiles[-curr.P1Data]);
				}

				if (curr.P2Data > 0) {
					grid.transform.GetChild(2).GetComponent<Tilemap>().SetTile(thisLevel.TopLeftTile + new Vector3Int(y, -x, 0), tiles[curr.P2Data]);
				} else if (curr.P2Data < 0) {
					grid.transform.GetChild(3).GetComponent<Tilemap>().SetTile(thisLevel.TopLeftTile + new Vector3Int(y, -x, 0), tiles[-curr.P2Data]);
				}
			}
		}
	}
}
