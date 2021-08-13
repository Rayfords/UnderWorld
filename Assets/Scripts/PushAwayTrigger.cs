using UnityEngine;

public class PushAwayTrigger : MonoBehaviour {
	public float xAddForce, yAddForce, zAddForce;
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<FishMovement>() != null)
			other.gameObject.GetComponent<FishMovement>().rb.AddForce(xAddForce, yAddForce, zAddForce, ForceMode.VelocityChange);
	}
}
