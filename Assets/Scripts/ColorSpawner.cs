using UnityEngine;
using System.Collections;
using KDTreeDLL;

public class ColorSpawner : MonoBehaviour {
	public static ColorSpawner main;

	public GameObject spawnObj;
	public KDTree tree{get; private set;}
	public MouseOrbit orb;

	public GameObject ui;

	public GameObject cubes;

	void Awake(){main = this;}

	// Use this for initialization
	IEnumerator start (int steps = 5, bool random = false){
		tree = new KDTree(3);
		int spacing = 6;
		if (cubes != null) Destroy(cubes);
		cubes = new GameObject("Cubes");
		for(int i = 0; i < steps; i++){
			for(int j = 0; j < steps; j++){
				for(int k = 0; k < steps; k++){
					float s = (float) steps;
					Color c = new Color(i / s, j / s, k / s);
					if (random) c = new Color(Random.value, Random.value, Random.value);
					GameObject cube = (GameObject)Instantiate(spawnObj, new Vector3(i * spacing, j * spacing, k * spacing), Quaternion.identity);
					cube.transform.parent = cubes.transform;
					cube.GetComponent<MeshManipulator>().SetColor(c);
					tree.insert(c.GetKey(), cube);
				}
			}
		}
		float center = ((steps-1) / 2f)*spacing;
		cubes.transform.Translate(-new Vector3(center, center, center));
		orb.target = this.transform;
		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H)){
			ui.gameObject.SetActive(!ui.gameObject.activeInHierarchy);
		}
		if (Input.GetKeyDown(KeyCode.Alpha1)){

			orb.distance = 40;
			StartCoroutine(start());
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			orb.distance = 80;
			StartCoroutine(start(10, true));
		}
		if (Input.GetKeyDown(KeyCode.O)){
			Application.LoadLevel(Application.loadedLevel);
		}
	}

}

public static class ExtensionMethods{

	public static double[] GetKey(this Color c){
		double[] keys = new double[3];
		keys[0] = c.r;
		keys[1] = c.g;
		keys[2] = c.b;
		return keys;
	}
}


