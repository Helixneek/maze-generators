using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Maze generator using Recursive Backtracking Algorithm
class MazeGenerator {
    // Maze variables
    private int width;
    private int height;
    private int[,] maze;
    private Random random;

    public MazeGenerator(int width, int height) { // Constructor
        this.width = width;
        this.height = height;
        this.maze = new int[width, height];
        this.random = new Random();
    }


    public void GenerateMaze()
    { // Generate initial maze
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; // Initialize all cells as walls
            }
        }

        Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>(); // Stack to keep track of cells
        Tuple<int, int> currentCell = new Tuple<int, int>(1, 1); // Start from cell (1, 1)
        stack.Push(currentCell);
        maze[currentCell.Item1, currentCell.Item2] = 0; // Mark first cell as visited / path

        while(stack.Count > 0)
        {
            List<Tuple<int, int>> neighbors = GetUnvisitedNeighbors(currentCell); // Get all unvisited neighbors of current cell

            if(neighbors.Count > 0)
            {
                stack.Push(currentCell);

                int randomIndex = random.Next(neighbors.Count);
                Tuple<int, int> neighbor = neighbors[randomIndex]; // Get random unvisited neighbor

                int nx = neighbor.Item1;
                int ny = neighbor.Item2;
                maze[nx, ny] = 0; // Set random unvisited neighbor as visited / path

                int mx = (currentCell.Item1 + nx) / 2;
                int my = (currentCell.Item2 + ny) / 2;
                maze[mx, my] = 0; // Set midpoint between current cell and neighbor as visited
                // This is so that there is a path between these two cells

                currentCell = neighbor; // Continue building paths from the neighbor used
            } else
            {
                // If there is no unvisited neighbor found,
                // Remove current cell from stack and move on to the next
                currentCell = stack.Pop(); 
            }
        }
    }

    public void PrintMaze()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                // Print unvisited cells as walls and visited cells as empty paths
                Console.Write(maze[x, y] == 1 ? "|" : " ");
            }
            Console.WriteLine();
        }
    }

    private List<Tuple<int, int>> GetUnvisitedNeighbors(Tuple<int, int> cell)
    {
        int x = cell.Item1;
        int y = cell.Item2;
        List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>(); // Prepare list for cells

        int[][] moves = { new int[] { 2, 0 }, new int[] { -2, 0 }, new int[] { 0, 2 }, new int[] { 0, -2 } };

        foreach(int[] move in moves)
        {
            int nx = x + move[0];
            int ny = y + move[1];

            // Check if the neighbor cell is above (0,0) and is still inside the bounding box
            if (nx > 0 && ny > 0 && nx < width && ny < height && maze[nx, ny] == 1)
            {
                neighbors.Add(new Tuple<int, int>(nx, ny));
            }
        }

        return neighbors;
    }

    public static void Main(string[] args)
    {
        int width = 21;
        int height = 21;

        MazeGenerator generator = new MazeGenerator(width, height);
        generator.GenerateMaze();
        generator.PrintMaze();

        Console.WriteLine("Maze has been generated");
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}