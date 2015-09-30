using UnityEngine;
using System.Collections;

public class KinectHands : MonoBehaviour {

	public Transform handL, handR;

	public static bool isRight = true;
	public static bool is3D = false;
	
	void Start ()
	{
		// if right hand enabled, left sent off screen
		if (isRight)
		{
			handL.position = new Vector3(1000f,1000f,1000f);
		}
		// else left hand enabled, right sent off screen
		else
		{
			handR.position = new Vector3(1000f,1000f,1000f);
		}
	}
	
	void Update ()
	{
		// if in 2D
		if (!is3D)
		{
			if (isRight)
			{
				handR.position = KinectJoints.handRpos2D;
			}
			else
			{
				handL.position = KinectJoints.handLpos2D;
			}
		}

		// else using depth
		else
		{
			if (isRight)
			{
				handR.position = KinectJoints.handRpos3D;
			}
			else
			{
				handL.position = KinectJoints.handLpos3D;
			}
		}
	}
}
