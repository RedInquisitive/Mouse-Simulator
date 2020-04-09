using UnityEngine;

namespace Assets.Scripts {

	public enum MazeDirection {
		North,
		East,
		South,
		West
	}

	public static class MazeDirections {
		private static readonly Vector2Int[] vectors = {
			new Vector2Int(0, 1),
			new Vector2Int(1, 0),
			new Vector2Int(0, -1),
			new Vector2Int(-1, 0)
		};

		private static readonly MazeDirection[] opposites = {
			MazeDirection.South,
			MazeDirection.West,
			MazeDirection.North,
			MazeDirection.East
		};

		private static readonly Quaternion[] rotations = {
			Quaternion.identity,
			Quaternion.Euler(0f, 90f, 0f),
			Quaternion.Euler(0f, 180f, 0f),
			Quaternion.Euler(0f, 270f, 0f)
		};

		public static int Count => vectors.Length;

		public static Vector2Int Vector(this MazeDirection direction) {
			return vectors[(int)direction];
		}

		public static MazeDirection GetOpposite(this MazeDirection direction) {
			return opposites[(int)direction];
		}

		public static Quaternion ToRotation(this MazeDirection direction) {
			return rotations[(int)direction];
		}
	}
}