using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TakenNodes {

	public int[] col;  // x region
	public int[] row;  // y region
	public int pixel_x;
	public int pixel_y;
	
	public TakenNodes()
	{
		int bound = RandomNodes.inner_bound;
		int w = RandomNodes.node_W;
		int h = RandomNodes.node_H;
		int neglect_region = ((2 * bound) + w) * ((2 * bound) + h);
		
		this.col = new int[neglect_region];
		this.row = new int[neglect_region];
		this.pixel_x = 0;
		this.pixel_y = 0;
	}
}
