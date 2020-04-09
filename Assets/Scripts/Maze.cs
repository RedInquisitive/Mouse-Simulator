using System.Collections;
using UnityEngine;

public class Maze : MonoBehaviour {
	public MazeCell mazeCell;

	public Vector2Int size;
	public float delayTime;

	private MazeCell[,] cells;

	public bool ContainsCoordinates(Vector2Int coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
	}

	public IEnumerator Generate() {
		WaitForSeconds delay = new WaitForSeconds(delayTime);
		cells = new MazeCell[size.x, size.y];
		for (int x = 0; x < size.x; x++) {
			for (int y = 0; y < size.y; y++) {
				yield return delay;
				CreateCell(new Vector2Int(x, y));
			}
		}
	}

	private void CreateCell(Vector2Int position) {
		MazeCell newCell = Instantiate(mazeCell) as MazeCell;
		cells[position.x, position.y] = newCell;
		newCell.name = "Maze Cell " + position.x + ", " + position.y;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = 
			new Vector3(position.x - size.x * 0.5f + 0.5f, 0f, position.y - size.y * 0.5f + 0.5f);
	}
}
