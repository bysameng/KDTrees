using UnityEngine;
using System.Collections;

public class CubeThrower : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T)){
			GameObject proj = (GameObject)Instantiate(ColorSpawner.main.spawnObj, transform.position, Quaternion.Euler(Random.insideUnitSphere));
			proj.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
			proj.GetComponent<MeshManipulator>().SetColor(new Color(Random.value, Random.value, Random.value));
			proj.GetComponent<CubePositionFader>().enabled = false;
			if (Input.GetKey(KeyCode.Y)){
				proj.transform.localScale = new Vector3(4f, 4f, 4f);
			}
		}
	}
}
