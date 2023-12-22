using UnityEngine;

public class AutonomousAirplane : MonoBehaviour
{
    public Transform[] waypoints;
    private float speed = 1f;
    private float rotationSpeed = 5f;
    private float heightChangeSpeed = 0.5f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        // Déplacez l'avion vers le prochain waypoint
        MoveTowardsWaypoint();

        // Changez de waypoint lorsque l'avion atteint le waypoint actuel
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void MoveTowardsWaypoint()
    {
        // Calculez la direction vers le waypoint
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Calculez la rotation vers la direction du waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Effectuez une rotation douce vers la direction du waypoint
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Déplacez l'avion vers le waypoint
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Changez de hauteur
        float targetHeight = waypoints[currentWaypointIndex].position.y;
        float newHeight = Mathf.MoveTowards(transform.position.y, targetHeight, heightChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
