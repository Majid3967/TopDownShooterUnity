using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    PlayerController controller;
    GunController gunController;
    Camera playerCamera;
    public float speed = 5;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Input
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 velocity = input * speed;
        controller.Move(velocity);
        //Rotation Input
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if(plane.Raycast(ray,out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            controller.LookAt(point);
            Debug.DrawLine(ray.origin, point,Color.red);
        }

        //Weapon Input
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
