using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMain : MonoBehaviour
{

    public float speed;
    public float leftRightSpeed;
    public float aimSpeed;
    public Animator anim;
    public Rigidbody rigid;
   // public UserInterface ui;


    public float shootDelay = 1f;
    public Transform bulletSpawnPoint;
    public GameObject bullet;

    public EnemyAi[] enemies;

    float T_ShootDelay;

    void Start()
    {
        T_ShootDelay = shootDelay;
    }
    private void OnEnable()
    {
       
    }

    void Update()
    {
        if (T_ShootDelay < shootDelay)
            T_ShootDelay += Time.deltaTime;


        if (T_ShootDelay >= shootDelay && Input.GetMouseButton(0))
        {
            T_ShootDelay = 0;         
        }

        Movement();

    }

    public bool _isMove = true;

    public void GameOver()
    {
        //_isMove = false;
        //ui.LevelFailed();

    }

    bool Rotation()
    {
        if (Input.GetMouseButton(0))
        {
            var r = Input.GetAxis("Mouse X");
            r -= Input.GetAxis("Mouse Y");
            transform.Rotate(0f, r * (aimSpeed * Time.fixedDeltaTime), 0f);

            return true;
        }
        return false;
    }
    void Movement()
    {
        if (!_isMove)
        {
            return;
        }


        var v = rigid.velocity.magnitude;

        if (v > 0) anim.SetBool("IsRun", true);
        else anim.SetBool("IsRun", false);



    }
    void Rotate()
    {
        Vector3 xzDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (xzDirection.magnitude > 0 && xzDirection != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
          Quaternion.LookRotation(xzDirection), leftRightSpeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        if (!_isMove)
        {
            rigid.velocity = Vector3.zero;
            anim.SetBool("IsRun", false);
            return;
        }

        Move();

        if (!Rotation())
            Rotate();
    }

    public void Move()
    {
        // print(Input.GetAxis("Horizontal"));
        rigid.velocity = (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) *
            speed * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {       

        if (other.name.StartsWith("box"))
        {

            UIManagerFinalGame.Instance.AddScore();
            Destroy(other.gameObject,3f);
            other.transform.GetChild(2).gameObject.SetActive(true);
            other.transform.GetChild(1).gameObject.SetActive(false);
            other.enabled = false;
        }

       


    }
}
