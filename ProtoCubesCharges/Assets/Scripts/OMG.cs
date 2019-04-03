﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OMG : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int currentCharges;
    [SerializeField] private int maxCharges;

    public Text txt_charges;


    private RaycastHit hit;
    private Cube touchedObject;
    private float lastShot;

    private void Start()
    {
        lastShot = Time.time;
        txt_charges.text = "Charges : " + currentCharges + " / " + maxCharges;
    }

    private void Update()
    {
        if (Time.time > lastShot + cooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shot(false);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Shot(true);
            }
        }
    }

    private void Shot(bool alourdir)
    {
        if (CalculateRayCast())
        {
            if (touchedObject != null)
            {
                if (alourdir && currentCharges < maxCharges && touchedObject.NbCharges > 0)
                {
                    touchedObject.Alourdir();
                    currentCharges++;
                    //currentCharges += touchedObject.IncreaseCharge(currentCharges, maxCharges - currentCharges);
                }
                else if (!alourdir && currentCharges > 0 && touchedObject.NbChargesAjoutables > 0)
                {
                    touchedObject.Alleger();
                    currentCharges--;
                    //currentCharges += touchedObject.DecreaseCharge(currentCharges, maxCharges - currentCharges);
                }
                txt_charges.text = "Charges : " + currentCharges + " / " + maxCharges;
            }
        }
        lastShot = Time.time;
    }

    private bool CalculateRayCast()
    {
        bool result = Physics.Raycast(firePoint.position, firePoint.forward, out hit, 500f, layerMask.value);
        if (result)
            touchedObject = hit.transform.GetComponent<Cube>();
        return result;
    }
}
