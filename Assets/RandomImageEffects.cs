using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class RandomImageEffects : MonoBehaviour {

	public EdgeDetection edged;
	public FloatSmoothDamper edgeExpDamper;
	public FloatSmoothDamper sampleDistDamper;

	void Start(){
		Invoke("RandomizeEdge", 2f);
		edgeExpDamper = new FloatSmoothDamper(0f, .5f, 5f, true);
		sampleDistDamper = new FloatSmoothDamper(0f, .5f, 5f, true);
	}

	public void Update(){
		edged.edgeExp = edgeExpDamper.current;
		edged.sampleDist = sampleDistDamper.current;
	}

	public void RandomizeEdge(){
		edgeExpDamper.target = Random.Range(0f, 2f);
		sampleDistDamper.target = Random.value < .9f ? Random.Range(.9f, 1.1f) : Random.value > .5f ? .1f : 10f;
		Invoke("RandomizeEdge", 5f);
	}

}
