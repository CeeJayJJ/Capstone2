using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    public Animator animator; 
 

    private void Start()
    {
        rb.gameObject.GetComponent<Rigidbody>();

      
    }

    private void Update()
    {
      
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist; transform.position = movePos;
                AudioManager.Instance.PlaySFX("Walk");
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 movDir = new Vector3(x, 0, y);


        animator.SetFloat("xMove", movDir.x);
        animator.SetFloat("yMove", movDir.z);
        animator.SetFloat("Speed", movDir.sqrMagnitude);

        rb.velocity = movDir * speed;

        //if(x != 0 && x < 0)
        //{
        //    sr.flipX = true;
        //}
        //else if(x != 0 && x > 0)
        //{
        //    sr.flipX=false;
        //}    
    }
   
    }
