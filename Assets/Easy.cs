using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Easy : MonoBehaviour {

	public List<Transform> diskList;
	public GUIStyle textStyle;
	TakenNodes[] takenNodes;
	int currentNodeIndex = 0;		// next node to be selected
	int amount = 25;				// total number of nodes
	int amountToDisplay = 8;		// number of nodes to display on screen
	float distance = 0f;			// how far disks are in front of camera

	void Start () 
	{
		// set random x and y values for the nodes, keeping track of proximity
		RandomNodes nodes = new RandomNodes(amount);
		nodes.RandomCoordinates();
		takenNodes = (TakenNodes[])nodes.GetNodes ();

		// set initial disk rotation and positions
		for (int i = 0; i < amount; i++) 
		{
			diskList[i].localScale = new Vector3(10, 10, 10);
			diskList[i].position = new Vector3(0, 0, 10);
			// diskList[i].position = Camera.main.transform.position + Camera.main.transform.position * distance;
			diskList[i].rotation = new Quaternion();
			Debug.Log ("Disk Position" + i + " \t x: " + diskList[i].position.x + "\t y: " + diskList[i].position.y + "\t z: " + diskList[i].position.z);
			Debug.Log ("Disk Rotation" + i + " \t x: " + diskList[i].rotation.x + "\t y: " + diskList[i].rotation.y + "\t z: " + diskList[i].rotation.z);

		}
		
		// set disk positions
		for (int i = 0; i < amount; i++) 
		{
			// diskList[i].position = new Vector3(takenNodes[i].pixel_x, takenNodes[i].pixel_y, 0);
		}

		/*
		for (int i = 0; i < amount; i++) 
		{
			Debug.Log("Node " + i + " \tx: " + takenNodes[i].pixel_x + " \ty: "+ takenNodes[i].pixel_y);
		}
		*/
	}
	
	void Update () 
	{
		Debug.Log ("mouseX = " + Input.mousePosition.x);
		Debug.Log ("mouseY = " + Input.mousePosition.y);	
		if (isMouseOverCurrentNode (Input.mousePosition.x, Input.mousePosition.y)) 
		{
			// *** perform animation
			// *** make currentNode disappear
			currentNodeIndex++;
		}
	}

	void OnGUI()
	{
		// if not at last 8
		if (currentNodeIndex + amountToDisplay < amount) 
		{
			for (int i = currentNodeIndex; i < currentNodeIndex + 8; i++) 
			{
				GUI.Label(new Rect(takenNodes[i].pixel_x, takenNodes[i].pixel_y, 10, 10), (i+1).ToString(), textStyle);
			}		
		}

		// if at last 8
		else 
		{
			for (int i = currentNodeIndex; i < amount; i++) 
			{
				GUI.Label(new Rect(takenNodes[i].pixel_x, takenNodes[i].pixel_y, 10, 10), (i+1).ToString(), textStyle);
			}	
		}
	}

	bool isMouseOverCurrentNode(float mouseX, float mouseY)
	{
		int nodeWidth = RandomNodes.node_W;
		int nodeHeight = RandomNodes.node_H;

		// assuming (takenNodes[currentNodeIndex].pixel_x, takenNodes[currentNodeIndex].pixel_y) 
		// is topleftmost coordinate of tokenNode[currentNodeIndex]
		if(takenNodes[currentNodeIndex].pixel_x <= mouseX && mouseX <= (takenNodes[currentNodeIndex].pixel_x + nodeWidth) && 
		   takenNodes[currentNodeIndex].pixel_y <= (Screen.height - mouseY) && (Screen.height - mouseY) <= (takenNodes[currentNodeIndex].pixel_y + nodeHeight))
		{
			return true;
		}
		return false;
	}
}
																																											