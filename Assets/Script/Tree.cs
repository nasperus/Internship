using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class Tree : MonoBehaviour, IAttackable
{
    public static Tree instance;

    [SerializeField] private int health;
    [SerializeField] private GameObject logPrefab;
    [SerializeField] Transform backPosition;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 0.5f;
    private int currentChildIndex = 0;


    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {

        backPosition = Player.instance.PlayerBack;

    }


    //Tree take damage
    public void Damage(int damage)
    {
        health -= damage;
        transform.GetChild(currentChildIndex).gameObject.SetActive(false);
        currentChildIndex++;

        StartCoroutine(DropLogs());
        ShakeTree();

        if (currentChildIndex == transform.childCount)
        {

            SpawnTrees.instance.counter--;
            currentChildIndex = 0;
            Destroy(gameObject);

        }

    }


    // Tree shaking animation
    public void ShakeTree()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength);
    }


    //Spawn logs and the player picked them up
    private IEnumerator DropLogs()
    {
        for (int i = 0; i < 2; i++)
        {

            GameObject log = Instantiate(logPrefab, transform.position, Quaternion.identity);
            log.transform.DOMove(backPosition.position, 1).SetEase(Ease.OutBack).OnComplete(() => { log.transform.SetParent(backPosition); });
            yield return new WaitForSeconds(0.5f);

        }
    }
}
