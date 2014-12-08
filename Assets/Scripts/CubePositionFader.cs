using UnityEngine;
using System.Collections;

public class CubePositionFader : MonoBehaviour {

	public Vector3 target;
	public Vector3 velocity;
	public float smoothTime = .5f;

	// Use this for initialization
	void Start () {
		target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
	}

	void OnCollisionEnter(){
		Destroy(this);
	}
}
