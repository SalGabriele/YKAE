using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{    
    public static RoomData roomData = new();

    [SerializeField] GameObject chair;
    [SerializeField] GameObject table;
    [SerializeField] GameObject plate;
    [SerializeField] GameObject picture1;
    [SerializeField] GameObject picture2;

    private static bool reloading;
    private static string roomDataJson;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("RoomManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (reloading)
        {
            roomData = JsonUtility.FromJson<RoomData>(roomDataJson);
            for (int i = 0; i < roomData.furnitureData.Count; i++)
            {
                FurnitureData fd = roomData.furnitureData[i];
                GameObject furniture = null;
                switch (fd.furniture)
                {
                    case FurnitureType.Chair:
                        furniture = chair;
                        break;
                    case FurnitureType.Table:
                        furniture = table;
                        break;
                    case FurnitureType.Plate:
                        furniture = plate;
                        break;
                    case FurnitureType.Picture1:
                        furniture = picture1;
                        break;
                    case FurnitureType.Picture2:
                        furniture = picture2;
                        break;
                }
                FurnitureCreator.instance.InstantiateFurnitureJson(furniture, fd.position, fd.rotation, fd.color, i);
            }
            reloading = false;
        }
    }

    public static int AddFurnitureToList(FurnitureType furnitureType, Vector3 position, Quaternion rotation, Color color, int index)
    {
        if (index == -1)
        {
            FurnitureData furnitureData = new(furnitureType, position, rotation, color);
            roomData.furnitureData.Add(furnitureData);
            return roomData.furnitureData.Count - 1;
        }
        else
        {
            roomData.furnitureData[index].position = position;
            roomData.furnitureData[index].rotation = rotation;
            roomData.furnitureData[index].color = color;
            return index;
        }
    }

    public static void FurnitureRemoved(int index)
    {
        roomData.furnitureData.RemoveAt(index);
    }

    public static void FurnitureChangedColor(int index, Color color)
    {
        roomData.furnitureData[index].color = color;
    }

    public static void LoadRoom(string json)
    {
        roomDataJson = json;
        reloading = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
