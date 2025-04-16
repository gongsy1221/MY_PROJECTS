using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _roomNumText;

    public void UpdateText(string interact)
    {
        _interactText.text = interact;
    }

    public void UpdateRoomText()
    {
        _roomNumText.text = RoomManager.Instance.roomNumber.ToString() + "¹ø";
    }
}
