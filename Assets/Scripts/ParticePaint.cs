using System.Threading.Tasks;
using UnityEngine;

public class ParticePaint : MonoBehaviour {
    private Texture2D workingTexture;
    private Renderer mioRenderer;
    Texture2D texture;
    private Color colorPaint;
    // Start is called before the first frame update
    void Start() {
        colorPaint = new Color(0, 0, 0, 0);
        mioRenderer = GetComponent<Renderer>();
        texture = mioRenderer.material.mainTexture as Texture2D;
        workingTexture = new Texture2D(texture.width, texture.height);

        Color32[] sourcePixels = texture.GetPixels32();
        workingTexture.SetPixels32(sourcePixels);
        mioRenderer.material.mainTexture = workingTexture;
        workingTexture.Apply();
    }

    private void changePixels(Vector3 pos, Vector2 pixelUV, Vector2 pixelPoint, Ray Ray, RaycastHit hit, int diametr) {
        float wait = -((diametr + diametr) / diametr);
        for (float i = -diametr; i < diametr; i += 0.2f) {
            for (float j = -diametr; j < diametr; j += 0.2f) {
                Ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(i, j, 0));
                if (Physics.Raycast(Ray, out hit)) {
                    pos = hit.point;
                    pixelUV = hit.textureCoord;
                    pixelPoint = new Vector2(pixelUV.x * texture.width, pixelUV.y * texture.height);
                    workingTexture.SetPixel((int)pixelPoint.x, (int)pixelPoint.y, colorPaint);
                }
            }
        }
        workingTexture.Apply();
    }

    //Оновлення викликається один раз на кадр
    void Update() {
        gameObject.GetComponent<Renderer>().material.mainTexture = workingTexture;
        //Змінює колір пензлика.
        if (FindObjectOfType<ManagerMenu>() != null)
            colorPaint = FindObjectOfType<ManagerMenu>().getSelectedColor();
        //При натискані на ліву клавішу миші випускається промінь.
        if (Input.GetMouseButton(0)) {
            Vector2 pixelUV = new Vector2();
            Vector2 pixelPoint = new Vector2();
            //Отримується промінь за позицією миші.
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            //Отримується діаметр пензлика.
            int diametr = FindObjectOfType<ManagerMenu>().getDiametr();
            //Розраховується діаметр пензлика.
            for (float i = -diametr; i < diametr; i += 0.2f) {
                for (float j = -diametr; j < diametr; j += 0.2f) {
                    Ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(i, j, 0));
                    if (Physics.Raycast(Ray, out hit)) {
                        pixelUV = hit.textureCoord;
                        pixelPoint = new Vector2(pixelUV.x * texture.width, pixelUV.y * texture.height);
                        workingTexture.SetPixel((int)pixelPoint.x, (int)pixelPoint.y, colorPaint);
                    }
                }
            }
            workingTexture.Apply();
        }
    }

    public Texture2D getChangedTexture() => workingTexture;

    public async void setWorkingTexture(Texture2D tempTexture) {
        FindObjectOfType<ManagerMenu>().GetComponent<ManagerMenu>().loadingMenu();
        int wait = 2;
        for (int x = 0; x < workingTexture.width; x++) {
            for (int y = 0; y < workingTexture.height; y++)
                if (tempTexture.GetPixel(x, y).a != 0f)
                    workingTexture.SetPixel(x, y, new Color(tempTexture.GetPixel(x, y).r, tempTexture.GetPixel(x, y).g, tempTexture.GetPixel(x, y).b));
            if(wait == x) {
                wait += wait;
                await Task.Yield();
            }
        }
        workingTexture.Apply();
        FindObjectOfType<ManagerMenu>().GetComponent<ManagerMenu>().loadingMenu();
    }
}
