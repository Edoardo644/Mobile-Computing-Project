
using UnityEngine;

public class EdoCameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    
    }
}// volendo si può implementare il fatto di andare avanti con la videocamera
