using UnityEngine;
using UnityEngine.InputSystem;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector; // direction and distance of oscillation
    [SerializeField] float speed = 1f;        // speed of oscillation

    Vector3 startPosition;                   // starting position of the object
    Vector3 endPosition;                     // calculated as start + movementVector
    float movementFactor;                    // value between 0 and 1 for interpolation

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector; // calculate target position
    }
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f); // oscillates between 0 and 1
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}