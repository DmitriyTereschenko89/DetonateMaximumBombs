namespace DetonateMaximumBombs
{
	internal class Program
	{
		public class DetonateMaximumBombs
		{
			private Dictionary<int, List<int>> CreateGraph(int[][] bombs)
			{
				Dictionary<int, List<int>> graph = new();
				for (int i = 0; i < bombs.Length; ++i)
				{
					for (int j = i + 1; j < bombs.Length; ++j)
					{
						int x1 = bombs[i][0];
						int y1 = bombs[i][1];
						int radius1 = bombs[i][2];
						int x2 = bombs[j][0];
						int y2 = bombs[j][1];
						int radius2 = bombs[j][2];
						double distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
						if (distance <= radius1)
						{
							if(!graph.ContainsKey(i))
							{
								graph.Add(i, new List<int>());
							}
							graph[i].Add(j);
						}
						if (distance <= radius2)
						{
							if (!graph.ContainsKey(j))
							{
								graph.Add(j, new List<int>());
							}
							graph[j].Add(i);
						}
					}
				}
				return graph;
			} 

			private int BFS(Dictionary<int, List<int>> graph, int node)
			{
				HashSet<int> visited = new();
				Queue<int> queue = new();
				queue.Enqueue(node);
				while(queue.Count > 0)
				{
					int edge = queue.Dequeue();
					if (!visited.Add(edge))
					{
						continue;
					}
					if (graph.ContainsKey(edge))
					{
						foreach (int adj in graph[edge])
						{
							queue.Enqueue(adj);
						}
					}
				}
				return visited.Count;
			}

			public int MaximumDetonation(int[][] bombs)
			{
				int maxDetonation = 0;
				int n = bombs.Length;
				Dictionary<int, List<int>> graph = CreateGraph(bombs);
				for (int node = 0; node < n; ++node)
				{
					maxDetonation = Math.Max(maxDetonation, BFS(graph, node));
				}
				return maxDetonation;
			}
		}

		static void Main(string[] args)
		{
			DetonateMaximumBombs detonateMaximumBombs = new();
            Console.WriteLine(detonateMaximumBombs.MaximumDetonation(new int[][]
			{
				new int[] { 2, 1, 3 }, new int[] { 6, 1, 4 }
			}));
			Console.WriteLine(detonateMaximumBombs.MaximumDetonation(new int[][]
			{
				new int[] { 1, 2, 3 }, new int[] { 2, 3, 1 },
				new int[] { 3, 4, 2 }, new int[] { 4, 5, 3 }, new int[] { 5, 6, 4 }
			}));
            Console.WriteLine(detonateMaximumBombs.MaximumDetonation(new int[][]
			{
				new int[] { 4, 4, 3 }, new int[] { 4, 4, 3 }
			}));
        }
	}
}