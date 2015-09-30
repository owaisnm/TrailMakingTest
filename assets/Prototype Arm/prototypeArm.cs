using UnityEngine;
using System.Collections;

public class prototypeArm : MonoBehaviour {

	public Transform armL, armR;

	void Start ()
	{

	}

	void Update ()
	{
		//armL.position = KinectJoints.handLpos2D;
		//armL.rotation = Quaternion.Euler(0f, 90f, 1*KinectJoints.armLrot2D);

		//armR.position = KinectJoints.handRpos2D;
		//armR.localRotation = Quaternion.Euler(KinectJoints.armRrot2D, 90f, 270);
		//Debug.Log ("model: " + KinectJoints.handRpos2D.x + "," + KinectJoints.handRpos2D.y);

		armL.position = KinectJoints.handLpos3D;
		armR.position = KinectJoints.handRpos3D;
	}
}
