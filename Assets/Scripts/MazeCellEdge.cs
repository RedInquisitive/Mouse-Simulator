using UnityEngine;

namespace Assets.Scripts {
	public abstract class MazeCellEdge : MonoBehaviour {
		
		private MazeCell cell, cellNext;
		private MazeDirection direction;

		public void Initialize(MazeCell cell, MazeCell cellNext, MazeDirection direction) {
			this.cell = cell;
			this.cellNext = cellNext;
			this.direction = direction;
			cell.SetEdge(direction, this);
			transform.parent = cell.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = direction.ToRotation();
		}
	}
}