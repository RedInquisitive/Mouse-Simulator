using UnityEngine;

namespace Assets.Scripts {
	public class MazeCell : MonoBehaviour {
		internal Vector2Int position;

		internal bool IsFullyInitialized => initializedEdgeCount == MazeDirections.Count;

		private readonly MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

		private int initializedEdgeCount;

		public MazeCellEdge GetEdge(MazeDirection direction) {
			return edges[(int)direction];
		}

		public void SetEdge(MazeDirection direction, MazeCellEdge edge) {
			edges[(int)direction] = edge;
			initializedEdgeCount += 1;
		}

		public MazeDirection RandomUninitializedDirection() {
			var skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
			for (var i = 0; i < MazeDirections.Count; i++) {
				if (edges[i] != null) continue;
				if (skips == 0) return (MazeDirection)i;
				skips -= 1;
			}

			throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
		}
	}
}