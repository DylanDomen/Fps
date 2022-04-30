using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollectItem : MonoBehaviour
{
	public enum CollectibleTypes {Heal}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;

	public GameObject Player;
	public bool makeHeartsSpawn = true;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (makeHeartsSpawn)
		{
			makeHeartsSpawn = false;
			spawnHearts(3);
		}

		if (rotate)
			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Collect();
		}
	}

	public void Collect()
	{
		if (collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if (collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);


		if (CollectibleType == CollectibleTypes.Heal)
		{
			Player.GetComponent<PlayerInfo>().heal(1);
		}
		Destroy(gameObject);
	}

	void spawnHearts(int numberOfHearts)
	{
		for (int i = 0; i < numberOfHearts; i++)
		{
			Vector3 randomSpawnPosition = new Vector3(Random.Range(10, 20), 1, Random.Range(-11, -1));
			Instantiate(this.gameObject, randomSpawnPosition, Quaternion.identity);
			this.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}
}
