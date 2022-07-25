using UnityEngine;

public class Furniture : MonoBehaviour
{
    private bool holding;
    public bool Holding
    {
        get { return holding; }
        set
        {
            holding = value;
            UIManager.ChangeFurnitureGridStatus(!holding);
        }
    }

    public void HideCanvas()
    {
        GameObject[] furnitureCanvas = GameObject.FindGameObjectsWithTag("FurnitureCanvas");
        foreach (GameObject canvas in furnitureCanvas)
        {
            canvas.SetActive(false);
        }
    }
}
