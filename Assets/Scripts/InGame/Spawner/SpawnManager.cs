using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private LevelSpawnSO chapter;
    private SpawnSO spawnSO;
    [SerializeField] private Slider waveSlider;
    [SerializeField] private Transform[] spawnPoints = new Transform[5];
    [SerializeField] private Transform[] targetPoints = new Transform[5];
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private GameObject waveScreen;
    private bool finalPhase = false;
    private int currentPhase;
    
    public static Action winGame;


    [Header("For Shine Spawn")]
    [SerializeField] Vector2 sunSpawnArea;
    [SerializeField] private float sunFallSpeed;
    [SerializeField] private float sunSpawnTimer;
    [SerializeField] private Transform sunSpawnPoint;
    [SerializeField] private Transform sunTargetPoint;

    [Header("For Zombie Spawn")]
    private int zombieTypeLimit;
    private int currentZombieCount=0;
    private int deadZombie=0;
    
    string zombieTag;
    int spawnIndex;
    Vector3 spawnPosition;
    GameObject zombieInstance;
    private void Start() {
        //StartSpawner();
        int levelIndex =  PlayerPrefs.GetInt("levelIndex");
        if (levelIndex >= chapter.chapterLevels.Length)
        {
            levelIndex=0;
            PlayerPrefs.SetInt("levelIndex", levelIndex);  
        }

        spawnSO = chapter.chapterLevels[levelIndex];
        levelName.text = spawnSO.levelName;
    }
       private void OnEnable() {
      GameManager.gameStartAction += StartSpawner;
      ZombieBase.zombieDead += DeadZombie;
   }
   private void OnDisable() {
      GameManager.gameStartAction -= StartSpawner;
      ZombieBase.zombieDead -= DeadZombie;
   }
   private void StartSpawner()
   {
        zombieTypeLimit = spawnSO.levelSettings[currentPhase].zombieTypes.Count;
        StartCoroutine(FirstSpawn());
   }

   IEnumerator FirstSpawn()
   {
        AudioManager.Instance.PlaySFX("Zombie_Wave_Start_SFX");

        yield return new WaitForSeconds(spawnSO.firstSpawnTimer);
        waveSlider.gameObject.transform.parent.gameObject.SetActive(true);
        ZombieSpawn();
        InvokeRepeating(nameof(ZombieSpawn), spawnSO.spawnTimer, spawnSO.spawnTimer);
        StartCoroutine(PhaseControl(spawnSO.levelSettings[currentPhase].phaseEndTime));
        Invoke(nameof(SunSpawn),sunSpawnTimer);
   }

    private void SunSpawn()
    {
        Vector2 randomPosition = sunSpawnPoint.position;
        randomPosition.x = UnityEngine.Random.Range(sunSpawnArea.x, sunSpawnArea.y);
        GameObject sunInstance = PoolManager.Instance.SpawnFromPool("Shine",randomPosition, Quaternion.identity);
        StartCoroutine(SunFall(randomPosition,sunInstance));
    }

    IEnumerator SunFall( Vector2 randomPoint, GameObject sunInstance)
    {
        Vector2 moveTarget = randomPoint;
        moveTarget.y = sunTargetPoint.position.y;
        while (Mathf.Abs(sunInstance.transform.position.y - moveTarget.y) >= 0.1f)
        {
            if (!sunInstance.activeInHierarchy)
            break;
            sunInstance.transform.position = Vector2.MoveTowards(sunInstance.transform.position, moveTarget, sunFallSpeed *Time.deltaTime);
            yield return null;
        }
        sunInstance.SetActive(false);
        Invoke(nameof(SunSpawn),sunSpawnTimer);
    }

    IEnumerator PhaseControl(float endTime)
    {
        float currentTime = 0f;
        while (currentTime< endTime)
        {
            currentTime += Time.deltaTime;
            waveSlider.value = currentTime / endTime;
            yield return null;
        }
        CancelInvoke(nameof(ZombieSpawn));
        Debug.Log("Chapter " +currentPhase + " Wave");
        AudioManager.Instance.PlaySFX("Wave_SFX");
        waveScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        waveScreen.SetActive(false);
        WaveSpawn(spawnSO.levelSettings[currentPhase].zombieCount);
    }

    private void WaveSpawn(int zombieCount)
    {
        for (int i = 0; i < zombieCount; i++)
        {
           ZombieSpawn(); 
           
        }
        finalPhase = true;
    }

    private void ZombieSpawn()
    {
        AudioManager.Instance.PlaySFX("Zombie_SFX" + UnityEngine.Random.Range(1,5));
        //AudioManager.Instance.PlaySFX("Zombie_SFX1");
        
        zombieTag = spawnSO.levelSettings[currentPhase].zombieTypes[UnityEngine.Random.Range(0,zombieTypeLimit)].ToString();
        spawnIndex = UnityEngine.Random.Range(0,spawnPoints.Length);
        spawnPosition = spawnPoints[spawnIndex].position;
        spawnPosition.x += UnityEngine.Random.Range(-3f, 3f);
        zombieInstance = PoolManager.Instance.SpawnFromPool(zombieTag,spawnPosition, Quaternion.identity);
        zombieInstance.GetComponent<SpriteRenderer>().sortingOrder = spawnIndex;
        zombieInstance.GetComponent<ZombieBase>().SetMainTarget(targetPoints[spawnIndex]);
        currentZombieCount++;
    }

    public void DeadZombie()
    {
        deadZombie++;
        if (finalPhase && currentZombieCount == deadZombie)
        {
            winGame?.Invoke();
            Debug.Log("Oyun Bitti");     
        }
    }
}
