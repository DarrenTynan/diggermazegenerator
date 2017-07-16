using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour
{
	public int mapHeight = 11;
	public int mapWidth = 11;
	private int[,] mazeArray;
	public GameObject wallPrefab;

	// New random instance.
	static System.Random random = new System.Random();

	// Use this for initialization
	void Start ()
	{
		// Generate maze and store in pointer.
		mazeArray = GenerateMaze(mapHeight, mapWidth);

		// Show visual representation.
		ShowMaze();
	}

	/// <summary>
	///	Loop through maze array and instantiate wall prefab if set.
	///	Also set the parent to the MazeHolder to be neat.
	/// </summary>
	private void ShowMaze()
	{
		// Create empty go to hold the wall prefabs;
		GameObject mazeHolder = new GameObject();
		mazeHolder.name = "MazeHolder";

		for(int h = 0; h < mapHeight; h++)
		{
			for(int w = 0; w < mapWidth; w++)
			{
				// Is it a wall?
				if(mazeArray[h, w] == 1)
				{
					// Create a position vector.
					Vector3 position = new Vector3(h, 0, w);

					// Instantiate wall block.
					GameObject go = Instantiate(wallPrefab) as GameObject;

					// Move to correct position.
					if(go != null)
					{
						go.transform.position = position;
						// Set the parent to keep the heirachy simple.
						go.transform.SetParent(mazeHolder.transform);
					}
				}
			}
		}
	}

	/// <summary>
	/// Create a temporary maze array and fill with walls.
	///	Select random starting position.
	///	Call to dig out maze.
	/// </summary>
	/// <returns>The maze.</returns>
	/// <param name="height">Height.</param>
	/// <param name="width">Width.</param>
	private int[,] GenerateMaze(int height, int width)
	{
		// Create temporary maze array.
		int[,] tempMaze = new int[height, width];

		// Init all the walls to value 1.
		for(int h = 0; h <height; h++)
		{
			for(int w = 0; w < width; w++)
			{
				tempMaze[h, w] = 1;
			}
		}

		// Generate a new random seed.
		System.Random rand = new System.Random();

		// Select random start cell.
		int r = rand.Next(height);
		while(r % 2 == 0)
			r = rand.Next(height);

		int c = rand.Next(width);
		while(c % 2 == 0)
			c = rand.Next(width);

		// Set starting cell to non wall.
		tempMaze[r, c] = 0;

		// Create maze using dfs.
		MazeDigger(tempMaze, r, c);

		// Return maze.
		return tempMaze;
	}

	/// <summary>
	/// Digs out the maze recursively.
	/// </summary>
	/// <param name="maze">Maze array.</param>
	/// <param name="r">Row.</param>
	/// <param name="c">Column.</param>
	private void MazeDigger(int[,] maze, int r, int c)
	{
		// Create digging directions
		// 1 = north
		// 2 = east
		// 3 = south
		// 4 = west

		int[] directions = new int[] {1, 2, 3, 4};
		Shuffle(directions);

		// Look in direction 2 blocks ahead.
		for(int i = 0; i < directions.Length; i++)
		{
			switch(directions[i])
			{
				case 1: // north
					// check wether 2 cells is out of range.
					if(r - 2 <= 0)
						continue;
					if(maze[r - 2, c] != 0)
					{
						maze[r - 2, c] = 0;
						maze[r - 1, c] = 0;
						MazeDigger(maze, r - 2, c);
					}
					break;

				case 3: // east
					// check wether 2 cells is out of range.
					if(c + 2 >= mapWidth - 1)
						continue;
					if(maze[r, c + 2] != 0)
					{
						maze[r, c + 2] = 0;
						maze[r, c + 1] = 0;
						MazeDigger(maze, r, c + 2);
					}
					break;

				case 2: // south
					// check wether 2 cells is out of range.
					if(r + 2 >= mapHeight - 1)
						continue;
					if(maze[r + 2, c] != 0)
					{
						maze[r + 2, c] = 0;
						maze[r + 1, c] = 0;
						MazeDigger(maze, r + 2, c);
					}
					break;


				case 4: // west
					// check wether 2 cells is out of range.
					if(c - 2 <= 0)
						continue;
					if(maze[r, c - 2] != 0)
					{
						maze[r, c - 2] = 0;
						maze[r, c - 1] = 0;
						MazeDigger(maze, r, c - 2);
					}
					break;
			}
		}
	}

	/// <summary>
	/// Shuffle the array.
	/// </summary>
	/// <typeparam name="T">Array element type.</typeparam>
	/// <param name="array">Array to shuffle.</param>
	static void Shuffle<T>(T[] array)
	{
		int n = array.Length;
		for (int i = 0; i < n; i++)
		{
			// NextDouble returns a random number between 0 and 1.
			// ... It is equivalent to Math.random() in Java.
			int r = i + (int)(random.NextDouble() * (n - i));
			T t = array[r];
			array[r] = array[i];
			array[i] = t;
		}
	}
}
