using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Wave = 1;
    public Transform SpawnPoint;
    public Transform DistPoint;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyPrefab;
    public float SecondsRest = 30;
    public int HP = 100;
    public GameObject GameOverOverlay;
    public Text TextHp;
    public Text TextTimer;
    private float Timer = 0;
    private bool RestTime = true;
    private bool gameAlive = true;

    private void Start()
    {
        ChangeTextHP();
    }

    void Update()
    {
        if (RestTime && gameAlive)
        {
            Timer += Time.deltaTime;
            ChangeTextTimer();
            if (Timer > SecondsRest) DoWave();
        }
    }

    public void DoWave()
    {
        RestTime = false;
        Timer = 0;
        for (int i = 0; i < Wave; i++)
        {
            GameObject enemyGO = Instantiate(enemyPrefab, SpawnPoint.position, Quaternion.identity);
            var enemy = enemyGO.GetComponent<Enemy>();
            enemy.SetGameManager(this);
            enemy.SetPoint(DistPoint);
            enemies.Add(enemyGO);
        }
        Wave++;
    }

    public void RemoveEnemy(GameObject enemy, bool hpRemain = false)
    {
        enemies.Remove(enemy);
        if (hpRemain)
        {
            HP -= 3;
            ChangeTextHP();
        }
        if (HP < 1) GameOver();
        if (enemies.Count() == 0)
        {
            RestTime = true;
        }
    }

    private void GameOver()
    {
        gameAlive = false;
        GameOverOverlay.SetActive(true);
    }

    private void ChangeTextHP()
    {
        TextHp.text = "HP: " + HP.ToString();
    }

    private void ChangeTextTimer()
    {
        TextTimer.text = (SecondsRest - Timer).ToString();
    }
}
