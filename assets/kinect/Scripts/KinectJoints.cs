using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class KinectJoints : MonoBehaviour {

	public GameObject BodySourceManager;
	private BodySourceManager _BodyManager;

	public static string jointsXY;

	public static Vector3 handLpos2D, handRpos2D, handLpos3D, handRpos3D;
	public static float armLrot2D, armRrot2D, armLrot3D, armRrot3D;

	void Start ()
	{
	}

	void Update ()
	{
		if (BodySourceManager == null)
		{
			Debug.Log("Forgot to attach BodySourceManager to this script/object in Inspector");
			return;
		}
		
		_BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
		if (_BodyManager == null)
		{
			Debug.Log("Forgot to attach BodySourceManager script to BodyManager object");
			return;
		}

		Body[] data = _BodyManager.GetData();
		if (data == null)
		{
			jointsXY = "NONE";
			HandleKinectInput(jointsXY);
			return;
		}

		HandleKinectInput(jointsXY);
	}

	private void HandleKinectInput(string input)
	{
		// If joints found
		if (input != "NONE")
		{
			string[] values = input.Split(',');
			// If proper joint specs were found
			if (values.Length % 4 == 0)
			{
				//Debug.Log("Good Joint Data Found and Used!");
				int count = values.Length / 4;
				int[] node = new int[count];
				float[] x = new float[count];
				float[] y = new float[count];
				float[] z = new float[count];
				
				for(int i = 0; i < values.Length; i+=4)
					node[i/4] = int.Parse(values[i]);
				
				for(int i = 1; i < values.Length; i+=4)
					x[(i-1)/4] = float.Parse(values[i]);
				
				for(int i = 2; i < values.Length; i+=4)
					y[(i-2)/4] = float.Parse(values[i]);	// flip y for VS to Unity compliance
				
				for(int i = 3; i < values.Length; i+=4)
					z[(i-3)/4] = float.Parse(values[i]);

				// set hand positions for arm placement
				handLpos2D = new Vector3(x[7], y[7], 0f);
				handRpos2D = new Vector3(x[11], y[11], 0f);
				handLpos3D = new Vector3(x[7], y[7], z[7]);
				handRpos3D = new Vector3(x[11], y[11], z[11]);
				// find angles between elbow and hand joints for rotation
				// 2D rotation = xy-plane, depth rotation = yz-plane
				//armLrot2D = Vector3.Angle(new Vector3(x[5],y[5],0f), handLpos2D);
				//armRrot2D = Vector3.Angle(handRpos2D, new Vector3(x[9],y[9],0f));
				/*armLrot2D = GetAngle(new Vector2(x[11],y[11]), new Vector2(x[9],y[9]));
				armRrot2D = GetAngle(new Vector2(x[9],y[9]), new Vector2(x[11],y[11]));
				armLrot3D = Vector3.Angle(new Vector3(x[5],y[5],z[5]), handLpos3D);
				armRrot3D = Vector3.Angle(new Vector3(x[9],y[9],z[5]), handRpos3D);*/

				//Debug.Log(armRrot2D);
			}
			else
			{
				//Debug.Log("Improper data");
			}
		}
		else
		{
			//Debug.Log("No Joints");
		}
	}

	float GetAngle (Vector2 a, Vector2 b)
	{
		float deltaY = b.y - a.y;
		float deltaX = b.x - a.x;
		float angle = Mathf.Atan(deltaY / deltaX) * 180f / Mathf.PI;
		return angle;
	}
}
