using UnityEngine;

public class Awesome2DCamera : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 newPosition = new Vector3(Target.transform.position.x, Target.transform.position.y+2f, Target.transform.position.z);
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}