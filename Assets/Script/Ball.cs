using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce = 400f;
    AudioManager audioManager;

    public GameObject splitPrefab;
    public ParticleSystem particles;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
        audioManager.Play("Land");

        GameObject newsplit = Instantiate(splitPrefab, new Vector3(transform.position.x, other.transform.position.y + 0.19f, transform.position.z), transform.rotation);

        //Ball patches

        newsplit.transform.localScale = Vector3.one * Random.Range(0.7f, 1.3f);
        newsplit.transform.parent = other.transform;


        //Safe & Unsafe segments

        string materialName = other.transform.GetComponent<MeshRenderer> ().material.name;

        if(materialName == "Safe (Instance)") {
            Debug.Log("Safe");
            
        }
        if (materialName == "Unsafe (Instance)") {
            //particles.Play();
            GameManager.gameOver = true;
            //Destroy(this.gameObject);
            audioManager.Play("GameOver");
        }
        if (materialName == "End (Instance)" && !GameManager.levelWin) {
            GameManager.levelWin = true;
            audioManager.Play("LevelWin");
        }
    }
}
