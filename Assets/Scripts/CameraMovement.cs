using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 0.15f;
    [SerializeField] GameObject player;
    [SerializeField] AestroidSpawner spawner;
    [SerializeField] GameObject controlsText;
    [SerializeField] PlayerMovement playerMovement;

    bool gameStarted = false;
    bool playerAlive = true;

    void Update()
    {
        AutoMovePlayer(player);
        AutoScroll();
        StartCoroutine(GameStart());
    }

    void AutoMovePlayer(GameObject player)
    {
        if (!gameStarted && playerAlive)
        {
            player.transform.position += new Vector3(cameraSpeed, 0, 0);
        }
    }

    IEnumerator GameStart()
    {
        playerMovement.GameStartup = false;
        yield return new WaitForSeconds(5);
        controlsText.SetActive(false);
        spawner.CanSpawnAsteroids = true;
        yield return new WaitForSeconds(3);
        Cursor.lockState = CursorLockMode.Confined;
        gameStarted = true;
    }

    void AutoScroll()
    {
        if (playerAlive)
        {
            gameObject.transform.position += new Vector3(cameraSpeed, 0, 0);
        }
    }

    public void StopMovement()
    {
        playerAlive = false;
    }
}
