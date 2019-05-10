using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDatas
{
    private Vector3 _spawnPoint;
    private Vector3 _finishPoint;
    private int _gameDuration;
    public MapDatas(int mapNumber)
    {
        switch (mapNumber)
        {
            case 1:
                _spawnPoint = new Vector3(-20, 40, -1200);
                _finishPoint = new Vector3(-20, 40, -1000);
                _gameDuration = 15;
                break;
            case 2:
                _spawnPoint = new Vector3(-4800, 40, 0);
                _finishPoint = new Vector3(-4600, 40, 0);
                _gameDuration = 15;
                break;
            default:
                throw new System.Exception("Not a valid map number.");
                
            
        }
    }

    public Vector3 GetSpawnPoint()
    {
        return _spawnPoint;
    }

    public int GetGameDuration()
    {
        return _gameDuration;
    }

    public Vector3 GetFinishPoint()
    {
        return _finishPoint;
    }

}
