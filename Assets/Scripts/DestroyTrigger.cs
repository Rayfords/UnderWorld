using UnityEngine;

public class DestroyTrigger : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals("DestroyTriggeTag")) {
			Destroy(other.gameObject);
			if(other.gameObject.GetComponent<FishMovement>() != null)
				other.gameObject.GetComponent<FishMovement>().destroyText();
			else if(other.gameObject.GetComponent<CrabMovement>() != null)
				other.gameObject.GetComponent<CrabMovement>().destroyText();
			else if(other.gameObject.GetComponent<JellyfishMovement>() != null)
				other.gameObject.GetComponent<JellyfishMovement>().destroyText();
		}
	}
}
