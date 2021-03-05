using UnityEngine;

public class AppStart : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerStartPoint;
    private GameObject player;
    void Start()
    {
        player = Instantiate(playerPrefab, playerStartPoint.transform.position, Quaternion.identity);
        player.name = "Player";
    }
}
