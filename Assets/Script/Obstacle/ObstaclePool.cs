using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    public List<Obstacle> Obstacles = new List<Obstacle>();
    public static ObstaclePool Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddObstacle(Obstacle obstacle)
    {
        Obstacles.Add(obstacle);
    }

    public Obstacle GetAnObstacle()
    {
        Obstacle obstacle = null;
        if(Obstacles.Count != 0)
        {
            obstacle = Obstacles[0];
            Obstacles.Remove(obstacle);
        }
        return obstacle;
    }
}
