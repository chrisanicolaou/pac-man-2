using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnFoodEat : MonoBehaviour
{
    public AudioSource munch;
    private Image coinImg;
    private CircleCollider2D coinColl;

    void Start()
    {
        munch = GetComponent<AudioSource>();
        coinImg = GetComponent<Image>();
        coinColl = GetComponent<CircleCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
        munch.Play(0);
        Globals.score += 10;
        this.coinImg.enabled = false;
        this.coinColl.enabled = false;
        Destroy(gameObject, 1f);
        }
    }
}
