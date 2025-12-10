
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotate;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainengine;
    [SerializeField] ParticleSystem thrustparticals;
    [SerializeField] ParticleSystem leftPartical;
    [SerializeField] ParticleSystem rightPartical;
    AudioSource audioSource;
    Rigidbody rb;


    void OnEnable()
    {
        thrust.Enable();
        rotate.Enable();
        rb = GetComponent<Rigidbody>();

    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        thrustprocess();
        rotationprocess();

    }

    private void thrustprocess()
    {
        if (thrust.IsPressed())
        {
            startthrusting();

        }
        else
        {
            stopthrusting();
        }
    }

    private void startthrusting()
    {
        rb.AddRelativeForce(thrustStrength * Time.fixedDeltaTime * Vector3.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainengine); ;
        }
        if (!thrustparticals.isPlaying)
        {
            thrustparticals.Play();
        }
    }
    private void stopthrusting()
    {
        audioSource.Stop();
        thrustparticals.Stop();
    }

    void rotationprocess()
    {
        float rotationinput = rotate.ReadValue<float>();
        
        if (rotationinput < 0)
        {
            leftrotate();
        }
        else if (rotationinput > 0)
        {
            rightrotate();
        }
        else
        {
            stoprotate();
        }

    }

    private void stoprotate()
    {
        leftPartical.Stop();
        rightPartical.Stop();
    }

    private void rightrotate()
    {
        applyrotation(-rotationStrength);
        if (!rightPartical.isPlaying)
        {
            leftPartical.Stop();
            rightPartical.Play();
        }
    }

    private void leftrotate()
    {
        applyrotation(rotationStrength);
        if (!leftPartical.isPlaying)
        {
            rightPartical.Stop();
            leftPartical.Play();
        }
    }

    void applyrotation(float rotationdirection)
    {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationdirection * Time.fixedDeltaTime);
            rb.freezeRotation = false;
    }
    
}