using UnityEngine;

public class Furniture : MonoBehaviour
{
    public static Furniture instance;

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
