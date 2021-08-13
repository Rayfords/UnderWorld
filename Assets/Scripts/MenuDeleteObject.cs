using UnityEngine;

public class MenuDeleteObject : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab) && !FindObjectOfType<ManagerMenu>().GetComponent<ManagerMenu>().isLoadingMenuOpen())
            Destroy(gameObject);
    }
    public void DestroyObject() {
        Destroy(gameObject);
    }
}