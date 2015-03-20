using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothDamperController : MonoBehaviour {
	public static SmoothDamperController main;

	private List<SmoothDamper> dampers;

	void Awake(){
		main = this;
		dampers = new List<SmoothDamper>();
	}


	// Update is called once per frame
	void Update () {
		for(int i = 0; i < dampers.Count; i++){
			dampers[i].Update();
		}
	}

	public void AddDamper(SmoothDamper s){
		dampers.Add(s);
	}

	public void RemoveDamper(SmoothDamper s){
		dampers.Remove(s);
	}

}

public abstract class SmoothDamper{
	public abstract void Update();
	public abstract void Remove();
}

public class FloatSmoothDamper : SmoothDamper{
	public float current;
	public float target;
	public float velocity;
	public float smoothtime;
	public bool useUnscaled;
	public FloatSmoothDamper(float init, float target, float smoothtime, bool useUnscaled = false){
		this.current = init;
		this.target = target;
		this.velocity = 0f;
		this.smoothtime = smoothtime;
		this.useUnscaled = useUnscaled;
		SmoothDamperController.main.AddDamper(this);
	}
	public override void Update(){
		if (!useUnscaled)
			current = Mathf.SmoothDamp(current, target, ref velocity, smoothtime);
		else
			current = Mathf.SmoothDamp(current, target, ref velocity, smoothtime, Mathf.Infinity, Time.unscaledDeltaTime);
	}
	public override void Remove(){
		SmoothDamperController.main.RemoveDamper(this);
	}
}


public class Vector3SmoothDamper : SmoothDamper{
	public Vector3 current;
	public Vector3 target;
	public Vector3 velocity;
	public float smoothtime;
	public bool useUnscaled;
	public Vector3SmoothDamper(Vector3 init, Vector3 target, float smoothtime, bool useUnscaled = false){
		this.current = init;
		this.target = target;
		this.velocity = Vector3.zero;
		this.smoothtime = smoothtime;
		this.useUnscaled = useUnscaled;
		SmoothDamperController.main.AddDamper(this);
	}
	public override void Update(){
		if (!useUnscaled)
			current = Vector3.SmoothDamp(current, target, ref velocity, smoothtime);
		else
			current = Vector3.SmoothDamp(current, target, ref velocity, smoothtime, Mathf.Infinity, Time.unscaledDeltaTime);
	}
	public override void Remove(){
		SmoothDamperController.main.RemoveDamper(this);
	}
}

