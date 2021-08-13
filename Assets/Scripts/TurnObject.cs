using UnityEngine;

public class TurnObject : MonoBehaviour {
    public Vector3 posLastFame;
    public float speedTurn;

	// Update is called once per frame
	void Update() {
        if (Input.GetMouseButtonDown(1)) {
            posLastFame = Input.mousePosition;
        }
        if (Input.GetMouseButton(1)) {
            var delta = Input.mousePosition - posLastFame;
            posLastFame = Input.mousePosition;
            var axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
            transform.rotation = Quaternion.AngleAxis(delta.magnitude * speedTurn, axis) * transform.rotation;
        }
    }
}
