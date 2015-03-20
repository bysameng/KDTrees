using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	public SpriteRenderer spr;
	float velocity;
	float target = 1f;
	float timer = 2f;

	// Use this for initialization
	void Start () {
		Invoke("StartFade", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= 0)
			timer -= Time.unscaledDeltaTime;
		if (timer < 0){
			StartFade();
			timer = 0;
		}
		Color c = spr.color;
		c.a = Mathf.SmoothDamp(c.a, target, ref velocity, 1f, Mathf.Infinity, Time.unscaledDeltaTime);
		spr.color = c;
		if (c.a < .01f) Destroy(this.gameObject);
	}


	void StartFade(){
		target = 0f;
	}
}
