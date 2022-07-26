using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    public bool snapped;
    public bool canvasIsVisible;
    public int index = -1;

    Material material;
    Collider innerCollider;
    FurnitureInnerController furnitureInnerController;
    SnappingRules snappingRules;

    private bool canBeSnapped;

    private void Start()
    {
        material = GetComponentInChildren<MeshRenderer>().material;
        innerCollider = GetComponentInChildren<Collider>();
        furnitureInnerController = GetComponentInChildren<FurnitureInnerController>();
        snappingRules = FindObjectOfType<SnappingRules>();
    }
    void Update()
    {
        if (snapped)
        {
            return;
        }
        furnitureInnerController.HandleRotationWhileHolding();
        if (Furniture.instance.Holding)
        {
            HandleRaycast();
        }
        HandleCancelling();
    }

    private void HandleCancelling()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
            Furniture.instance.Holding = false;
        }
    }

    private void HandleRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            canBeSnapped = CanBeSnapped(hit.transform.tag, transform.tag);
            UIManager.instance.ShowSnappingText(canBeSnapped);

            transform.position = hit.point;
            if ((CompareTag("Picture1") || CompareTag("Picture2")) && hit.transform.CompareTag("Wall"))
            {
                if (hit.transform.GetComponent<WallData>().leftWall)
                {
                    transform.Translate(0, 0, 0.02f);
                    transform.GetChild(0).eulerAngles = new Vector3(-90, 0, 0);
                    transform.GetChild(1).eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    transform.Translate(0.02f, 0, 0);
                    transform.GetChild(0).eulerAngles = new Vector3(-90, 90, 0);
                    transform.GetChild(1).eulerAngles = new Vector3(0, 270, 0);
                    transform.GetChild(1).position = new Vector3(transform.GetChild(1).position.x + 0.01f, transform.GetChild(1).position.y, transform.GetChild(1).position.z);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                snapped = true;
                UIManager.instance.ShowSnappingText(false, false);
                Furniture.instance.Holding = false;
                innerCollider.enabled = true;
                if (!hit.transform.CompareTag("Floor") && transform.CompareTag("Plate"))
                {
                    transform.parent = hit.transform.parent;
                }
                index = RoomManager.AddFurnitureToList(TagToFurnitureType(transform.tag), transform.position, transform.GetChild(0).rotation, material.color, index);
                return;
            }
        }
    }

    public void MouseDown()
    {
        if (!Furniture.instance.Holding)
        {
            Furniture.instance.HideCanvas();
            canvasIsVisible = !canvasIsVisible;
            foreach (Transform child in transform)
            {
                if (child.name.Contains("Canvas"))
                {
                    child.gameObject.SetActive(canvasIsVisible);
                }
            }
        }

    }

    public void MoveFurniture()
    {
        if (!transform.parent.CompareTag("Furniture"))
        {
            transform.parent = Furniture.instance.transform;
        }
        Furniture.instance.HideCanvas();
        canvasIsVisible = false;
        snapped = false;
        Furniture.instance.Holding = true;
        innerCollider.enabled = false;
    }

    public void DeleteFurniture()
    {
        Furniture.instance.HideCanvas();
        Destroy(gameObject);
    }

    public void ChangeColor()
    {
        Furniture.instance.HideCanvas();
        Furniture.instance.HideCanvas();
        canvasIsVisible = false;
        ColorPicker.Create(material.color, "Chose the color", SetColor, ColorFinished);
    }

    public void RotateFurniture()
    {
        furnitureInnerController.RotateFurniture();
        index = RoomManager.AddFurnitureToList(TagToFurnitureType(transform.tag), transform.position, transform.GetChild(0).rotation, material.color, index);
    }

    private void SetColor(Color currentColor)
    {
        material.color = currentColor;
    }

    private void ColorFinished(Color finishedColor)
    {
        RoomManager.FurnitureChangedColor(index, finishedColor);
    }

    FurnitureType TagToFurnitureType(string tag)
    {
        if (tag == "Chair") return FurnitureType.Chair;
        if (tag == "Table") return FurnitureType.Table;
        if (tag == "Plate") return FurnitureType.Plate;
        if (tag == "Picture1") return FurnitureType.Picture1;
        if (tag == "Picture2") return FurnitureType.Picture2;

        throw new System.ComponentModel.InvalidEnumArgumentException();
    }

    private bool CanBeSnapped(string placedObjectTag, string objectToSnapTag)
    {
        foreach (var snappingRule in snappingRules.snappingRulesList)
        {
            if (snappingRule.placedObjectTag == placedObjectTag && snappingRule.objectToSnapTag == objectToSnapTag)
            {
                return snappingRule.canBeSnapped;
            }
        }
        Debug.LogError(placedObjectTag + " & " + objectToSnapTag + " snapping rule should be added");
        return false;
    }
}
