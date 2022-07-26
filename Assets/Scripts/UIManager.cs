using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] GameObject savePanel;
    [SerializeField] TextMeshProUGUI inputText;
    [SerializeField] TextMeshProUGUI snappingText;
    [SerializeField] GameObject RoomLoadButton;
    [SerializeField] GameObject RoomLoadPanel;

    private string location;
    private string fileName;
    private string json;

    private static string snappingTextTrue = "Object can be placed here";
    private static string snappingTextFalse = "Object can't be placed here";

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
            clone.GetComponentInChildren<TextMeshProUGUI>().text = a.Replace(location, "").Replace(".json","");
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
        var fileName = go.GetComponentInChildren<TextMeshProUGUI>().text+".json";
        var content = File.ReadAllText(location + fileName);
        RoomManager.LoadRoom(content);
    }

    public void ChangeFurnitureGridStatus(bool status)
    {
        Button[] furnitureGridButtons = GameObject.FindGameObjectWithTag("FurnitureGrid").GetComponentsInChildren<Button>();
        foreach (Button button in furnitureGridButtons)
        {
            button.interactable = status;
        }
    }

    public void ShowSnappingText(bool canBeSnapped, bool showText=true)
    {
        if (showText && canBeSnapped)
        {
            snappingText.text = snappingTextTrue;
            snappingText.color = Color.green;
        }
        else if(showText && !canBeSnapped)
        {
            snappingText.text = snappingTextFalse;
            snappingText.color = Color.red;
        }
        else{
            snappingText.text = "";
        }
    }
}
