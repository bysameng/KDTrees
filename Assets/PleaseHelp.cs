using UnityEngine;
using System.Collections;

public class PleaseHelp : MonoBehaviour {

	public TextMesh text;
	public TextAsset texts;

	private string[] lines;

	void Awake(){
		lines = texts.text.Split ('\n');
	}

	void Start(){
		Invoke("ChangeText", 5f);
	}

	void ChangeText(){
		text.text = Random.value > .95f ? lines[Random.Range(0, lines.Length-1)] : "";
		Invoke("ChangeText", 5f);
	}
}
