using UnityEngine;

public class FurnitureCreator : MonoBehaviour
{
    public static FurnitureCreator instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void InstantiateFurniture(GameObject furniturePrefab)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
        Furniture.instance.Holding = true;
        Instantiate(furniturePrefab, pos, Quaternion.identity, Furniture.instance.transform);
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
