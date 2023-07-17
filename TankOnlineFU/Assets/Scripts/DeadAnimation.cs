using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimation : MonoBehaviour
{
	[SerializeField]
	private Sprite[] _explosions;
	[SerializeField]
	private SpriteRenderer render;
	void Start()
	{
		StartCoroutine(OnDead());
	}

	private IEnumerator OnDead()
	{
		for (int i = 0; i < _explosions.Length; i++)
		{
			render.sprite = _explosions[i];
			yield return new WaitForSecondsRealtime(0.15f);
		}
		Destroy(gameObject);
	}
}
