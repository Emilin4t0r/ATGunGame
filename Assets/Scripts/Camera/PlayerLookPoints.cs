using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookPoints : MonoBehaviour
{

    public static PlayerLookPoints instance;

    public Transform gunLook, breechLook, ammoLook;
    public Transform gunPos, breechPos, ammoPos;
    Vector3 lookTarget, posTarget;
    float angleDiff, posDiff;
    float rotationSpeed = 250f;
    float moveSpeed = 5f;
    public int currentView;
    public bool movementInProgress;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MoveToView("Gun", 5, 250);
        NextView();
        NextView();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            NextView();
        }
    }

    void MoveToView(string pos, float m, float r)
    {
        switch (pos)
        {
            case "Gun":
                lookTarget = gunLook.position;
                posTarget = gunPos.position;
                break;
            case "Breech":
                lookTarget = breechLook.position;
                posTarget = breechPos.position;
                break;
            case "Ammo":
                lookTarget = ammoLook.position;
                posTarget = ammoPos.position;
                break;
        }
        moveSpeed = m;
        MoveToTarget();
        rotationSpeed = r;
        RotateToTarget();
    }

    // Quick and dirty way of contstantly switching views.
    // gun -> breech -> ammo -> breech -> repeat
    public void NextView()
    {
        // views: 0 = gun, 1 & 3 = breech, 2 = ammo
        if (currentView < 3)
            currentView++;
        else
            currentView = 0;

        switch (currentView)
        {
            case 0:
                MoveToView("Gun", 5, 120);
                break;
            case 1:
                MoveToView("Breech", 5, 120);
                break;
            case 2:
                MoveToView("Ammo", 4.5f, 375);
                break;
            case 3:
                MoveToView("Breech", 4.5f, 375);
                break;
        }
        movementInProgress = true;
    }

    void FixedUpdate()
    {
        if (angleDiff > 1 || posDiff > 0.025f)
        {
            RotateToTarget();
            MoveToTarget();            
        } else
        {
            if (movementInProgress == true) movementInProgress = false;
        }
    }

    void RotateToTarget()
    {
        Vector3 direction = lookTarget - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        angleDiff = Quaternion.Angle(transform.rotation, targetRotation);
    }

    void MoveToTarget()
    {
        float step = moveSpeed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, posTarget, step);
        posDiff = Vector3.Distance(transform.position, posTarget);
    }
}
