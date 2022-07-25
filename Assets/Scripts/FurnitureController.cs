using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    public bool snapped;
    public bool canvasIsVisible;
    private Material material;
    public int index = -1;
    SnappingRules snappingRules;

    private void Start()
    {
        material = GetComponentInChildren<MeshRenderer>().material;
        snappingRules = FindObjectOfType<SnappingRules>();
    }
    void Update()
    {
        if (snapped)
        {
            return;
        }
      
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.position = hit.point;
            if ((tag == "Picture1" || tag == "Picture2") && hit.transform.tag=="Wall")
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
                    transform.GetChild(1).position = new Vector3(transform.GetChild(1).position.x+0.01f, transform.GetChild(1).position.y, transform.GetChild(1).position.z);
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                if(canBeSnapped(hit.transform.tag, transform.tag))
                {
                    snapped = true;
                    FindObjectOfType<Furniture>().Holding = false;
                    GetComponentInChildren<Collider>().enabled = true;
                    if(hit.transform.tag!="Floor" && transform.tag == "Plate")
                    {
                        transform.parent = hit.transform.parent;
                    }
                    index = RoomManager.AddFurnitureToList(TagToFurnitureType(transform.tag), transform.position, transform.GetChild(0).rotation, material.color, index);
                    return;
                }
            }
        }
    }

    public void MouseDown()
    {
        if (!FindObjectOfType<Furniture>().Holding)
         {
            transform.GetComponentInParent<Furniture>().HideCanvas();
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
        if (transform.parent.tag != "Furniture")
        {
            transform.parent = GameObject.FindGameObjectWithTag("Furniture").transform;
        }
        transform.GetComponentInParent<Furniture>().HideCanvas();
        canvasIsVisible = false;
        snapped = false;
        FindObjectOfType<Furniture>().Holding = true;
        GetComponentInChildren<Collider>().enabled = false;
    }

    public void DeleteFurniture()
    {
        transform.GetComponentInParent<Furniture>().HideCanvas();
        Destroy(gameObject);
    }

    public void ChangeColor()
    {
        transform.GetComponentInParent<Furniture>().HideCanvas();
        canvasIsVisible = false;
        //GameObject.FindObjectOfType<UIController>().EnableColorPicker();
        ColorPicker.Create(material.color, "Chose the color", SetColor, ColorFinished);
    }

    public void RotateFurniture()
    {
        GetComponentInChildren<FurnitureInnerController>().RotateFurniture();
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

    private bool canBeSnapped(string placedObjectTag, string objectToSnapTag)
    {
        foreach(var snappingRule in snappingRules.snappingRulesList)
        {
            if(snappingRule.placedObjectTag==placedObjectTag && snappingRule.objectToSnapTag == objectToSnapTag)
            {
                return snappingRule.canBeSnapped;
            }
        }
        Debug.LogError(placedObjectTag + " & " + objectToSnapTag + " snapping rule should be added");
        return false;
    }
}
