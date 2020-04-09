using UnityEngine;

namespace Assets.Scripts {
	public class GameManager : MonoBehaviour {
		public Maze maze;
		private Maze instance;

		private void Start() {
			BeginGame();
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				RestartGame();
			}
		}

		private void BeginGame() {
			instance = Instantiate(maze);
			StartCoroutine(instance.Generate());
		}

		private void RestartGame() {
			StopAllCoroutines();
			Destroy(instance.gameObject);
			BeginGame();
		}
	}
}
