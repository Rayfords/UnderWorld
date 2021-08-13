using UnityEngine;
using UnityEngine.UI;

public class JellyfishMovement : MonoBehaviour {
    public float rotationX, rotationY, rotationZ;
    public float moveYfloat = 0.08f;
    public Text nameFish;
    public Vector3 offsetText;
    private Text tempNameFish;
    private bool bMoveX = true;
    private bool bChangeTexture;
    void Start() {
        rotationX = Random.Range(rotationX - 2, rotationX + 2);
        rotationY = Random.Range(rotationY - 2, rotationY + 2);
        rotationZ = Random.Range(rotationZ - 2, rotationZ + 2);
        gameObject.transform.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);
        //tempNameFish = Instantiate(nameFish, FindObjectOfType<Canvas>().transform).GetComponent<Text>();
        changeText();
        tempNameFish.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        bChangeTexture = false;
        Destroy(gameObject, 100f);
    }

    // Update is called once per frame
    void Update() {
        if (gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>() != null &&
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material != null &&
            FindObjectOfType<ParticePaint>() != null &&
            FindObjectOfType<ParticePaint>().getChangedTexture() != null) {
            if (bChangeTexture) {
                Texture2D tempTexture = FindObjectOfType<ParticePaint>().getChangedTexture();
                gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = tempTexture;
                tempTexture.Apply();
                bChangeTexture = false;
            }
        }
        gameObject.transform.Translate(new Vector3(0, moveYfloat, 0) * Time.deltaTime);
        float secY = Random.Range(1f, 3f);
        if (bMoveX) {
            Invoke("moveY", secY);
            bMoveX = false;
        }
        //tempNameFish.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsetText);
    }

    void changeText() {
        tempNameFish.text = "";
        int i = 0;
        while (i < gameObject.name.Length && gameObject.name[i] != '(')
            tempNameFish.text += gameObject.name[i++];
    }

    void moveY() {
        moveYfloat = Random.Range(0.08f, 0.09f);
        bMoveX = true;
    }

    public void destroyText() {
        Destroy(tempNameFish.gameObject);
    }

    public void changeTexture() {
        bChangeTexture = true;
    }
}
