using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        // FindObjectOfType - looks through the entire program and returns the first scoreboard
        // in this case, only have one score board so it works
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        addRigidBody();
    }

    void addRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            killEnemy();
        }
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        // the parentGameObject var for the vfx entity will be the parentGameObject var that's defined in the beginning of this code
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
    }

    void killEnemy()
    {
        scoreBoard.increaseScore(scorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        // the parentGameObject var for the vfx entity will be the parentGameObject var that's defined in the beginning of this code
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }    
}
