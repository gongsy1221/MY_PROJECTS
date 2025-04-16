using System.Collections;
using UnityEngine;

public class PlayerLookCheck : MonoBehaviour
{
    [SerializeField] private GameObject ghostObj;
    [SerializeField] private AudioClip audioClip;

    public Transform ghostGirl;

    private Camera playerCamera;
    private Player _player;
    
    private float gazeTime = 1.0f;
    private float timer = 0.0f;
    private float ghostGirlY;
    public float moveDuration = 1f;
    public int moveSteps = 3;

    private void Awake()
    {
        playerCamera = Camera.main;
        _player = FindObjectOfType<Player>();
        ghostObj = _player.transform.Find("PlayerDie").gameObject;

        if (ghostGirl != null)
            ghostGirlY = ghostGirl.transform.position.y;

        SoundManager.PlayFx(audioClip, 0.6f, true);
    }

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                timer += Time.deltaTime;
                if (timer >= gazeTime)
                {
                    _player.PlayerDie();
                    ghostGirl.GetComponent<CapsuleCollider>().enabled = false;
                    timer = 0.0f;
                    SoundManager.StopFx(audioClip, 1f);

                    StartCoroutine(MoveGhostGirlCoroutine());
                }
            }
            else
            {
                timer = 0.0f;
            }
        }
        else
        {
            timer = 0.0f;
        }
    }

    private IEnumerator MoveGhostGirlCoroutine()
    {
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraPosition = playerCamera.transform.position;

        Vector3 startPosition = ghostGirl.position;
        Vector3 endPosition = cameraPosition + cameraForward;
        endPosition.y = ghostGirlY - 0.1f;

        for (int i = 1; i <= moveSteps; i++)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, (float)i / moveSteps);
            ghostGirl.position = newPosition;
            ghostGirl.LookAt(new Vector3(cameraPosition.x, ghostGirlY, cameraPosition.z));

            yield return new WaitForSeconds(moveDuration);
        }

        ghostGirl.gameObject.SetActive(false);
        ghostObj.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        RoomManager.Instance.RestartGame();
    }
}
