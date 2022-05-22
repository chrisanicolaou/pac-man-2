using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour
{
    private float speed = 5f;
    private float currentSpeed;
    private Image ghostIcon;

    private int randomDirNum;
    private int lastDirection = 1;
    private Rigidbody2D rigid;
    private Transform rotation;
    private Image ghostImage;
    private BoxCollider2D thisCollider;

    private Vector3 currentPosition1;

    private Vector3 currentPosition2;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rotation = GetComponent<Transform>();
        ghostImage = GetComponent<Image>();
        rotation.rotation = Quaternion.Euler(0f, 0f, 90f);
        thisCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        rigid.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
    }
    void Update()
    {
        // StartCoroutine("CheckIfStuck");
    }

    // IEnumerator CheckIfStuck()
    // {
    //     currentPosition1 = rotation.position;
    //     yield return new WaitForSeconds(1f);
    //     currentPosition2 = rotation.position;
    //     if (currentPosition1 == currentPosition2)
    //     {
    //         Debug.Log("Stuck");
    //         Vector3 unstuck = new Vector3(2.7476f, 53.7f, 0f);
    //         rotation.position = unstuck;
    //     }
    // }

    void OnCollisionStay2D(Collision2D col)
    {
        do {
            randomDirNum = Random.Range(1, 5);
        } while (randomDirNum == lastDirection);

        if (col.gameObject.tag == "Wall")
        {
            if (randomDirNum == 1)
            {
                rotation.rotation = Quaternion.Euler(0f, 0f, 90f);
                lastDirection = 1;
            } 
            else if (randomDirNum == 2)
            {
                rotation.rotation = Quaternion.Euler(0f, 180f, 0f);
                lastDirection = 2;
            } 
            else if (randomDirNum == 3)
            {
                rotation.rotation = Quaternion.Euler(0f, 0f, 0f);
                lastDirection = 3;
            } 
            else {
                rotation.rotation = Quaternion.Euler(0f, 0f, -90f);
                lastDirection = 4;
            }
        } else if (col.gameObject.tag == "Ghost")
        {
            Physics2D.IgnoreCollision(thisCollider, col.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
