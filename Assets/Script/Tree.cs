using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;

public class Tree : MonoBehaviour
{

    [SerializeField] private int health;
    [SerializeField] GameObject logPrefab;


    private GameObject log;
    [HideInInspector] public GameObject spawnedLog;
    private int currentChildIndex = 0;




    Player player;

    public float shakeDuration = 0.5f;
    public float shakeStrength = 0.5f;


    private void Start()
    {

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        transform.GetChild(currentChildIndex).gameObject.SetActive(false);
        currentChildIndex++;


        StartCoroutine(DropLogs());
        ShakeTree();

        if (currentChildIndex == transform.childCount)
        {
            currentChildIndex = 0;
            Destroy(gameObject);


        }

    }

    private void TreeChecker()
    {

    }

    public void ShakeTree()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength);
    }


    private IEnumerator DropLogs()
    {
        for (int i = 0; i < 2; i++)
        {


            log = Instantiate(logPrefab, transform.position, Quaternion.identity);
            log.transform.position = player.GetPosition().position;
            //log.transform.DOMove(player.GetPosition().position, 1).SetEase(Ease.OutCubic).OnComplete(() => { log.transform.SetParent(player.GetPosition()); });

            spawnedLog = log;
            log.transform.DOMoveY(0.5f, 0.5f).SetEase(Ease.OutBounce);

            yield return new WaitForSeconds(0.5f);

        }
    }
}
