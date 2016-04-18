using UnityEngine;
using System.Collections;

public static class ShapeUtils {
	public static bool IsOnPlatform (GameObject player) {
		RaycastHit2D hit = Physics2D.Raycast (player.transform.position, Vector2.down, GetDistanceToGround(player) + 0.1f);

		if (hit.collider && (hit.collider.gameObject.tag == "Platform" || hit.collider.gameObject.tag == "Player")) {
			return true;
		}

		return false;
	}

	private static float GetDistanceToGround (GameObject player) {
		return player.GetComponent<Collider2D> ().bounds.extents.y;
	}
}
