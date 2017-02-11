using UnityEngine;
using System.Collections;

public class RotateBody : MonoBehaviour {
	public float degreesPerSecond = 5f;
	public float totalRotation = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float currentAngle = transform.rotation.eulerAngles.y;
		transform.rotation = 
			Quaternion.AngleAxis(currentAngle + (Time.deltaTime * degreesPerSecond), Vector3.right);
		totalRotation += Time.deltaTime * degreesPerSecond;
	}
}
