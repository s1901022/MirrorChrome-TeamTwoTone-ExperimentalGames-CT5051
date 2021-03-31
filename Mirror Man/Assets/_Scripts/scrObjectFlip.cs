﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrObjectFlip : MonoBehaviour
{
    private scrEntity m_entity;
    private Rigidbody2D rb;
    private bool top;
    public GameObject mirror;
    public bool canFlip;
    public float height;
    public Transform reflectNormal;
    public float initialFlipDirection;
    public float checkFlip;
    RaycastHit2D hitReflective;

    public GameObject prefabReflection;
    public GameObject myReflection;

    // Start is called before the first frame update
    void Start()
    {
        m_entity = GetComponent<scrEntity>();
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.Find("Player Reflection") == null)
        {
            myReflection = Instantiate(prefabReflection, transform.position, Quaternion.identity);
        }
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_entity.GetGrounded())
        {
            canFlip = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canFlip)
        {
            MirrorObject();
        }
        UpdateReflection();
    }

    void DetectTerrain()
    {
        if (checkFlip == 1f)
        {
            hitReflective = Physics2D.Raycast(transform.position - new Vector3(0f, transform.localScale.y, 0f), Vector2.down);
        }
        else if (checkFlip == -1f)
        {
            hitReflective = Physics2D.Raycast(transform.position + new Vector3(0f, transform.localScale.y + 1, 0f), Vector2.down);
        }
        reflectNormal = hitReflective.transform;
    }

    void MirrorObject()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
        canFlip = false;

        DetectTerrain();

        if (hitReflective.collider != null && hitReflective.collider.tag == "Reflective")
        {
            float distanceBetweenReflection = transform.position.y - reflectNormal.position.y;
            float reflectiveScale = reflectNormal.localScale.y / 2;

            if (transform.position.y > reflectNormal.position.y)
            {
                //transform.position = new Vector3(transform.position.x, ((reflectNormal.position.y - transform.localScale.y) + reflectiveScale) - distanceBetweenReflection, transform.position.z);
                transform.position = new Vector3(transform.position.x, (distanceBetweenReflection - reflectNormal.position.y) * -1, transform.position.z);
            }
            else if (transform.position.y < reflectNormal.position.y)
            {
                //transform.position = new Vector3(transform.position.x, ((reflectNormal.position.y + transform.localScale.y) - reflectiveScale) - distanceBetweenReflection, transform.position.z);
                transform.position = new Vector3(transform.position.x, (distanceBetweenReflection - reflectNormal.position.y) * -1, transform.position.z);
            }
            checkFlip *= -1;
            rb.gravityScale *= -1;
            Rotation();
        }
        else if (!m_entity.GetGrounded())
        {
            Debug.Log("Nope goodbye");
            checkFlip *= -1;
            rb.gravityScale *= -1;
            Rotation();
        }
    }

    void UpdateReflection()
    {
        DetectTerrain();
        if (hitReflective.collider != null && hitReflective.collider.tag == "Reflective")
        {
            myReflection.SetActive(true);
            float distanceBetweenReflection = transform.position.y - reflectNormal.position.y;
            float reflectiveScale = reflectNormal.localScale.y / 2;

            if (transform.position.y > reflectNormal.position.y)
            {
                //myReflection.transform.position = new Vector3(transform.position.x, ((reflectNormal.position.y + myReflection.transform.localScale.y) - reflectiveScale) - distanceBetweenReflection, transform.position.z);
                myReflection.transform.position = new Vector3(transform.position.x, (distanceBetweenReflection - reflectNormal.position.y) * -1, transform.position.z);
            }
            else if (transform.position.y < reflectNormal.position.y)
            {
                //myReflection.transform.position = new Vector3(transform.position.x, ((reflectNormal.position.y + myReflection.transform.localScale.y) - reflectiveScale) - distanceBetweenReflection, transform.position.z);
                myReflection.transform.position = new Vector3(transform.position.x, (distanceBetweenReflection - reflectNormal.position.y) * -1, transform.position.z);
            }
        }
        else if (!m_entity.GetGrounded())
        {
            myReflection.SetActive(false);
        }
    }

    public void Rotation()
    {
        if (top == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
        top = !top;
    }

    public void Reset()
    {
        checkFlip = initialFlipDirection;
        reflectNormal = null;
        if (initialFlipDirection < 0f && rb.gravityScale > 0)
        {
            rb.gravityScale *= -1;
            return;
        }
        if (initialFlipDirection > 0f && rb.gravityScale < 0)
        {
            rb.gravityScale *= -1;
            return;
        }

    }
}
