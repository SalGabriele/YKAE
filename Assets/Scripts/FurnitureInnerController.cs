using UnityEngine;

public class FurnitureInnerController : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponentInParent<FurnitureController>().MouseDown();
    }

    public void RotateFurniture()
    {
        int currentRotation = Mathf.RoundToInt(transform.eulerAngles.y);
        int newRotation = 0;
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
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newRotation, transform.eulerAngles.z);
    }
}
