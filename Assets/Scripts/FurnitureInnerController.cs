using UnityEngine;

public class FurnitureInnerController : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponentInParent<FurnitureController>().MouseDown();
    }

    public void RotateFurniture(bool clockwise=false)
    {
        int currentRotation = Mathf.RoundToInt(transform.eulerAngles.y);
        int newRotation = 0;
        if (clockwise)
        {
            switch (currentRotation)
            {
                case 0:
                    newRotation = 90;
                    break;
                case 90:
                    newRotation = 180;
                    break;
                case 180:
                    newRotation = 270;
                    break;
                case 270:
                    newRotation = 0;
                    break;
            }
        }
        else
        {
            switch (currentRotation)
            {
                case 0:
                    newRotation = 270;
                    break;
                case 90:
                    newRotation = 0;
                    break;
                case 180:
                    newRotation = 90;
                    break;
                case 270:
                    newRotation = 180;
                    break;
            }
        }
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newRotation, transform.eulerAngles.z);
    }

    public void HandleRotationWhileHolding()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            RotateFurniture();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            RotateFurniture(true);
        }
    }
}