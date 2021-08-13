using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMenu : MonoBehaviour {
	public GameObject cameraObject;
	public GameObject gameObjectUI;
	public GameObject gameObjectUI2;
	public GameObject loadingPanel;
	public Text nameFish;
	public GameObject[] objects;
	public FishSpawn[] spawns;
	public Canvas ListName;
	public Image textureImage;
	private Texture2D selectedTexture;
	private int indexArrFish = 0, indexArrTexture = 0;
	private GameObject selectedObj;
	private GameObject createdObj;
	private Color selectedColor;
	private int diametrPaint;
	private List<List<Texture2D>> textureMenu = new List<List<Texture2D>>();

	// Start is called before the first frame update
	void Start() {
		diametrPaint = 1;
		selectedColor = new Color(0f, 0f, 0f, 1f);
		ListName.enabled = false;
		gameObjectUI.SetActive(false);
		gameObjectUI.GetComponent<Image>().color = new Color(selectedColor.r, selectedColor.g, selectedColor.b, 0.6f);
		loadingPanel.GetComponent<Image>().color = gameObjectUI.GetComponent<Image>().color;
		BatchLoadImages();
		changeSelectedTextureMenu();
	}

	public void loadingMenu() {
		if (loadingPanel.activeInHierarchy) {
			loadingPanel.SetActive(false);
			gameObjectUI.SetActive(true);
			createdObj.SetActive(true);
		} else {
			loadingPanel.SetActive(true);
			gameObjectUI.SetActive(false);
			createdObj.SetActive(false);
		}
	}

	public bool isLoadingMenuOpen() => loadingPanel.activeInHierarchy;

	public void setSelectedTexture() {
		FindObjectOfType<ParticePaint>().GetComponent<ParticePaint>().setWorkingTexture(selectedTexture);
	}

	void changeSelectedTextureMenu() {
		selectedTexture = textureMenu[indexArrFish][indexArrTexture];
		textureImage.overrideSprite = Sprite.Create(selectedTexture, new Rect(0.0f, 0.0f, selectedTexture.width, selectedTexture.height), new Vector2(0.5f, 0.5f));
	}

	public void UptButtonClick() {
		indexArrTexture = (indexArrTexture + 1) % textureMenu[indexArrFish].Count;
		changeSelectedTextureMenu();
	}

	public void DowntButtonClick() {
		indexArrTexture = indexArrTexture - 1 < 0 ? textureMenu[indexArrFish].Count - 1 : indexArrTexture - 1;
		changeSelectedTextureMenu();
	}

	void BatchLoadImages() {
		int counterFolder = 0;
		string folderPath = System.IO.Directory.GetCurrentDirectory() + @$"\Assets\Textures\MenuTextures\{counterFolder}";
		while (Directory.Exists(folderPath)) {
			string[] dirPaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);
			if (dirPaths.Length > 0) {
				List<Texture2D> tempList = new List<Texture2D>();
				int counter = 0;
				foreach (string path in dirPaths) {
					string url = "file://" + path;
					Texture2D newTexture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
					WWW www = new WWW(url);
					www.LoadImageIntoTexture(newTexture);
					newTexture.name = counter.ToString();
					tempList.Add(newTexture);
				}
				textureMenu.Add(tempList);
				folderPath = System.IO.Directory.GetCurrentDirectory() + @$"\Assets\Textures\MenuTextures\{++counterFolder}";
			}
		}
	}


	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab) && !isLoadingMenuOpen()) {
			if (gameObjectUI.activeInHierarchy)
				gameObjectUI.SetActive(false);
			else if (gameObjectUI2.activeInHierarchy)
				gameObjectUI2.SetActive(false);
			else {
				DrawObject();
				gameObjectUI.SetActive(true);
				Debug.Log(indexArrFish);
			}
		}
		foreach (var gameObj in FindObjectsOfType(typeof(Text)) as Text[]) {
			if (gameObj.name == "diametrCount")
				gameObj.text = diametrPaint.ToString();
		}
	}

	void DrawObject() {
		//Створюється тимчасовій об'єкт типу "Vector3" для того, щоб отримати позицію камери.
		Vector3 tempObj = cameraObject.transform.position;
		//Змінюється позиція об'єкта для того, щоб він був подалі від камери.
		tempObj = new Vector3(tempObj.x, tempObj.y + 0.1f, tempObj.z + 1f);
		//Запам'ятовуємо обраний об'єкт.
		selectedObj = objects[indexArrFish];
		//Змінюємо розмір об'єкта.
		selectedObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		//Створюється об'єкт на сцені.
		createdObj = Instantiate(selectedObj, tempObj, Quaternion.identity);
		//Змінюється ім'я об'єкта.
		nameFish.text = selectedObj.name;
	}

	public void RightButtonClick() {
		FindObjectOfType<MenuDeleteObject>().GetComponent<MenuDeleteObject>().DestroyObject();
		indexArrFish = (indexArrFish + 1) % objects.Length;
		indexArrTexture = indexArrTexture  % textureMenu[indexArrFish].Count;
		changeSelectedTextureMenu();
		DrawObject();
		Debug.Log(indexArrFish);
	}

	public void LeftButtonClick() {
		FindObjectOfType<MenuDeleteObject>().GetComponent<MenuDeleteObject>().DestroyObject();
		indexArrFish = indexArrFish - 1 < 0 ? objects.Length - 1 : indexArrFish - 1;
		indexArrTexture = indexArrTexture % textureMenu[indexArrFish].Count;
		changeSelectedTextureMenu();
		DrawObject();
		Debug.Log(indexArrFish);
	}

	public void SpawnObject() {
		int count = 0;
		for (int j = 0; j < spawns[indexArrFish].obj.Length; j++) {
			if (spawns[indexArrFish].obj[j].name == selectedObj.name)
				break;
			++count;
		}
		if (FindObjectOfType<ParticePaint>() != null)
			spawns[indexArrFish].spawnObj(count, FindObjectOfType<ParticePaint>().getChangedTexture());
	}

	public GameObject getSelectedObj() => selectedObj;

	public void hideName() {
		ListName.enabled = !ListName.enabled;
	}

	public void changeColorClick() {
		if (gameObjectUI.activeInHierarchy) {
			FindObjectOfType<MenuDeleteObject>().GetComponent<MenuDeleteObject>().DestroyObject();
			gameObjectUI2.SetActive(true);
			gameObjectUI.SetActive(false);
		} else {
			gameObjectUI2.SetActive(false);
			gameObjectUI.SetActive(true);
			DrawObject();
		}
	}

	public void changeDiametr(float diametr) {
		diametrPaint = (int)diametr;
	}

	public int getDiametr() => diametrPaint;

	public void changeColor(string RGB) {
		string[] ar = RGB.Split(',');
		float Red = float.Parse(ar[0]), Green = float.Parse(ar[1]), Blue = float.Parse(ar[2]);
		selectedColor = new Color(Red, Green, Blue, 255f);
	}

	public void changeColorR(float R) {
		selectedColor = new Color(R, selectedColor.g, selectedColor.b, 1f);
		gameObjectUI.GetComponent<Image>().color = new Color(selectedColor.r, selectedColor.g, selectedColor.b, 0.6f);
		loadingPanel.GetComponent<Image>().color = gameObjectUI.GetComponent<Image>().color;
	}
	public void changeColorG(float G) {
		selectedColor = new Color(selectedColor.r, G, selectedColor.b, 1f);
		gameObjectUI.GetComponent<Image>().color = new Color(selectedColor.r, selectedColor.g, selectedColor.b, 0.6f);
		loadingPanel.GetComponent<Image>().color = gameObjectUI.GetComponent<Image>().color;
	}
	public void changeColorB(float B) {
		selectedColor = new Color(selectedColor.r, selectedColor.g, B, 1f);
		gameObjectUI.GetComponent<Image>().color = new Color(selectedColor.r, selectedColor.g, selectedColor.b, 0.6f);
		loadingPanel.GetComponent<Image>().color = gameObjectUI.GetComponent<Image>().color;
	}

	public Color getSelectedColor() => selectedColor;

	IEnumerator SaveTextureToFile(Texture2D savedTexture) {
		string fullPath = System.IO.Directory.GetCurrentDirectory();
		System.DateTime date = System.DateTime.Now;
		string fileName = "123123123.png";
		if (!System.IO.Directory.Exists(fullPath))
			System.IO.Directory.CreateDirectory(fullPath);
		var bytes = savedTexture.EncodeToPNG();
		System.IO.File.WriteAllBytes(fullPath + @"\" + fileName, bytes);
		yield return null;
	}

	public void clickSetTexture() {
		//StartCoroutine("SaveTextureToFile", FindObjectOfType<ParticePaint>().getChangedTexture());
	}
}