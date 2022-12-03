using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject RoadPrefab;
    public float maxSpeed = 10.0f;
    public int maxRoadCount = 5;
    
    private float speed = 0.0f;
    private List<GameObject> roads = new List<GameObject>();

    void Start()
    {
        ResetLevel();
        StartLevel();
    }
    
    void Update()
    {
        if(speed == 0) return;

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            
        }
//TODO: говнокод, отрефакторить дубляж
        if (roads[0].transform.position.z < -10)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            
            CreateNextRoad();
        }
    }

    private void StartLevel()
    {
        speed = maxSpeed;
        
    }

    private void CreateNextRoad()
    {
        Vector3 startPosition = Vector3.zero;
        if (roads.Count > 0)
        {
            // TODO: edit road size
            startPosition = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 10);
        }
        GameObject nextRoad = Instantiate(RoadPrefab, startPosition, Quaternion.identity);
        nextRoad.transform.SetParent(transform);
        roads.Add(nextRoad);
    }
    
    public void ResetLevel()
    {
        speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }

        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }
}
