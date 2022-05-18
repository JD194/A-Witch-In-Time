using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    //spell modifiers

    public bool steamActive;
    public bool torchActive;



    public float speed;
    public float rotationSpeed;

    Rigidbody rb;
    float vert, horz = 0;
    float camTilt, camYaw = 0;
    Vector3 moveDir;

    public Camera thisCam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;
        transform.Translate(moveDir * time * speed);
    }

    void Movement()
    {
        vert = 0;
        horz = 0;

        if (Input.GetKey(KeyCode.W))
        {
            vert = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vert = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horz = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horz = 1;
        }

        camTilt = 0;
        camYaw = 0;

        float mouseX = 0;
        float mouseY = 0;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            mouseX = Input.GetTouch(0).deltaPosition.x;
            mouseY = Input.GetTouch(0).deltaPosition.y;
            Debug.Log("X is: " + mouseX + "Mouse Y is: " + mouseY);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            camTilt = 1;
            Debug.Log(Camera.main.transform.rotation.eulerAngles.x);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            camTilt = -1;
            Debug.Log(Camera.main.transform.rotation.eulerAngles.x);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            camYaw = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            camYaw = 1;
        }

        moveDir = new Vector3(horz, 0, vert);
        moveDir.Normalize();
        transform.Rotate(0, camYaw * rotationSpeed * Time.deltaTime, 0);
        thisCam.transform.Rotate(camTilt * -rotationSpeed * Time.deltaTime, 0, 0);
        RotationCap();
    }

    void RotationCap()
    {
        if (transform.rotation.z > 0 || transform.rotation.z < 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        }

        float minRotation = -65;
        float maxRotation = 60;
        Vector3 currentRotation = thisCam.transform.localRotation.eulerAngles;

        currentRotation.x = ConvertToAngle180(currentRotation.x);
        currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation, maxRotation);
        thisCam.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public static float ConvertToAngle180(float input)
    {
        while (input > 360)
        {
            input = input - 360;
        }
        while (input < -360)
        {
            input = input + 360;
        }
        if (input > 180)
        {
            input = input - 360;
        }
        if (input < -180)
            input = 360 + input;
        return input;
    }
}
