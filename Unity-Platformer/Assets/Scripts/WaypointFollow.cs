using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int CurrentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[CurrentWaypointIndex].transform.position, transform.position)<0.1f)
        {
            CurrentWaypointIndex++;
            if(CurrentWaypointIndex >= waypoints.Length)
            {
                CurrentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[CurrentWaypointIndex].transform.position, speed * Time.deltaTime);

    }
}
