using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public enum States
{
    Walk,
    PlayerFollow,
    ShootPlayer,
    IsDead
}

public class EnemyAi : MonoBehaviour
{
   

    public States state = States.Walk;
    public NavMeshAgent agent;
    public Animator animController;
    public EnemyMoveTargets targets;
    public float targetDistance;
    public float targetShootDistance;
    public float SpeedNormal;
    public float SpeedFollow;
    public Transform target;

    public float shootDelay = 1f;
    public Transform bulletSpawnPoint;
    public GameObject bullet;
    public int health = 100;
    public bool IsShootorFollow = false;
    public bool IsCapturePlayer = false;

    public void ChangeState(States state)
    {
        this.state = state;
    } 


  

    void Awake()
    {
        target = GameObject.Find("Player").transform;
       
    }




    public void ChangeState()
    {
       

        if (state == States.IsDead) return;

        var d = (Vector3.Distance(agent.transform.position, target.position));

        if (d < targetDistance && d > targetShootDistance)
        {
            CheckOldState();
            state = States.PlayerFollow;

        }
        else if (d < targetShootDistance)
        {
            if(IsShootorFollow)
            state = States.ShootPlayer;
            else
                state = States.PlayerFollow;

        }
        else
        {
            CheckOldState();
            state = States.Walk;
        }
    }

    void CheckOldState()
    {
        if (state == States.ShootPlayer)
        {
            agent.enabled = true;
            agent.isStopped = false;
            animController.SetBool("Action", false);
           
        }
    }


    public bool IsDead
    {
        get { return PlayerPrefs.GetInt("enemyHealth" + transform.parent.gameObject.name, 0) == 0 ? false : true; }
        set
        {
            PlayerPrefs.SetInt("enemyHealth" + transform.parent.gameObject.name, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }





   


    float T_ShootDelay;
    void Start()
    {
        T_ShootDelay = shootDelay;
    }

    private void OnEnable()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        if (state == States.IsDead)
        {
            return;
        }

        CheckStateFollow();
        CheckStateWalk();
        CheckStateShoot();

        ChangeState();


    }

    void CheckStateWalk()
    {
        if (state == States.Walk)
            Walk();
    }

    void CheckStateFollow()
    {
        if (state == States.PlayerFollow)
            PlayerFollow();
    }

    void CheckStateShoot()
    {
        //if (state == States.ShootPlayer)          
        //    Shoot();
           

    }

    void Walk()
    {
        agent.SetDestination(targets.GetCurrentTargetPoint().position);
        agent.speed = SpeedNormal;
        agent.stoppingDistance = 0f;

        if (GetPathRemainingDistance(agent) < .1f)
            targets.NextTarget();

        animController.SetBool("IsRun", true);
    }


    float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
            return 1f;

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }

        return distance;
    }

  

    void PlayerFollow()
    {      


        agent.SetDestination(target.position);
        agent.speed = SpeedFollow;
        agent.stoppingDistance = 1f;
        animController.SetBool("IsRun", false);

        if (!IsCapturePlayer) return;

        if (Mathf.Abs( Vector3.Distance(transform.position,target.position)) < .9f )
        {
            UIManagerFinalGame.Instance.ShowFailPanel();
        }
      

    }


   


   
   
}
