using UnityEngine;

public class FishSpawn : MonoBehaviour {
    public GameObject []obj;
    public Vector3 center;
    public Vector3 size;
    public float timeFrom = 0f, timeTo = 0f;
    bool bSpawn = true;
    private int indexArr = 0;
	// Start is called before the first frame update
	void Start() {
        spawnObject();
    }

	// Update is called once per frame
	void Update() {
        if (bSpawn) {
            indexArr = Random.Range(0, obj.Length);
            Invoke("spawnObject", Random.Range(timeFrom, timeTo));
            bSpawn = false;
        }
    }
    public void spawnObject() {
        spawnObj(indexArr);
        bSpawn = true;
    }
    public void spawnObj(int tindex) {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(obj[tindex], pos, Quaternion.identity);
    }

    public void changeTexture()
    {
        FishMovement[] fishMovement = FindObjectsOfType<FishMovement>();
        CrabMovement[] crabMovement = FindObjectsOfType<CrabMovement>();
        JellyfishMovement[] jellyfishMovement = FindObjectsOfType<JellyfishMovement>();
        for (int i = 0; i < fishMovement.Length; i++)
        {
            string tempString = "";
            int j = 0;
            while (fishMovement[i].name[j] != '(')
                tempString += fishMovement[i].name[j++];
            if (FindObjectOfType<ManagerMenu>().GetComponent<ManagerMenu>().getSelectedObj().name == tempString)
                fishMovement[i].changeTexture();
        }
        for (int i = 0; i < crabMovement.Length; i++)
        {
            string tempString = "";
            int j = 0;
            while (crabMovement[i].name[j] != '(')
                tempString += crabMovement[i].name[j++];
            if (FindObjectOfType<ManagerMenu>().GetComponent<ManagerMenu>().getSelectedObj().name == tempString)
                crabMovement[i].changeTexture();
        }
        for (int i = 0; i < jellyfishMovement.Length; i++)
        {
            string tempString = "";
            int j = 0;
            while (jellyfishMovement[i].name[j] != '(')
                tempString += jellyfishMovement[i].name[j++];
            if (FindObjectOfType<ManagerMenu>().GetComponent<ManagerMenu>().getSelectedObj().name == tempString)
                jellyfishMovement[i].changeTexture();
        }
    }
    public void spawnObj(int tindex, Texture2D newTexture) {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(obj[tindex], pos, Quaternion.identity);
    }
	private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}