using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_newSpawnSettings", menuName = "SpawnSO/SO_SpawnSettings", order = 0)]
public class SpawnSO : ScriptableObject {

    public string levelName;
    public List<LevelSettings> levelSettings = new List<LevelSettings>();
    public float spawnTimer;
    public float firstSpawnTimer;
    
}

[System.Serializable]
public enum ZombieType
{
    NormalZombie,
    FunnelZombie,
    EminemZombie,
}

[System.Serializable]
public class LevelSettings
{
    public int phase;
    public float phaseEndTime;
    public int zombieCount;
    public List<ZombieType> zombieTypes = new List<ZombieType>();

}