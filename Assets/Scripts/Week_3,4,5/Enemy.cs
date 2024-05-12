using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform enemy;
    [SerializeField] float speed = 2f;
    [SerializeField] float speedMax = 2.5f;
    void Start()
    {
        pointA.GetComponent<SpriteRenderer>().enabled = false;
        pointB.GetComponent<SpriteRenderer>().enabled = false;

        speed = Random.Range(speed, speedMax);

        Patrol();
    }


    void Patrol()
    {
        enemy.DOLocalMoveX(pointA.localPosition.x, speed).SetEase(Ease.Linear).OnComplete(() => {

            Vector3 myScale = enemy.localScale;
            myScale.x *= -1;
            enemy.localScale = myScale;



            enemy.DOLocalMoveX(pointB.localPosition.x, speed).SetEase(Ease.Linear).OnComplete(() =>
            {

                Vector3 myScale = enemy.localScale;
                myScale.x *= -1;
                enemy.localScale = myScale;

                Patrol();

            });

        });
    }
   
}
