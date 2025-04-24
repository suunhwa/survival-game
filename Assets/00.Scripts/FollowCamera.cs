using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] private float posX;
    [SerializeField] private float posY;
    [SerializeField] private float posZ;

    [SerializeField] private float speed = 2.0f;

    void Start()
    {
        transform.position = new Vector3(
            player.transform.position.x + posX,
            player.transform.position.y + posY,
            player.transform.position.z + posZ
        );
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.Lerp(transform.position, 
            new Vector3(
                player.transform.position.x + posX, 
                player.transform.position.y + posY, 
                player.transform.position.z + posZ), 
            Time.deltaTime * speed);
    }
}
