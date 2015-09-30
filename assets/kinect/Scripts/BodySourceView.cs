using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{
	public GameObject BodySourceManager;

	private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
	private BodySourceManager _BodyManager;
	
	// added by gestherapy
	void Start ()
	{
	}
	
	void Update () 
	{
		if (BodySourceManager == null)
		{
			return;
		}
		
		_BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
		if (_BodyManager == null)
		{
			return;
		}
		
		Kinect.Body[] data = _BodyManager.GetData();
		if (data == null)
		{
			return;
		}
		
		List<ulong> trackedIds = new List<ulong>();
		foreach(var body in data)
		{
			if (body == null)
			{
				continue;
			}
			
			if(body.IsTracked)
			{
				trackedIds.Add (body.TrackingId);
			}
		}
		
		List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
		
		// First delete untracked bodies
		foreach(ulong trackingId in knownIds)
		{
			if(!trackedIds.Contains(trackingId))
			{
				Destroy(_Bodies[trackingId]);
				_Bodies.Remove(trackingId);
			}
		}
		
		foreach(var body in data)
		{
			if (body == null)
			{
				continue;
			}
			
			if(body.IsTracked)
			{
				if(!_Bodies.ContainsKey(body.TrackingId))
				{
					_Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
				}
				
				RefreshBodyObject(body, _Bodies[body.TrackingId]);
			}
		}
	}
	
	private GameObject CreateBodyObject(ulong id)
	{
		GameObject body = new GameObject("Body:" + id);
		
		for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
		{
			GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			
			jointObj.transform.localScale = new Vector3(0f, 0f, 0f);
			jointObj.name = jt.ToString();
			jointObj.transform.parent = body.transform;
		}
		
		return body;
	}
	
	// string use added by GesTherapy
	private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
	{
		string bodyOut = "";
		for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
		{
			Kinect.Joint sourceJoint = body.Joints[jt];
			
			Transform jointObj = bodyObject.transform.FindChild(jt.ToString());
			jointObj.position = GetVector3FromJoint(sourceJoint);
			
			//added
			if (jt == Kinect.JointType.ThumbRight)
			{
				bodyOut += ((int)jt).ToString() + "," + jointObj.position.x.ToString() + "," +
					jointObj.position.y.ToString() + "," + jointObj.position.z.ToString();
			}
			else
			{
				bodyOut += ((int)jt).ToString() + "," + jointObj.position.x.ToString() + "," +
					jointObj.position.y.ToString() + "," + jointObj.position.z.ToString() + ",";
			}

			/*if(jt == Kinect.JointType.ElbowRight)
				Debug.Log("skeleton: " + jointObj.position.x + "," + jointObj.position.y);*/
		}
		KinectJoints.jointsXY = bodyOut;
	}
	
	public static Vector3 GetVector3FromJoint(Kinect.Joint joint)
	{
		return new Vector3(joint.Position.X * 7f, (joint.Position.Y * 7f) - 1.0f, joint.Position.Z * 1f);
	}
}
