using UnityEngine;

public class AppStart : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerStartPoint;
    [SerializeField] private GameObject canvasPrefab;
    private GameObject player;

    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerPrefab, playerStartPoint.transform.position, Quaternion.identity);
        player.name = "Astronaut";
    }
}
