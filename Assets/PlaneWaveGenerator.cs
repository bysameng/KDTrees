using UnityEngine;
using System.Collections;

public class PlaneWaveGenerator : MonoBehaviour {
	public Mesh mesh;
	public GameObject debugCube;
	private Vector3[] verts;
	private Vector3[] originalverts;
	private Color32[] colors32;
	Color hue1;
	Color hue2;
	Color hue3;
	Color hue4;

	private Vector3SmoothDamper[] vertDamper;

	HSBColor h1;
	private float h1changetimer = 5f;
	private Vector3SmoothDamper h1damper;

	HSBColor h2;
	private Vector3SmoothDamper h2damper;

	HSBColor h3;
	private Vector3SmoothDamper h3damper;

	HSBColor h4;
	private Vector3SmoothDamper h4damper;

	public float smooth = 1.5f;
	private FloatSmoothDamper smoothingDamper;

	public float scale = 6f;
	private FloatSmoothDamper scaleDamper;

	public float speed = 2f;
	private FloatSmoothDamper speedDamper;

	public float devSpeed = 5f;

	private float random;
	public float timer;

	private FloatSmoothDamper timeDamper;

	float scalar1;
	float scalar2;
	float changeTimer = 10f;


	private Camera maincamera;

	private float rotationSpeed = 0;
	private float resetTimer = 10f;
	private Vector3 resetVel = Vector3.zero;

	public float smoothTime = 5f;


	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		Cursor.visible = false;
		maincamera = Camera.main;

		speedDamper = new FloatSmoothDamper(Random.Range(.01f, .2f), Random.Range(.01f, 1f), smoothTime * 2, true);
		smoothingDamper = new FloatSmoothDamper(Random.Range(0f, 2f), Random.Range(0f, 5f), smoothTime * 2, true);
		scaleDamper = new FloatSmoothDamper(Random.Range(0f, 5f), Random.Range(0f, 10f), smoothTime * 2, true);
		timeDamper = new FloatSmoothDamper(.1f, .2f, smoothTime * 2, true);

		ChangeSpeed();

		scalar1 = Random.Range(0.001f, 0.01f);
		scalar2 = Random.Range(0.001f, 0.01f);
		timer = Random.Range(-10f, 10f);

		hue1 = new Color(.5f, .5f, .5f);
		hue2 = new Color(.9f, .5f, .4f);

		h1 = new HSBColor(hue1);
		h2 = new HSBColor(hue1);
		h3 = new HSBColor(hue1);
		h4 = new HSBColor(hue1);
		h3.b = .1f;

		h1damper = new Vector3SmoothDamper(h1, h1, smoothTime, true);
		h2damper = new Vector3SmoothDamper(h2, h2, smoothTime, true);
		h3damper = new Vector3SmoothDamper(h3, h3, smoothTime/3f, true);
		h4damper = new Vector3SmoothDamper(h4, h4, smoothTime*2f, true);

		RandomizeColors();

		h1damper.current = h1;
		h2damper.current = h2;
		h3damper.current = h3;
		h4damper.current = h4;


		mesh = gameObject.GetComponent<MeshFilter>().mesh;
		mesh.MarkDynamic();
		verts = new Vector3[mesh.vertexCount];
		originalverts = new Vector3[mesh.vertexCount];
		vertDamper = new Vector3SmoothDamper[mesh.vertexCount];
		for(int i = 0; i < mesh.vertexCount; i++){
			verts[i] = mesh.vertices[i];
			originalverts[i] = verts[i];
			vertDamper[i] = new Vector3SmoothDamper(verts[i], verts[i], .5f, true);
		}
		colors32 = new Color32[mesh.vertexCount];
		ChangeSpeed();

	}
	
	// Update is called once per frame
	void LateUpdate () {


		Vector3 touchPos = Vector3.zero;
		bool hasTouched = false;
		if (Input.GetMouseButton(0)){
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 25;
			Vector3 worldpos = Camera.main.ScreenToWorldPoint(mousePos);
//			debugCube.transform.position = worldpos;
			touchPos = worldpos;
			hasTouched = true;
		}


//		if (Input.anyKeyDown){
//			Application.Quit();
//		}

//		if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		if (Input.GetKeyDown(KeyCode.R)) {
			Start ();
		}
//		if (Input.GetKey(KeyCode.RightArrow)){
//			rotationSpeed += 10f * Time.unscaledDeltaTime;
//			resetTimer = 1f;
//		}
//		else if (Input.GetKey (KeyCode.LeftArrow)){
//			rotationSpeed -= 10f * Time.unscaledDeltaTime;
//			resetTimer = 1f;
//		}
//		else if (rotationSpeed != 0){
//			rotationSpeed *= Mathf.Pow(.5f, Time.unscaledDeltaTime);
//			if (Mathf.Abs(rotationSpeed) <.01f) rotationSpeed = 0;
//		}
//		rotationSpeed = Mathf.Clamp(rotationSpeed, -180, 180);
//		Vector3 angles = transform.localRotation.eulerAngles;
//		if (rotationSpeed != 0){
//			angles.y += rotationSpeed * Time.unscaledDeltaTime;
//			transform.localRotation = Quaternion.Euler(angles);
//		}
//		if (resetTimer > 0){
//			resetTimer -= Time.deltaTime;
//			resetVel = Vector3.zero;
//		}
//		if (resetTimer <= 0){
//			angles = Vector3.SmoothDamp(angles, new Vector3(283.8091f, 180f, 180f), ref resetVel,  3f, 10f, Time.unscaledDeltaTime);
//			transform.localRotation = Quaternion.Euler(angles);
//		}

		timer += Mathf.Clamp(Time.deltaTime * speed, .0000001f, 1f);
		
		speed = speedDamper.current;
		Time.timeScale = Mathf.Clamp(timeDamper.current, .001f, 1f);
		smooth = smoothingDamper.current;
		scale = scaleDamper.current;
		
		h1 = HSBColor.FromVector(h1damper.current);
		h2 = HSBColor.FromVector(h2damper.current);
		h3 = HSBColor.FromVector(h3damper.current);
		h4 = HSBColor.FromVector(h4damper.current);
		
		hue1 = h1.ToColor();
		hue2 = h2.ToColor();
		hue3 = h3.ToColor();
		hue4 = h4.ToColor();
		
		float frameskip = verts.Length/2f;
		for(int i = 0; i < verts.Length; i++){
			
			//				Color blendColor = Vector4.Lerp(hue3, hue1, Mathf.Abs(verts[i].x)/10f);

			Vector3 v = originalverts[i];

			if (v.y > 9.5f) v.y = 9.5f;
			float height = SimplexNoise.Noise.Generate(timer, v.x, v.y) * scale + SimplexNoise.Noise.Generate(timer, v.x/15f, v.y/15f) * smooth;
			v.z = height;
			float distanceFromTouch = 1000000f;
			if (hasTouched){
				 distanceFromTouch = Vector3.Distance(v*2f, touchPos);
//				v = Vector3.MoveTowards(v, touchPos, Mathf.Clamp(1/(distanceFromTouch*distanceFromTouch) * Time.deltaTime, 0, 10));
				v = Vector3.MoveTowards(v, touchPos/2f, -21/(distanceFromTouch));
			}
			else{
//				float distanceFromTouch = Vector3.Distance(v, originalVert);
//				v = Vector3.MoveTowards(v, originalVert, Time.deltaTime);
			}
//			HSBColor color = new HSBColor(heightColor);
//			color.s += Vector3.Distance(vertDamper[i].current, vertDamper[i].target);
			vertDamper[i].target = v;
			verts[i] = vertDamper[i].current;

			Color heightColor = Vector4.Lerp(hue1, hue2, verts[i].z/2f);
			heightColor = Vector4.Lerp(heightColor, hue4, (verts[i].x+5)/10);
			colors32[i] = heightColor;
		}
		
		mesh.vertices = verts;
		mesh.colors32 = colors32;
		mesh.RecalculateNormals();
		if (Mathf.Abs(timer) > 2000f){
			timer = Random.Range(0f, 100f);
			scalar1 = Random.Range(0.001f, 0.01f);
			scalar2 = Random.Range(0.001f, 0.01f);
		}
		
		changeTimer -= Time.unscaledDeltaTime;
		if (changeTimer <= 0){
			ChangeSpeed();
		}
		
		h1changetimer -= Time.unscaledDeltaTime;
		if (h1changetimer <= 0){
			RandomizeColors();
		}
		
		maincamera.backgroundColor = hue3;
		RenderSettings.fogColor = hue3;
		//RenderSettings.fogDensity = .05f;

		hasTouched = false;
	}

	void ChangeSpeed(){
		speedDamper.target = Random.Range(.01f, 1f);
//		speedDamper.target = 5f;
		smoothingDamper.target = Random.Range(0f, 8f);
		scaleDamper.target = Random.Range(-7f, 7f);
		timeDamper.target = Random.Range(.1f, .3f);

		changeTimer = Mathf.Clamp(Random.Range(1f, 10f)  * devSpeed / speedDamper.target, 1f, 120f);
	}

	void RandomizeColors(){
		h1.h = Random.Range(0f, 360f)/360f;
		h2.h = Random.value > .1 ? Random.Range(0f, 360f)/360f : (h1.h + 360/1.618f) % 1;
		h3.h = Random.Range(0f, 360f)/360f;
//		h4.h = (h1.h + Random.value > .5 ? (360/45) : -(360/45) )%1;
		h4.h = (h1.h + Random.Range(-.2f, .2f))%1;

		h1.s = Random.Range(0f, .8f);
		h2.s = h1.s/Random.Range(1f, 3f);

		h1.b = Random.Range(.5f, 1f);
		h2.b = h1.b + Random.Range(0f, .1f);

		if (Random.value < .05f){
			h3.b = Random.value < .5f ? Random.Range(0f, .1f) : Random.Range(.9f, 1f);
			h3.s = h3.b < .5f ? Random.Range(0f, .2f) : Random.Range(0f, .1f);
		}
		else if (Random.value < .2f){
			h3.b = h1.b + Random.Range(-.2f, .2f);
			h3.s = h1.s - .1f;
			h3.h = (h1.h - h2.h)%1;
		}
		else if (Random.value < .5f){
			h3.b = Random.Range(0f, .1f);
		}

		h4.s = h1.s;
		h4.b = h1.b;

		h1damper.target = h1;
		h2damper.target = h2;
		h3damper.target = h3;

		h1changetimer = Mathf.Clamp(Random.Range(1f, 30f), 1f, 30f);
	}


}
