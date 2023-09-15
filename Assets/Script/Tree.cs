using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tree : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] GameObject logPrefab;
    private int currentChildIndex = 0;
    public Vector3 spawnRange = new Vector3(10f, 0f, 10f);


    public float shakeDuration = 0.2f;
    public float shakeStrength = 0.1f;


    public void TakeDamage(int damage)
    {
        health -= damage;
        transform.GetChild(currentChildIndex).gameObject.SetActive(false);
        currentChildIndex++;


        StartCoroutine(DropLogs());
        ShakeTree();

        if (currentChildIndex == transform.childCount)
        {
            Destroy(gameObject);
        }

    }




    public void ShakeTree()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength);
    }


    private IEnumerator DropLogs()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject log = Instantiate(logPrefab, transform.position, Quaternion.identity);
            log.transform.DOMoveY(0.5f, 0.5f).SetEase(Ease.OutBounce);

            yield return new WaitForSeconds(0.5f);

        }
    }
}
