using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCreator : MonoBehaviour
{
    public Transform furniturePos;
    
    public void InstantiateFurniture(GameObject furniture)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        FindObjectOfType<Furniture>().Holding = true;
        GameObject clone = Instantiate(furniture, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("Furniture").transform);
    }

    public void InstantiateFurnitureJson(GameObject furniture, Vector3 position, Quaternion rotation, Color color, int index)
    {
        GameObject furnitureClone = Instantiate(furniture, position, rotation, GameObject.FindGameObjectWithTag("Furniture").transform);
        furnitureClone.GetComponentInChildren<MeshRenderer>().material.color = color;
        furnitureClone.GetComponentInChildren<Collider>().enabled = true;
        furnitureClone.GetComponent<FurnitureController>().snapped = true;
        furnitureClone.GetComponent<FurnitureController>().index = index;
    }
}
