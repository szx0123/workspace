using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingleMonoBehaviour<EnemyManager> {

    //public Transform[] spawnPoints;
    public float spawnTime = 3f;
    //public GameObject spawnEnemy;
    List<Vector3> spawnPoints;

    private List<EnemyController> m_enemys;
    
    void Start ()
    {
        m_enemys = new List<EnemyController>();
        InvokeRepeating("Spawn", 0, spawnTime);
        spawnPoints = new List<Vector3>();
        spawnPoints.Add(new Vector3(8.38f, -1.53f, 0));
    }

    private void Spawn ()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);

        GameObject _obj = Instantiate(Resources.Load("Prefabs/EnemyAttacked"), spawnPoints[spawnPointIndex], Quaternion.Euler(Vector3.zero)) as GameObject;
        m_enemys.Add(_obj.GetComponent<EnemyController>());
	}

    public void Die(EnemyController _obj)
    {
        m_enemys.Remove(_obj);
    }

    public List<EnemyController> Enemys
    {
        get { return m_enemys; }
    }
}
