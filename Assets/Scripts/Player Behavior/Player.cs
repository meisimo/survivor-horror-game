using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float rotatioSpeed = 250;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate(){
        float moveHorizontal = Input.GetAxis("Horizontal"); 
        float moveVertical   = Input.GetAxis("Vertical"); 

        transform.Rotate(0,moveHorizontal*Time.deltaTime * rotatioSpeed,0);
        transform.Translate(0,0,moveVertical*Time.deltaTime*speed);

        animator.SetFloat("VelX",moveHorizontal);
        animator.SetFloat("VelY",moveVertical);
    }

}
