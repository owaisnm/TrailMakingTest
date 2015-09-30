using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RandomNodes
{
	int win_W = Screen.width, win_H = Screen.height;
	public const int node_W = 50, node_H = 50;
	public const int inner_bound = 50, outer_bound = 200;
	static int num_nodes;
	
	public static TakenNodes[] taken;
	
	public RandomNodes(int amount)
	{
		num_nodes = amount;
		taken = new TakenNodes[num_nodes];
		for (int i = 0; i < num_nodes; i++)
		{
			taken[i] = new TakenNodes();
		}
	}
	
	public void RandomCoordinates()
	{
		int x, y;
		// Random random = new Random();
		x = Random.Range(inner_bound, win_W - inner_bound);
		y = Random.Range(inner_bound, win_H - inner_bound);
		taken[0].pixel_x = x;
		taken[0].pixel_y = y;
		FillNeglectRegion(0, x, y);
		bool overlap = true, need_spot = true;
		for (int n = 1; n < num_nodes; n++)
		{
			while (need_spot)
			{
				// randomly generate pixel close to previous node
				x = Random.Range(taken[n - 1].pixel_x - outer_bound, taken[n - 1].pixel_x + node_W + outer_bound);
				y = Random.Range(taken[n - 1].pixel_y - outer_bound, taken[n - 1].pixel_y + node_H + outer_bound);
				overlap = CheckOverlap(n, x, y);
				if (!(overlap))
				{
					need_spot = false;
				}
			}
			taken[n].pixel_x = x;
			taken[n].pixel_y = y;
			FillNeglectRegion(n, x, y);
			overlap = true; // reset overlap boolean for next node
			need_spot = true; // reset need_spot for next node
		}
	}
	
	private bool CheckOverlap(int node_index, int col, int row)
	{
		for (int n = node_index - 1; n >= 0; n--)
		{
			int i = 0;
			for (int y = row - inner_bound; y < (row + node_H + inner_bound); y++)
			{
				for (int x = col - inner_bound; x < (col + node_W + inner_bound); x++)
				{
					// check if region still within bounds of window, also assures node is not too far at edge
					if (x < 0 || x > win_W || y < 0 || y > win_H)
					{
						return true;    // OVERLAP!
					}
					// check for overlap of regions
					if ((x == taken[n].col[i]) && (y == taken[n].row[i]))
					{
						return true;    // OVERLAP!
					}
					i++;
				}
			}
		}
		return false;   // no overlap
	}
	
	private void FillNeglectRegion(int node_index, int col, int row)
	{
		int i = 0;
		for (int y = row - inner_bound; y < (row + node_H + inner_bound); y++)
		{
			for (int x = col - inner_bound; x < (col + node_W + inner_bound); x++)
			{
				taken[node_index].col[i] = x;
				taken[node_index].row[i] = y;
				i++;
			}
		}
	}
	
	public TakenNodes[] GetNodes()
	{
		return taken;
	}
	
	public int FindChosenNode(int col, int row)
	{
		for (int n = 0; n < num_nodes; n++)
		{
			int i = 0;
			for (int y = taken[n].pixel_y; y <= (taken[n].pixel_y + node_W); y++)
			{
				for (int x = taken[n].pixel_x; x <= (taken[n].pixel_y + node_H); x++)
				{
					if ((x == col) && (y == row))
					{
						return (n + 1); // return node number, number_index+1
					}
					i++;
				}
			}
		}
		return 0;   // pixel belongs to no node
	}
}
