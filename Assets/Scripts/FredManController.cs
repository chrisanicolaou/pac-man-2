using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FredManController : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Transform rotation;
    private Renderer colorRenderer;
    private AudioSource playYeehaw;
    private bool isPoweredUp = false;
    private string ghostDestroyed;
    public GameObject ghostPrefab;
    public float speed = 3f;
    // Start is called before the first frame update
    // private bool isNotMoving;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rotation = GetComponent<Transform>();
        colorRenderer = GetComponent<Renderer>();
        playYeehaw = GetComponent<AudioSource>();
        // isNotMoving = false;
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w") // && isNotMoving
        )
        {
            // isNotMoving = false;
            rotation.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a") // && isNotMoving
        )
        {
            // isNotMoving = false;
            rotation.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d") // && isNotMoving
        )
        {
            // isNotMoving = false;
            rotation.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s") // && isNotMoving
        )
        {
            // isNotMoving = false;
            rotation.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ghost" && !isPoweredUp)
        {
            SceneManager.LoadScene("GameOver");
        } else if (col.gameObject.tag == "Ghost" && isPoweredUp)
        {
            Globals.score += 100;
            if (col.gameObject.GetComponent<Image>().color == new Color(0f, 1f, 0f, 1f))
            {
                ghostDestroyed = "GreenGhost";
            } else if (col.gameObject.GetComponent<Image>().color == new Color(1f, 0f, 0f, 1f))
            {
                ghostDestroyed = "RedGhost";
            } else 
            {
                ghostDestroyed = "OrangeGhost";
            }
            Destroy(col.gameObject);
            StartCoroutine("SpawnGhost");
        }
        // isNotMoving = true;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Respawn")
        {
            if (rotation.position.x < 0)
            {
                rotation.position = new Vector2((rotation.position.x * -1) - 0.5f, rotation.position.y);
            }
            else
            {
                rotation.position = new Vector2((rotation.position.x * -1) + 0.5f, rotation.position.y);
            }
        }

        if (col.gameObject.tag == "PowerUp")
        {
            if (colorRenderer.material.color == Color.white)
            {
            Destroy(col.gameObject);
            StartCoroutine("PowerUp");
            }
        }
    }

    void MoveCharacter()
    {
        rigid.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
    }

    IEnumerator PowerUp()
    {
        isPoweredUp = true;
        playYeehaw.Play();
        speed = 5f;
        colorRenderer.material.SetColor("_Color", new Color(255, 0, 0));

        yield return new WaitForSeconds(5f);

        colorRenderer.material.SetColor("_Color", Color.white);
        speed = 3f;
        isPoweredUp = false;
    }

    IEnumerator SpawnGhost()
    {
        var thisGhost = ghostDestroyed;
        yield return new WaitForSeconds(4f);
        GameObject newGhost = Instantiate(ghostPrefab) as GameObject;
        newGhost.transform.SetParent(GameObject.Find("Canvas").transform, false);
        if (thisGhost == "GreenGhost")
        {
            newGhost.GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
        } 
        else if (thisGhost == "RedGhost")
        {
            newGhost.GetComponent<Image>().color = new Color(1f, 0f, 0f, 1f);
        } 
        else 
        {
            newGhost.GetComponent<Image>().color = new Color(1f, 0.5922f, 0f, 1f);
        }
    }
}
