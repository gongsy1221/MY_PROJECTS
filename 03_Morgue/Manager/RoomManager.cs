using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoSingleton<RoomManager>
{
    [SerializeField] private GameObject[] specialRoomPrefabArray;
    private List<GameObject> specialRoomPrefabList;
    private List<GameObject> specialRoomPrefabSave;
    [SerializeField] private GameObject normalRoomPrefab;
    [SerializeField] private GameObject lastRoom;

    private Transform forwardMap;
    private Transform backwardMap;
    public GameObject currentRoom;
    private GameObject previousRoom;

    private SpriteRenderer roomNumObj;
    [SerializeField] private Sprite[] roomNumSprite;

    public int roomNumber = 0;
    private const int maxRooms = 8;

    public bool isRoom = false;
    private bool isSpecial = false;
    private bool isForward = true;

    private KeySpawner keySpawner;
    private Transform[] spawnPoints;

    private void Start()
    {
        isRoom = false;
        isSpecial = false;
        specialRoomPrefabList = new List<GameObject>(specialRoomPrefabArray);
        specialRoomPrefabSave = new List<GameObject>(specialRoomPrefabArray);

        Initialize();
    }

    private void Initialize()
    {
        forwardMap = GameObject.FindWithTag("FMap").transform;
        backwardMap = GameObject.FindWithTag("BMap").transform;

        keySpawner = KeySpawner.Instance;
        spawnPoints = GameObject.FindWithTag("Point").GetComponentsInChildren<Transform>();
        keySpawner.spawnPoints = spawnPoints;
        keySpawner.RandomSpawnKey();
    }

    public void PlayerCheck()
    {
        if (roomNumber <= maxRooms)
            StartCoroutine(GenerateRandomRoom());
        else
            EndGame();
    }

    private void EndGame()
    {
        Instantiate(lastRoom, isForward ? forwardMap.position : backwardMap.position,
            isForward ? forwardMap.rotation : backwardMap.rotation);
    }

    private IEnumerator GenerateRandomRoom()
    {
        isRoom = true;

        if (specialRoomPrefabList.Count == 0)
            specialRoomPrefabList = new List<GameObject>(specialRoomPrefabSave);

        int rand = Random.Range(0, specialRoomPrefabList.Count + 1);
        isSpecial = rand != 0;

        previousRoom = currentRoom;

        if (isSpecial)
            CreateSpecialRoom(rand - 1);
        else
            CreateNormalRoom();

        UpdateRoomNumberSprite();
        CloseRoomDoor();

        yield return null;
    }

    private void CreateNormalRoom()
    {
        currentRoom = Instantiate(normalRoomPrefab, isForward ? forwardMap.position : backwardMap.position,
            isForward ? forwardMap.rotation : backwardMap.rotation);
    }

    private void CreateSpecialRoom(int index)
    {
        currentRoom = Instantiate(specialRoomPrefabList[index], isForward ? forwardMap.position : backwardMap.position,
            isForward ? forwardMap.rotation : backwardMap.rotation);
        specialRoomPrefabList.RemoveAt(index);
    }

    private void UpdateRoomNumberSprite()
    {
        roomNumObj = currentRoom.transform.Find("paper").GetComponentInChildren<SpriteRenderer>();
        roomNumObj.sprite = roomNumSprite[roomNumber - 1];
    }

    private void CloseRoomDoor()
    {
        if (previousRoom != null)
        {
            string doorTag = isForward ? "FrontDoor" : "BackDoor";
            Door door = previousRoom.transform.Find(doorTag)?.GetComponent<Door>();
            if (door != null)
                door.CloseDoor();
        }
    }

    public bool RoomTransition(string tag)
    {
        SoundManager.StopAllFx();

        if (tag == "Backward")
        {
            roomNumber = isSpecial ? roomNumber + 1 : 1;
            isForward = false;
        }
        else if (tag == "Forward")
        {
            roomNumber = isSpecial ? 1 : roomNumber + 1;
            isForward = true;
        }
        else
            return false;

        if (!isRoom)
            PlayerCheck();

        return true;
    }

    public void CheckInRoom()
    {
        isRoom = false;

        Destroy(previousRoom);
        currentRoom.transform.Find("Street").gameObject.SetActive(true);

        var mapEvent = currentRoom.transform.Find("MapEvent");
        if (mapEvent != null)
            mapEvent.gameObject.SetActive(true);

        currentRoom.transform.Find("BackDoor").GetComponent<Door>().CloseDoor();

        Invoke(nameof(Initialize), 0.5f);
        GameObject.FindWithTag("Room").SetActive(false);
    }

    public void RestartGame()
    {
        SoundManager.StopAll();
        SceneManager.LoadScene("01_MainScene");
        StartCoroutine(FadeManager.Instance.FadeIn());
    }
}