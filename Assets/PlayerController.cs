using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    private Camera cam;

    public Vector3 mouse;

    public Rigidbody body;

    public GameObject weapon;

    public float rpm;

    public GameObject bullet;

    public bool canShoot;

    public float cd = 1;

    public float shot;

    public float playerHealth;

    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        if(playerHealth <= 0)
        {
            Gameover();
            Debug.Log(":(");
        }

        if (!canShoot)
        {
            shot -= Time.deltaTime;
        }

        if (shot <= 0)
        {
            canShoot = true;
        }

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
            shot = cd;
            canShoot = false;

        }

        void Shoot()
        {
            Instantiate(bullet.transform, weapon.transform.position, weapon.transform.rotation);
        }
    }
    public void Gameover()
    {
        Application.Quit();
    }



}
