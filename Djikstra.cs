//Alogrithme de Djikstra tel que vu dans le cour

using UnityEngine;
using System.Collections.Generic;


public static class Dijkstra
{
    private static readonly Vector2[] directions = {
        new Vector2(1, 0),    // right
        new Vector2(1, 1),    // up-right
        new Vector2(1, -1),   // down-right
        new Vector2(0, 1),    // up
        new Vector2(0, -1),   // down
        new Vector2(-1, 1),   // up-left (only used when stuck)
        new Vector2(-1, -1),  // down-left (only used when stuck)
        new Vector2(-1, 0),   // left (only used when stuck)
    };
     
    //cette méthode cherche le chemin à prendre
    public static List<Vector2> FindPath(Vector2 start, Vector2 target, int stuckCounter)
    {
        var openSet = new List<Vector2>();
        var closedSet = new HashSet<Vector2>();
        var cameFrom = new Dictionary<Vector2, Vector2>();
        var gScore = new Dictionary<Vector2, float>();
        var fScore = new Dictionary<Vector2, float>();

        openSet.Add(start);
        gScore[start] = 0;
        fScore[start] = Vector2.Distance(start, target);

        int iterations = 0;
        int maxIterations = 1000;

        while (openSet.Count > 0 && iterations < maxIterations)
        {
            iterations++;
            
            Vector2 current = GetLowestFScore(openSet, fScore);
            if (Vector2.Distance(current, target) < 0.5f)
            {
                return ReconstructPath(cameFrom, current, start);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            // Determine how many directions to use based on stuck counter
            int directionsToUse = stuckCounter > 2 ? directions.Length : 5; // Use all directions if stuck

            for (int i = 0; i < directionsToUse; i++)
            {
                Vector2 direction = directions[i];
                // Add more randomness to vertical movement when stuck
                if (stuckCounter > 0 && (direction.y != 0))
                {
                    direction.y *= Random.Range(1f, 2f);
                }

                Vector2 neighbor = current + direction;

                if (closedSet.Contains(neighbor))
                    continue;

                if (!IsPositionValid(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + Vector2.Distance(current, neighbor);

                // Add penalty for moving left when not stuck
                if (direction.x < 0 && stuckCounter <= 2)
                {
                    tentativeGScore += 10f;
                }

                if (!gScore.ContainsKey(neighbor))
                {
                    gScore[neighbor] = float.MaxValue;
                }

                if (tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Vector2.Distance(neighbor, target);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return new List<Vector2> { 
            start, 
            new Vector2(start.x + 2f, start.y + Random.Range(-1f, 1f)),
            new Vector2(target.x, target.y + Random.Range(-2f, 2f))
        };
    }

    //heuristique d'optimisation pour évaluer chaque position à parcourir
    private static Vector2 GetLowestFScore(List<Vector2> openSet, Dictionary<Vector2, float> fScore)
    {
        float lowestF = float.MaxValue;
        Vector2 lowest = openSet[0];

        foreach (Vector2 pos in openSet)
        {
            if (fScore.ContainsKey(pos) && fScore[pos] < lowestF)
            {
                lowestF = fScore[pos];
                lowest = pos;
            }
        }

        return lowest;
    }
    
    //vérifie si la position est valide
    private static bool IsPositionValid(Vector2 pos)
    {
        // Check if position is within game bounds
        if (pos.x < -10 || pos.x > 30 || pos.y < -10 || pos.y > 10)
            return false;

        // Check for obstacles using raycast
        RaycastHit2D hit = Physics2D.CircleCast(pos, 0.2f, Vector2.zero, 0f);
        return !hit;
    }

    //reconstruit le chemin qui a été pris pour revenir en arrière si le joueur est bloqué
    private static List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current, Vector2 start)
    {
        List<Vector2> path = new List<Vector2>();
        path.Add(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
            if (current == start)
                break;
        }

        path.Reverse();
        return path;
    }
}