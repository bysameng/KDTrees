using UnityEngine;
using System.Collections;
using KDTreeDLL;

public class ColorSpawner : MonoBehaviour {
	public static ColorSpawner main;

	public GameObject spawnObj;
	public KDTree tree{get; private set;}
	public MouseOrbit orb;

	void Awake(){main = this;}

	// Use this for initialization
	void Start () {
		tree = new KDTree(3);
		int steps = 5;
		int spacing = 6;
		GameObject cubes = new GameObject("Cubes");
		for(int i = 0; i < steps; i++){
			for(int j = 0; j < steps; j++){
				for(int k = 0; k < steps; k++){
					float s = (float) steps;
					Color c = new Color(i / s, j / s, k / s);
					GameObject cube = (GameObject)Instantiate(spawnObj, new Vector3(i * spacing, j * spacing, k * spacing), Quaternion.identity);
					cube.transform.parent = cubes.transform;
					cube.renderer.material.color = c;
					tree.insert(c.GetKey(), cube);
				}
			}
		}
		float center = ((steps-1) / 2f)*spacing;
		cubes.transform.Translate(-new Vector3(center, center, center));
		orb.target = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
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


