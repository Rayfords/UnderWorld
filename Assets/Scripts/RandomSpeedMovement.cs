using UnityEngine;
using UnityEngine.UI;

public class RandomSpeedMovement : MonoBehaviour {
    [SerializeField] private float horizontalMovement = 0.4f;
    [SerializeField] private Text nameFish;
    [SerializeField] private Vector3 nameOffset; // Offset of name label relative to object center position
    [SerializeField] private Vector2 speedRange; // Range of possible speed
    
    private Text fishName;
    
    // Variables to highlight that movement value is calculated
    private bool shouldMoveHorizontally = true;
    private bool shouldMoveVertically = true;
    private bool shouldRotate = true;
    
    private Vector3 rotateDirection;
    
    private bool shouldChangeTexture; // True when user clicks "Set Texture" button
    
    private float speed; // Randomly chosen speed 
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Get speed randomly so that each object moved differently
        speed = Random.Range(speedRange.x, speedRange.y);
        
        setName();
        
        // Set initial position
        fishName.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        
        shouldChangeTexture = false;
    }

	void Update() {
        // Check whether texture should be changed
        if (isTextureChanged() && shouldChangeTexture) {
            applyNewTexture();
        }
        
        applyMovement();
        textFollow();
        
        applyRotation();
    }

    // Sets name label for object
    void setName() {
        fishName = Instantiate(nameFish, FindObjectOfType<Canvas>().transform).GetComponent<Text>();
        fishName.text = "";
        int i = 0;
        while (i < gameObject.name.Length && gameObject.name[i] != '(')
            fishName.text += gameObject.name[i++];
    }

    bool isTextureChanged() =>
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>()?.material != null &&
        FindObjectOfType<ParticePaint>()?.getChangedTexture() != null;

    void applyNewTexture()
    {
        Texture2D newTexture = FindObjectOfType<ParticePaint>().getChangedTexture();
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = newTexture;
        newTexture.Apply();
        shouldChangeTexture = false;
    }

    void applyMovement()
    {
        // Transition between current and desired position
        gameObject.transform.Translate(new Vector3(horizontalMovement * speed, 0, 0) * Time.deltaTime);
        
        // Calculate next move turn
        float delayHorizontalMove = Random.Range(1f, 3f);
        if (shouldMoveHorizontally) {
            Invoke("randomMoveHorizontal", delayHorizontalMove);
            shouldMoveHorizontally = false;
        }
        float delatVerticalMove = Random.Range(0.4f, 0.5f);
        if (shouldMoveVertically) {
            Invoke("randomMoveVertical", delatVerticalMove);
            shouldMoveVertically = false;
        }
    }
    
    // Calculate horizontal movement
    void randomMoveHorizontal() {
        horizontalMovement = Random.Range(0.25f, 0.4f);
        shouldMoveHorizontally = true;
    }

    // Calculate and apply vertical movement
    void randomMoveVertical() {
        float verticalMovement = Random.Range(-0.005f, 0.005f);
        rb.AddForce(0, verticalMovement, 0, ForceMode.VelocityChange);
        shouldMoveVertically = true;
    }

    // Name label should follow object
    void textFollow()
    {
        fishName.transform.position = Camera.main.WorldToScreenPoint(transform.position + nameOffset);
    }

    // Calculate apd apply rotation
    void applyRotation() {
        if (shouldRotate) {
            Invoke("selectRotation", 5f);
            shouldRotate = false;
        }
        
        Vector3 dir = rotateDirection - gameObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, 0.1f * Time.deltaTime);
    }
        
    void selectRotation() {
        rotateDirection = new Vector3(Random.Range(gameObject.transform.position.x - 1f, gameObject.transform.position.x + 2f), gameObject.transform.position.y, gameObject.transform.position.z);
        shouldRotate = true;
    }

    // Called when user disables name label
    public void destroyText() {
        Destroy(fishName.gameObject);
	}
    
    // Called when user presses "Set Texture" button
    public void changeTexture() {
        shouldChangeTexture = true;
    }
}