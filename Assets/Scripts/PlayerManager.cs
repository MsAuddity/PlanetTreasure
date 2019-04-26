﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    public int lives;
    public int score;

    private List<GameObject> uniqueCollectibles;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        uniqueCollectibles = new List<GameObject>();
        score = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Unique Pick Up"))
        {
            foreach (GameObject collectible in GameObject.FindGameObjectsWithTag("Unique Pick Up"))
            {
                if (other.gameObject == collectible)
                {
                    Debug.Log("Player obtained a unique item!", collectible);
                    UpdateCollectibles(collectible);
                    collectible.SetActive(false);
                }
            }
        }
        else if (other.gameObject.CompareTag("Pickup"))
        {
            score++;
            other.gameObject.SetActive(false);
        }
    }

    void UpdateCollectibles(GameObject collectible)
    {
        if (collectible == null)
        {
            Debug.LogWarning("Object is null!");
        }
        uniqueCollectibles.Add(collectible);
        score += 10;
        if (uniqueCollectibles.Count >= 5)
        {
            Debug.Log("All collectibles acquired");

        }
    }

    public void OnDamage()
    {
        if (lives <= 0)
        {
            StartCoroutine("OnDeath");
        }
        else lives -= 1;
    }

    private IEnumerator OnDeath()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2.5f);
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);

    }
}