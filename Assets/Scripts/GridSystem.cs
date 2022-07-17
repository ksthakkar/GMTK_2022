using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Karan 
 * 
 * Takes a 4 corners, scales, breaks tiles based on rotation and row,col count
 * 
 * Has some errors with scaling
 */

public class GridSystem : MonoBehaviour
{
    public GameObject[] corners;
    public GameObject gridPoint;
    public static int[] size = {14, 13};

    private float length, width;
    private static int l, w;

    public static GameObject[,] gridNum = new GameObject [size[0], size[1]];
    void Start()
    {
        length = distancetoCorner(corners[0], corners[1]);
        width = distancetoCorner(corners[0], corners[2]);

        returnPositions();

        //gridNum[0, 0].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        hideCorners();

    }

    float distancetoCorner(GameObject a, GameObject b)
    {
        float deltaX = Mathf.Abs(a.transform.position.x - b.transform.position.x);
        float deltaY = Mathf.Abs(a.transform.position.y - b.transform.position.y);
        return Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2));
    }

    float splitDistance(float x, int count)
    {
        return x / (count - 1);
    }

    float getB(GameObject a, GameObject b)
    {
        return Mathf.Abs(a.transform.position.x - b.transform.position.x);
    }

    float getC(GameObject a, GameObject b)
    {
        return Mathf.Abs(a.transform.position.y - b.transform.position.y);
    }

    //properties of similar triangles
    Vector2 newXPose(float interval, GameObject aO, GameObject bO, float a_len, float b, float c)
    {
        float scaleFactor = interval / a_len;
        float b_new = b * scaleFactor;
        float c_new = c * scaleFactor;

        return new Vector2(aO.transform.position.x + b_new, aO.transform.position.y + c_new);
    }

    Vector2 newYRow(float interval, GameObject aO, GameObject bO, float a_len, float b, float c)
    {
        float scaleFactor = interval / a_len;
        float b_new = b * scaleFactor;
        float c_new = c * scaleFactor;

        return new Vector2(aO.transform.position.x + b_new, bO.transform.position.y - c_new);
    }

    void hideCorners()
    {
        foreach (GameObject g in corners){
            g.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void hidePoints(GameObject a)
    {
        a.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


    void returnPositions()
    {
        float columnInterval = splitDistance(length, size[1]);
        float rowInterval = splitDistance(width, size[0]);

        int newColumn = size[1] - 1;

        for (int i = 0; i < size[0]; i++) //for each row
        {
            Vector2 y = newYRow((rowInterval * i), corners[0], corners[0], width, getB(corners[0], corners[2]), getC(corners[0], corners[2]));
            Vector2 y2 = newYRow((rowInterval * i), corners[1], corners[1], width, getB(corners[1], corners[3]), getC(corners[1], corners[3]));
        
            GameObject gridObjectY = Instantiate(gridPoint, y, Quaternion.identity);
            gridNum[i, 0] = gridObjectY;
            GameObject gridObjectY2 = Instantiate(gridPoint, y2, Quaternion.identity);
            gridNum[i, newColumn] = gridObjectY2;
            hidePoints(gridObjectY);
            hidePoints(gridObjectY2);

            for (int j = 0; j < (newColumn); j++)// for each column 
            {
                Vector2 x = newXPose((columnInterval * j), gridObjectY, gridObjectY2, length, getB(gridObjectY, gridObjectY2), getC(gridObjectY, gridObjectY2));
                GameObject gridObjectX = Instantiate(gridPoint, x, Quaternion.identity);
                gridNum[i, j] = gridObjectX;
                hidePoints(gridObjectX);
            }

        }

    }

}
