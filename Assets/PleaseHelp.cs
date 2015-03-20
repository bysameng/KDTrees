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
		Invoke("ChangeText", 1f);
	}

	void ChangeText(){
		text.text = lines[Random.Range(0, lines.Length-1)];
		Invoke("ChangeText", 1f);
	}
}
