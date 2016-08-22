using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

    [SerializeField]
    private float spawnRate;

    [SerializeField]
    private GameObject[] objects;

    [SerializeField]
    private Transform target;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnObject(spawnRate));
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    IEnumerator SpawnObject(float maxSpawnRate)
    {
        
        while (true)
        {
            int randObj = Random.Range(0, objects.Length);
            float rate = Random.Range(.5f, spawnRate);

            yield return new WaitForSeconds(rate);

            var forward = (transform.position - target.position).normalized;
            GameObject obj = (GameObject) Instantiate(objects[randObj], transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce((Vector3.up * Random.Range(400, 500)) + (forward * Random.Range(750, 1000) * -1));

            yield return null;
        }
        

       
    }
}
