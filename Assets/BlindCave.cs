using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlindCave {

	public class Levels {

		public int Level { get; set; }
		public string Name { get; set; }
		public bool CamMoves { get; set; }
		public Vector3 CamPos { get; set; }
		public Vector2 P1Start { get; set; }
		public Vector2 P2Start { get; set; }
		public Vector3Int TopLeftTile { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Tile[,] Grid { get; set; }

		public Levels(int number) {
			// Opening the file
			string path = Application.streamingAssetsPath + "\\Levels\\Level_" + number.ToString() + ".txt";
			WWW dat = new WWW(path);
			while (!dat.isDone) { }
			string info = dat.text;

			// Get the components
			string[] lines = info.Split('\n');

			// Initial data
			Name = lines[0];
			Width = System.Convert.ToInt16(lines[1].Split('x')[0]);
			Height = System.Convert.ToInt16(lines[1].Split('x')[1]);
			CamMoves = (lines[2].Split(' ')[0] == "True");
			if (!CamMoves) {
				CamPos = new Vector3(System.Convert.ToInt16(lines[2].Split(' ')[1]), System.Convert.ToInt16(lines[2].Split(' ')[2]), System.Convert.ToInt16(lines[2].Split(' ')[3]));
			} else {
				CamPos = new Vector3(0, 0, -5);
			}

			// Starting points
			P1Start = new Vector2(System.Convert.ToInt16(lines[3].Split(' ')[0]), System.Convert.ToInt16(lines[3].Split(' ')[1]));
			P2Start = new Vector2(System.Convert.ToInt16(lines[4].Split(' ')[0]), System.Convert.ToInt16(lines[4].Split(' ')[1]));

			// Generation of tiles
			TopLeftTile = new Vector3Int(System.Convert.ToInt16(lines[5].Split(' ')[0]), System.Convert.ToInt16(lines[5].Split(' ')[1]), 0);

			Grid = new Tile[Height, Width];

			for (int i = 6; i < 6 + Height; i++) {
				string[] innerData = lines[i].TrimEnd('\r').Split(' ');
				Debug.Log("Length: " + innerData.Length);
				for (int j = 0; j < innerData.Length; j++) {
					//(x,y)
					Tile newTile = new Tile(innerData[j]);
					Grid[i - 6, j] = newTile;
				}
			}
		}


	}

	public class Tile {

		public int P1Data { get; set; }
		public int P2Data { get; set; }

		public Tile(int p1, int p2) {
			P1Data = p1;
			P2Data = p2;
		}

		public Tile(string data) {
			// (1,2)
			P1Data = System.Convert.ToInt16(data.TrimStart('(').TrimEnd(')').Split(',')[0]);
			P2Data = System.Convert.ToInt16(data.TrimStart('(').TrimEnd(')').Split(',')[1]);

		}

	}

}
