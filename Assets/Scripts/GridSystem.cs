using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Karan 
 * 
 * Takes a 4 corners, scales, breaks tiles based on rotation and row,col count
 */

public class GridSystem : MonoBehaviour
{
    public GameObject[] corners;
    public int[] size;

    private float length, width;
    private static int l, w;

    public float[,] grid;
    void Start()
    {
        length = distancetoCorner(corners[0], corners[1]);
        width = distancetoCorner(corners[0], corners[2]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float distancetoCorner(GameObject a, GameObject b)
    {
        float deltaX = Mathf.Abs(a.transform.position.x - b.transform.position.x);
        float deltaY = Mathf.Abs(a.transform.position.y - b.transform.position.y);
        return Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2));
    }

    float splitDistance(float x, int count)
    {
        return x / count;
    }

    void returnPositions()
    {
        float columnInterval = splitDistance(length, size[0]);
        float rowInterval = 0f;
        for (int i = 0; i < size[1]; i++) //for each row
        {
            float yPos = corners[0].transform.position.y + rowInterval;
            for (int j = 0; j<size[0]; j++)// for each column 
            {
                float xPos = corners[0].transform.position.x + columnInterval;

            }
            
        }

    }

    void addToGrid()
    {

    }

}
