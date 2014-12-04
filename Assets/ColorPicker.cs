using UnityEngine;
using System.Collections;

public class ColorPicker : MonoBehaviour {

	private MouseOrbit orb;
	public bool pickingColor = false;
	private GameObject cubePicker;


	void Start () {
		orb = ColorSpawner.main.orb;
		cubePicker = (GameObject)Instantiate(ColorSpawner.main.spawnObj, new Vector3(-30f, -30f, -30f), Quaternion.identity);
		cubePicker.transform.renderer.material.color = Color.black;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.F)){
			orb.target = cubePicker.transform;
			orb.distance = 1f;
			pickingColor = true;
		}


		if (pickingColor){
			Debug.Log("???");
			float speed = 10f * Time.deltaTime;

			Color c = cubePicker.renderer.material.color;

			if (Input.GetKey(KeyCode.Space)){
				c = new Color(Random.Range(0, 1f),
				              Random.Range(0, 1f),
				              Random.Range(0, 1f));
			}

			cubePicker.renderer.material.color = c;

			if (Input.GetKeyDown(KeyCode.L)){
				LocateColor(cubePicker.renderer.material.color);
			}
		}
	}

	void LocateColor(Color c){
		GameObject closest = ColorSpawner.main.tree.nearest(c.GetKey()) as GameObject;
		cubePicker.GetComponent<CubePositionFader>().target = closest.transform.position + Camera.main.transform.forward * 2f;
		orb.target = closest.transform;
		orb.distance = 5f;
	}
}
