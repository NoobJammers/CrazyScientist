using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootHandler : MonoBehaviour
{

    [SerializeField] GameObject pointPrefab;
    [SerializeField] GameObject[] points;
    [SerializeField] int noOfPoints;
    [SerializeField] float force;

    [SerializeField] GameObject blob;

    Vector2 startPoint, endPoint, direction;

    public bool isHovering = true; //Set when a blob is selected, set default to false

    private void Start()
    {
        points = new GameObject[noOfPoints];
        for (int i = 0; i < noOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            points[i].SetActive(false);
            Color colourTemp = points[i].GetComponent<SpriteRenderer>().color;
            points[i].GetComponent<SpriteRenderer>().color = new Color(colourTemp.r, colourTemp.b, colourTemp.g, (1f / noOfPoints) * (noOfPoints - i));
        }
    }



    private void OnMouseDown()
    {
        if (isHovering)
        {
            //TODO: set blob gameobject


            startPoint = transform.position;
            for (int i = 1; i < noOfPoints; i++) //Not setting the first point to active state, hence i iterates from 1
            {

                points[i].SetActive(true);
            }
        }
    }


    private void OnMouseDrag()
    {
        if (isHovering)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = endPoint - startPoint;
            transform.right = direction;

            for (int i = 0; i < points.Length; i++)
            {
                points[i].transform.position = PointPos(i * 0.1f);
            }
        }

    }

    private void OnMouseUp()
    {
        if (isHovering)
        {
            Debug.Log("Shoot");
            //TODO: enable collider of blob   
            for (int i = 0; i < noOfPoints; i++)
            {
                points[i].SetActive(false);
            }
        }

    }

    void Shoot()
    {
        blob.GetComponent<Rigidbody2D>().velocity = transform.right * force;
    }

    Vector2 PointPos(float time)
    {
        Vector2 currentPos = (Vector2)transform.position + (direction.normalized * force * time) + 0.5f * Physics2D.gravity * (time * time);
        return currentPos;
    }
}
