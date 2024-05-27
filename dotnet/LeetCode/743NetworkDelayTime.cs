using System.Diagnostics;


namespace LeetCode;

/* TODO: incomplete */
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var example1 = NetworkDelayTime([[2, 1, 1], [2, 3, 1], [3, 4, 1]], 4, 2);
        var example2 = NetworkDelayTime([[1, 2, 1]], 2, 1);
        var example3 = NetworkDelayTime([[1, 2, 1]], 2, 2);
        var example4 = NetworkDelayTime(
        [
                [4, 2, 76], [1, 3, 79], [3, 1, 81], [4, 3, 30], [2, 1, 47], [1, 5, 61], [1, 4, 99], [3, 4, 68],
                [3, 5, 46], [4, 1, 6], [5, 4, 7], [5, 3, 44], [4, 5, 19], [2, 3, 13], [3, 2, 18], [1, 2, 0], [5, 1, 25],
                [2, 5, 58], [2, 4, 77], [5, 2, 74]
        ], 5, 3);

        Assert.AreEqual(example1,  2);
        Assert.AreEqual(example2,  1);
        Assert.AreEqual(example3,  -1);
        Assert.AreEqual(example4,  59);
    }

    public int NetworkDelayTime(int[][] times,
                                int n,
                                int k)
    {
        var paths = Enumerable.Range(1, n).ToDictionary(x => x, x => new List<(int target, int weight)>());
        foreach (var time in times)
        {
            var source = time[0];
            var target = time[1];
            var weight = time[2];
            paths[source].Add((target, weight));
        }

        var shortestDistance = Enumerable.Range(1, n).ToDictionary(x => x, x => int.MaxValue);
        shortestDistance[k] = 0;

        var visited = new HashSet<int>();

        var pq = new SortedSet<(int dist, int node)>();
        pq.Add((0, k));

        while (pq.Any())
        {
            var (dist, c) = pq.First();
            pq.Remove(pq.First());
            if (visited.Contains(c)) continue;
            visited.Add(c);

            foreach (var valueTuple in paths[c])
            {
                if (visited.Contains(valueTuple.target)) continue;
                var nextDist = dist + valueTuple.weight;
                if (nextDist < shortestDistance[valueTuple.target]) shortestDistance[valueTuple.target] = nextDist;
                shortestDistance[valueTuple.target] = nextDist;
                pq.Add((nextDist, valueTuple.target));
            }
        }

        return shortestDistance.Values.Max() == int.MaxValue ? -1 : shortestDistance.Values.Max();
    }
}

/*
 * original implementation:
 *     public int NetworkDelayTime(int[][] times,
                               int n,
                               int k)
   {
       var paths = Enumerable.Range(1, n).ToDictionary(x => x, x => new List<(int target, int weight)>());
       foreach (var time in times)
       {
           var source = time[0];
           var target = time[1];
           var weight = time[2];
           paths[source].Add((target, weight));
       }

       var shortestDistance = Enumerable.Range(1, n).ToDictionary(x => x, x => int.MaxValue);
       shortestDistance[k] = 0;

       var visited = Enumerable.Range(1, n).ToDictionary(x => x, x => false);
       var q = new Queue<int>();
       q.Enqueue(k);
       while (q.Any())
       {
           var c = q.Dequeue();
           if (visited[c]) continue;
           var dist = shortestDistance[c];
           visited[c] = true;
           foreach (var valueTuple in paths[c])
           {
               q.Enqueue(valueTuple.target);
               var nextDist = dist + valueTuple.weight;
               if (nextDist < shortestDistance[valueTuple.target]) shortestDistance[valueTuple.target] = nextDist;
           }
       }

       if (visited.Count(x => x.Value) != n) return -1;

       return shortestDistance.Values.Max();
   }

   the problem with this is that it uses BFS instead of dijkstra
   BFS doesn't find the shortest path - BFS looks at the shortest path by the # of edges, not the weight
   was using a dictionary here in a way very similar to a hashset - couldve just used a hashset, but instead need to use a priority queue any way

*/