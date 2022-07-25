using UnityEngine;

public class FurnitureCreator : MonoBehaviour
{
    Furniture furniture;
    
    private void Start()
    {
        furniture = FindObjectOfType<Furniture>();
    }

    public void InstantiateFurniture(GameObject furniturePrefab)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        furniture.Holding = true;
        Instantiate(furniturePrefab, pos, Quaternion.identity, furniture.transform);
    }

    public void InstantiateFurnitureJson(GameObject furniture, Vector3 position, Quaternion rotation, Color color, int index)
    {
        GameObject furnitureClone = Instantiate(furniture, position, Quaternion.identity, GameObject.FindGameObjectWithTag("Furniture").transform);
        furnitureClone.transform.GetChild(0).rotation = rotation;
        furnitureClone.GetComponentInChildren<MeshRenderer>().material.color = color;
        furnitureClone.GetComponentInChildren<Collider>().enabled = true;
        FurnitureController furnitureController = furnitureClone.GetComponent<FurnitureController>();
        furnitureController.snapped = true;
        furnitureController.index = index;
    }
}
