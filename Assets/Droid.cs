using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Droid : MonoBehaviour
{

    public int hp = 1;
    public int fuseeCount = 1;
    private bool jumpKeyWasPressed = false;
    private float horizontalInput = 0;

    private Rigidbody rigidbodyComponent;

    private bool isGrounded = false;
    
    private int collectibles = 0;
    private int speedRatio = 1;

    private bool cameraInverted = false;

    [SerializeField] private GameObject fusee;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hp: " + hp);
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)){
            jumpKeyWasPressed = true;
        }
        if(collectibles >= 5){
            speedRatio = 2;
        }
        if (Input.GetKeyDown(KeyCode.S) && fuseeCount > 0)
        {
            fuseeCount --;
            GameObject ball = Instantiate(fusee, transform.position,transform.rotation);
            ball.GetComponent<Rigidbody>().velocity = new Vector3(20,ball.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3((horizontalInput*2)*speedRatio, rigidbodyComponent.velocity.y, 0);
        if(jumpKeyWasPressed && isGrounded){
            rigidbodyComponent.AddForce((Vector3.up*5)*speedRatio,ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            collectibles ++;
            fuseeCount ++;
            hp ++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == 6)
        {
            Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
            if(!cameraInverted){
                transforms[3].rotation = Quaternion.Euler(0, 0, 180);
            } else{
                transforms[3].rotation = Quaternion.Euler(0, 0, 0);
            }
            cameraInverted = !cameraInverted;
        }
        if (other.gameObject.layer == 10)
        {
            hp -= 2;
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == 11)
        {
            other.gameObject.GetComponent<SkyStarsManager>().enabled = true;
        }
        if (other.tag == "Exit"){
            SceneManager.LoadScene("Level2") ;
        }

    }

}
