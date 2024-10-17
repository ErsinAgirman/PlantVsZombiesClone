using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_newChapter", menuName = "LevelSpawnSO/ChapterSO", order = 0)]
public class LevelSpawnSO : ScriptableObject 
{
    public SpawnSO[] chapterLevels;
    //Chapterde değişecek olan her şeyin değişkeni burda tutulur.
    //Chapter background, new Zombie Types, new Difficulty etc
}
