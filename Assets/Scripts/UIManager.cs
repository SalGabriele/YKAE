using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject savePanel;
    [SerializeField] TextMeshProUGUI inputText;
    [SerializeField] GameObject RoomLoadButton;
    [SerializeField] GameObject RoomLoadPanel;
    private string location;
    private string fileName;
    private string json;

    private void Awake()
    {
        location = Application.persistentDataPath + "/RoomFiles/";
        if (!Directory.Exists(location))
        {
            Directory.CreateDirectory(location);
        }
    }

    public void NewRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveRoom()
    {
        json = JsonUtility.ToJson(RoomManager.roomData);
        savePanel.SetActive(true);
    }

    public void LoadRoom()
    {
        RoomLoadPanel.SetActive(true);
        var files = Directory.GetFiles(location);
        foreach (var a in files)
        {
            GameObject clone = Instantiate(RoomLoadButton, RoomLoadPanel.transform);
            clone.GetComponentInChildren<TextMeshProUGUI>().text = a.Replace(location, "");
            clone.GetComponent<Button>().onClick.AddListener(() => LoadSelectedRoom(clone));
        }
    }

    public void ConfirmSave()
    {
        fileName = inputText.text + ".json";
        File.WriteAllText(location + fileName, json);
        savePanel.SetActive(false);
    }

    private void LoadSelectedRoom(GameObject go)
    {
        var fileName = go.GetComponentInChildren<TextMeshProUGUI>().text;
        var content = File.ReadAllText(location + fileName);
        RoomManager.LoadRoom(content);
    }

    public static void ChangeFurnitureGridStatus(bool status)
    {
        Button[] furnitureGridButtons = GameObject.FindGameObjectWithTag("FurnitureGrid").GetComponentsInChildren<Button>();
        foreach (Button button in furnitureGridButtons)
        {
            button.interactable = status;
        }
    }
}
