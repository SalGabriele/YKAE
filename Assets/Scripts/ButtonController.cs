using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Button button;
    FurnitureController furnitureController;

    void Start()
    {
        button = GetComponent<Button>();
        furnitureController = GetComponentInParent<FurnitureController>();
        if (gameObject.name.Contains("Move"))
        {
            button.onClick.AddListener(delegate { furnitureController.MoveFurniture(); });
        }
        else if (gameObject.name.Contains("Rotate"))
        {
            button.onClick.AddListener(delegate { furnitureController.RotateFurniture(); });
        }
        else if (gameObject.name.Contains("ChangeColor"))
        {
            button.onClick.AddListener(delegate { furnitureController.ChangeColor(); });
        }
        else if (gameObject.name.Contains("Delete"))
        {
            button.onClick.AddListener(delegate { furnitureController.DeleteFurniture(); });
        }
    }
}
