using UnityEngine;
using System.Collections;
using Ovr;

public class TestOVRManager : MonoBehaviour {

	public ParticleSystemRenderer stars1;
	public ParticleSystemRenderer stars2;
	public ParticleSystemRenderer stars3;
	public ParticleSystemRenderer stars4;
	public ParticleSystemRenderer smoke1;
	public ParticleSystemRenderer smoke2;
	public ParticleSystemRenderer smoke3;

	public GameObject mysphere;

	public float accelX;
	public float accelY;
	public float accelZ;
	public float accelZ2;
	public float velocityX;
	public float velocityY;
	public float velocityZ;

	public float totalaccel;

	public float gyroX;
	public float gyroY;
	public float gyroZ;

	public float magnetometerX;
	public float magnetometerY;
	public float magnetometerZ;

	public float thistime;
	public float lasttime;
	public float finaltime;

	public float linvelX;
	public float linvelY;
	public float linvelZ;

	public float linaccelX;
	public float linaccelX2;
	public float linaccelY;
	public float linaccelY2;
	public float linaccelZ;
	public float linaccelZ2;

	public float angvelX;
	public float angvelY;
	public float angvelZ;

	public float angaccelX;
	public float angaccelY;
	public float angaccelZ;

	public float pitch;
	public float yaw;
	public float roll;

	public float objectposZ;


	// Use this for initialization
	void Start () {
		velocityX = 0;
		velocityY = 0;
		velocityZ = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Grab IMU data
		magnetometerX=OVRManager.capiHmd.GetTrackingState().RawSensorData.Magnetometer.x;
		magnetometerY=OVRManager.capiHmd.GetTrackingState().RawSensorData.Magnetometer.y;
		magnetometerZ=OVRManager.capiHmd.GetTrackingState().RawSensorData.Magnetometer.z;
		gyroX = OVRManager.capiHmd.GetTrackingState().RawSensorData.Gyro.x;
		gyroY = OVRManager.capiHmd.GetTrackingState().RawSensorData.Gyro.y;
		gyroZ = OVRManager.capiHmd.GetTrackingState().RawSensorData.Gyro.z;
		accelX = OVRManager.capiHmd.GetTrackingState().RawSensorData.Accelerometer.x;
		accelY = OVRManager.capiHmd.GetTrackingState().RawSensorData.Accelerometer.y;
		accelZ = OVRManager.capiHmd.GetTrackingState().RawSensorData.Accelerometer.z;
		//thistime = OVRManager.capiHmd.GetTrackingState ().RawSensorData.TimeInSeconds;
		//finaltime = thistime - lasttime;
		//velocityZ = accelZ + (Mathf.Abs (accelZ - accelZ2) / 2) * finaltime + (accelY-9.81f);
		//Debug.Log (velocityZ);

		//Does not work without tracking camera?
		linvelX= OVRManager.capiHmd.GetTrackingState().HeadPose.LinearVelocity.x;
		linvelY= OVRManager.capiHmd.GetTrackingState().HeadPose.LinearVelocity.y;
		linvelZ= OVRManager.capiHmd.GetTrackingState().HeadPose.LinearVelocity.z;

		angvelX= OVRManager.capiHmd.GetTrackingState().HeadPose.AngularVelocity.x;
		angvelY= OVRManager.capiHmd.GetTrackingState().HeadPose.AngularVelocity.y;
		angvelZ= OVRManager.capiHmd.GetTrackingState().HeadPose.AngularVelocity.z;
		
		angaccelX= OVRManager.capiHmd.GetTrackingState().HeadPose.AngularAcceleration.x;
		angaccelY= OVRManager.capiHmd.GetTrackingState().HeadPose.AngularAcceleration.y;
		angaccelZ= OVRManager.capiHmd.GetTrackingState().HeadPose.AngularAcceleration.z;

		linaccelX= OVRManager.capiHmd.GetTrackingState().HeadPose.LinearAcceleration.x;
		linaccelY= OVRManager.capiHmd.GetTrackingState().HeadPose.LinearAcceleration.y;
		linaccelZ= OVRManager.capiHmd.GetTrackingState().HeadPose.LinearAcceleration.z;
		totalaccel = Mathf.Sqrt (Mathf.Pow (2, linaccelX) + Mathf.Pow (2, linaccelY) + Mathf.Pow (2, linaccelZ));


		//Debug.Log (totalaccel);

		//Velocity integration
		thistime = OVRManager.capiHmd.GetTrackingState ().RawSensorData.TimeInSeconds;
		finaltime = thistime - lasttime;
		//velocityZ = linaccelZ + velocityZ;
		velocityX = linaccelX + (Mathf.Abs (linaccelX - linaccelX2) / 2) * finaltime;
		velocityY = linaccelY+ (Mathf.Abs (linaccelY - linaccelY2) / 2) * finaltime;
		velocityZ = linaccelZ + (Mathf.Abs (linaccelZ - linaccelZ2) / 2) * finaltime;
	
		//Warp Speed
		stars1.lengthScale=Mathf.Pow(velocityX,2);
		stars1.cameraVelocityScale = Mathf.Pow(velocityX,2);
		stars2.lengthScale = Mathf.Pow(velocityX,2);
		stars2.cameraVelocityScale = Mathf.Pow(velocityX,2);
		stars3.lengthScale=Mathf.Pow(velocityX,2);
		stars3.cameraVelocityScale = Mathf.Pow(velocityX,2);
		stars4.lengthScale = Mathf.Pow(velocityX,2);
		stars4.cameraVelocityScale = Mathf.Pow(velocityX,2);
		/*
		smoke1.lengthScale=Mathf.Pow(velocityX,2);
		smoke1.cameraVelocityScale = Mathf.Pow(velocityX,2);
		smoke2.lengthScale = Mathf.Pow(velocityX,2);
		smoke2.cameraVelocityScale = Mathf.Pow(velocityX,2);
		smoke3.lengthScale = Mathf.Pow(velocityX,2);
		smoke3.cameraVelocityScale = Mathf.Pow(velocityX,2);
*/
		//Pedometer
		if (linaccelY < -1.5) {
			Vector3 position = new Vector3(1f*Mathf.Sin(magnetometerX),0,1f*Mathf.Sin(magnetometerX));
			mysphere.transform.position += position;
			Debug.Log("step");
		}
		float pitch = Mathf.Atan (accelY / Mathf.Sqrt (Mathf.Pow (accelX, 2) + Mathf.Pow (accelZ, 2)));
		float yaw = Mathf.Atan (accelZ / Mathf.Sqrt (Mathf.Pow (accelX, 2) + Mathf.Pow (accelY, 2)));
		float roll = Mathf.Atan (accelX / Mathf.Sqrt (Mathf.Pow (accelY, 2) + Mathf.Pow (accelZ, 2)));
		//Store Old Data for next Calculatio
		lasttime = thistime;
		accelZ2 = accelZ; //store previous data for integration into velocity
		linaccelX2 = linaccelX;
		linaccelY2 = linaccelY;
		linaccelZ2 = linaccelZ;

	}
}
