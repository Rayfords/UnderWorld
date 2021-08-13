 using UnityEngine;
 using UnityEngine.UI;

public class CrabMovement : MonoBehaviour {
    public float moveXfloat = 0.08f;
    public float direction = 1f;
    public Text nameFish;
    public Vector3 offsetText;
    private Text tempNameFish;
    private bool bMoveX = true;
    private bool bChangeTexture;
    void Start() {
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
        gameObject.transform.Translate(new Vector3(moveXfloat * direction, 0, 0) * Time.deltaTime);
        float secX = Random.Range(1f, 3f);
        if (bMoveX) {
            Invoke("moveX", secX);
            bMoveX = false;
        }
        tempNameFish.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsetText);
    }

    public void changeTexture() {
        bChangeTexture = true;
    }

    void changeText() {
        tempNameFish.text = "";
        int i = 0;
        while (i < gameObject.name.Length && gameObject.name[i] != '(')
            tempNameFish.text += gameObject.name[i++];
    }

    void moveX() {
        moveXfloat = Random.Range(0.08f, 0.09f);
        bMoveX = true;
    }

    public void destroyText() {
        Destroy(tempNameFish.gameObject);
    }
}
