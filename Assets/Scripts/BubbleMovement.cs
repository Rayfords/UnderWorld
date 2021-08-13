using UnityEngine;

public class BubbleMovement : MonoBehaviour {
    public Rigidbody rb;
    public float moveYfloat = 0.15f;
    public float pushY;
    private bool bMoveX = true;
    private bool bMoveY = true;
    // Start is called before the first frame update
    void Start() {
        float randSize = Random.Range(0.03f, 0.122f);
        gameObject.transform.localScale = new Vector3(randSize, randSize, randSize);
        rb.AddForce(0, pushY / randSize, 0, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update() {
        gameObject.transform.Translate(new Vector3(0, moveYfloat, 0) * Time.deltaTime);
        float secX = Random.Range(1f, 3f);
        if (bMoveX) {
            Invoke("moveY", secX);
            bMoveX = false;
        }
        float secY = Random.Range(0.4f, 0.5f);
        if (bMoveY) {
            Invoke("moveX", secY);
            bMoveY = false;
        }
    }
    void moveY() {
        moveYfloat = Random.Range(0.1f, 0.15f);
        bMoveX = true;
    }

    void moveX() {
        float moveXfloat = Random.Range(-0.005f, 0.005f);
        rb.AddForce(moveXfloat, 0, 0, ForceMode.VelocityChange);
        bMoveY = true;
    }
}