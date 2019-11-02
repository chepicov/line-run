using System;
using UnityEngine;
using TouchScript;

public class Player : MonoBehaviour
{
    // This script can be used with any object that is supposed to follow a
    // route marked out by waypoints.

    // This script manages the amount to look ahead along the route,
    // and keeps track of progress and laps.

    [SerializeField] private WaypointCircuit circuit; // A reference to the waypoint-based route we should follow

    private float distance;
    bool isMoving = false;
    bool canMove = true;
    public float speed = 1f; // current speed of this object (calculated from delta since last frame)

    // setup script properties

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        distance = circuit.distances[circuit.startPoint];
        WaypointCircuit.RoutePoint startPoint = circuit.GetRoutePoint(distance);
        Vector3 position =
                startPoint
                        .position;
        transform.rotation =
                    Quaternion.LookRotation(
                        startPoint
                                .direction);
        position.y = transform.position.y;
        transform.position = position;
    }

    private void FixedUpdate() {
        if (isMoving && canMove) {
            if (distance > circuit.distances[circuit.stopPoint]) {
                distance = circuit.distances[circuit.startPoint];
                return;
            }
            WaypointCircuit.RoutePoint point = circuit.GetRoutePoint(distance);
            Vector3 position = point.position;
            transform.rotation = Quaternion.LookRotation(point.direction);
            position.y = transform.position.y;
            transform.position = position;
            distance += speed * Time.deltaTime;
        }
    }

    private void pointersPressedHandler(object sender, PointerEventArgs e)
    {
        isMoving = true;
    }

    private void pointersReleasedHandler(object sender, PointerEventArgs e)
    {
        isMoving = false;
    }

    
    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        { 
            TouchManager.Instance.PointersPressed += pointersPressedHandler;
            TouchManager.Instance.PointersReleased += pointersReleasedHandler;
        }
    }

    private void OnDisable()
    {
        if (TouchManager.Instance != null)
        { 
            TouchManager.Instance.PointersPressed -= pointersPressedHandler;
            TouchManager.Instance.PointersReleased -= pointersReleasedHandler;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "obstacle") {
            Debug.Log("Loh");
            Obstacle.Stop();
            canMove = false;
        }
    }
}
