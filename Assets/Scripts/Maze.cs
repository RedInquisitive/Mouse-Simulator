using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {

	public class Maze : MonoBehaviour {
		public MazeCell mazeCell;
		public MazeWall mazeWall;
		public MazePassage mazePassage;

		public Vector2Int size;
		public float delayTime;

		private MazeCell[,] cells;

		public Vector2Int RandomCoordinates() {
			return new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
		}

		public bool ContainsCoordinates(Vector2Int coordinate) {
			return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
		}

		public MazeCell GetCell(Vector2Int position) {
			return cells[position.x, position.y];
		}

		public IEnumerator Generate() {
			cells = new MazeCell[size.x, size.y];
			var delay = new WaitForSeconds(delayTime);
			var active = new List<MazeCell> {CreateCell(RandomCoordinates())};

			while (active.Count > 0) {
				yield return delay;
				GenerateStep(active);
			}
		}

		private void GenerateStep(IList<MazeCell> active) {
			var index = active.Count - 1;
			var cell = active[index];
			if (cell.IsFullyInitialized) {
				active.RemoveAt(index);
				return;
			}
			
			var direction = cell.RandomUninitializedDirection();
			var position = cell.position + direction.Vector();

			if (ContainsCoordinates(position)) {
				var neighbor = GetCell(position);

				if (neighbor) {
					CreateWall(cell, neighbor, direction);
				} else {
					neighbor = CreateCell(position);
					CreatePassage(cell, neighbor, direction);
					active.Add(neighbor);
				}
			} else {
				CreateWall(cell, null, direction);
			}
		}

		private MazeCell CreateCell(Vector2Int position) {
			var newCell = Instantiate(mazeCell);
			cells[position.x, position.y] = newCell;
			newCell.position = position;
			newCell.name = "Maze Cell " + position.x + ", " + position.y;
			newCell.transform.parent = transform;
			newCell.transform.localPosition =
				new Vector3(position.x - size.x * 0.5f + 0.5f, 0f, position.y - size.y * 0.5f + 0.5f);
			return newCell;
		}

		private void CreatePassage(MazeCell cell, MazeCell cellNext, MazeDirection direction) {
			var passage = Instantiate(mazePassage);
			passage.Initialize(cell, cellNext, direction);
			passage = Instantiate(mazePassage);
			passage.Initialize(cellNext, cell, direction.GetOpposite());
		}

		private void CreateWall(MazeCell cell, MazeCell cellNext, MazeDirection direction) {
			var wall = Instantiate(mazeWall);
			wall.Initialize(cell, cellNext, direction);
			if (cellNext == null) return;
			wall = Instantiate(mazeWall);
			wall.Initialize(cellNext, cell, direction.GetOpposite());
		}
	}
}