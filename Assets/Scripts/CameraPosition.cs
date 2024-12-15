//une classe qui contrôle la position de la caméra

using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    public Transform player;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -12);
    }
}
