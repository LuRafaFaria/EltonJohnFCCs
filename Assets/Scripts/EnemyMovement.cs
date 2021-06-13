using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex = 0;
    private int wavePointOddIndex = 1;
    private Enemy enemy;
    public Animator animator;
    private int levelReached;
    private WaveSpawner wSpawner;


    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
        animator = GetComponent<Animator>();
        animator.SetFloat("speed", 1f);
        levelReached = PlayerPrefs.GetInt("levelReached", 1);
    }

    void Update()
    {
        
        Debug.Log($"level: {levelReached}");

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            if (levelReached == 3)
            {
                GetNextWayPointLevel3();
            }
            else
            {
                GetNextWayPoint();
            }
        }

        enemy.speed = enemy.startSpeed;

        if (wSpawner.count % 2 == 0)
        {
            Debug.Log("even");
            enemy.even = true;
        }
        else
        {
            enemy.even = false;
        }


      
    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
    }

    void GetNextWayPointLevel3()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        else if (wavePointOddIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        if (enemy.even)
        {
            wavePointIndex += 2;
            target = Waypoints.points[wavePointIndex];
        }
        else
        {
            wavePointOddIndex += 2;
            target = Waypoints.points[wavePointOddIndex];
        }


    }

    void EndPath()
    {
        PlayerStats.lives--;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
