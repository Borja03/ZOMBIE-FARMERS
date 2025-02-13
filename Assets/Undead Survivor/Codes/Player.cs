﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private int DAMAGE = 0;
    private int HEAL = 1;

    public Vector2 inputVec;
    public float speed;
    public float rotateSpeed; // Rotation speed for shovel
    public float curHp = 100;
    public float maxHp = 100;

    Rigidbody2D rigid;
    public SpriteRenderer spriter;
    Animator anim;
    public Slider hpBar;

    public GameObject[] bulletObj0;
    public int[] WeaponLevel = { 0, 0, 0, 0, 0, 0 };
    public float[] WeaponTimer = { 0, 0, 0, 0, 0, 0 };
    public float[] WeaponShotDelay = { 0, 1, 1, 0.3f, 0.1f, 0.4f };
    public float[] WeaponShotSpead = { 0, 3, 0, 10, 20, 7 };

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento con WASD
        inputVec.x = 0;
        inputVec.y = 0;

        if (Keyboard.current.wKey.isPressed)
        {
            inputVec.y = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            inputVec.y = -1;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            inputVec.x = -1;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            inputVec.x = 1;
        }

        inputVec.Normalize();  // Normaliza el vector para que no se mueva más rápido diagonalmente

        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, -0.8f, 0));
        hpBar.value = curHp / maxHp;

        for (int i = 1; i <= 5; i++)
            if (WeaponLevel[i] != 0)
                Fire(i);
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void UpdateHpBar(int type, float value_)
    {
        if (type == DAMAGE)
        {
            curHp -= value_;
            hpBar.value = curHp / maxHp;
        }
        else if (type == HEAL)
        {
            curHp += value_;
            hpBar.value = curHp / maxHp;
        }
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    Vector3 nearestEnemyDirection()
    {
        float nearestEnemyDistance = Mathf.Infinity;
        Vector3 nearestEnemyDirection = Vector3.up;
        for (int i = 0; i < 2; i++)  //enemy => index 0~2 bullet => index 3~5
        {
            for (int j = 0; j < GameManager.instance.pool.pools[i].Count; j++)
            {
                if (GameManager.instance.pool.pools[i][j].activeSelf == true)
                {
                    float playerToEnemy = Vector3.Distance(transform.position, GameManager.instance.pool.pools[i][j].transform.position);
                    if (nearestEnemyDistance > playerToEnemy)
                    {
                        nearestEnemyDistance = playerToEnemy;
                        nearestEnemyDirection = (GameManager.instance.pool.pools[i][j].transform.position - transform.position) / playerToEnemy;
                    }
                }
            }
        }
        return nearestEnemyDirection;
    }

    void Fire(int WeaponNum)
    {
        WeaponTimer[WeaponNum] += Time.deltaTime;

        if (WeaponTimer[WeaponNum] > WeaponShotDelay[WeaponNum] / WeaponLevel[WeaponNum])
        {
            // Initiate first bullet
            Vector3 FireDirection = nearestEnemyDirection();
            Rigidbody2D rigid;
            WeaponTimer[WeaponNum] = 0;
            GameObject bullet;
            if (WeaponNum == 2)
            {
                bullet = MakeBullet(FireDirection, WeaponNum, transform.position + FireDirection);
                rigid = bullet.GetComponent<Rigidbody2D>();
            }
            else
            {
                bullet = MakeBullet(FireDirection, WeaponNum, transform.position);
                rigid = bullet.GetComponent<Rigidbody2D>();
            }

            switch (WeaponNum)
            {
                case 1: //Fork
                    Vector3 ForkFireDirection = new Vector3(Random.Range(-1, 2), Random.Range(2, 4), 0);
                    rigid.AddForce(ForkFireDirection * WeaponShotSpead[WeaponNum], ForceMode2D.Impulse);
                    break;

                case 2: //Scythe
                    break;

                case 3: // Pistol
                    rigid.AddForce(FireDirection * (WeaponShotSpead[WeaponNum] + WeaponLevel[WeaponNum]), ForceMode2D.Impulse);
                    break;

                case 4: // AR
                    if (WeaponLevel[4] == 2)
                        WeaponShotSpead[4] = 22;
                    else if (WeaponLevel[4] == 3)
                        WeaponShotSpead[4] = 25;

                    rigid.AddForce(FireDirection * (WeaponShotSpead[WeaponNum] + 2 * WeaponLevel[WeaponNum]), ForceMode2D.Impulse);
                    break;

                case 5: // Shotgun
                    rigid.AddForce(FireDirection * (WeaponShotSpead[WeaponNum] + WeaponLevel[WeaponNum]), ForceMode2D.Impulse);
                    float FireAngle = DirectionToAngle(FireDirection);

                    FireAngle += Mathf.PI / 6;
                    FireDirection = new Vector3(Mathf.Cos(FireAngle), Mathf.Sin(FireAngle), 0);
                    GameObject bullet1 = MakeBullet(FireDirection, WeaponNum, transform.position);
                    Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
                    rigid1.AddForce(FireDirection * (WeaponShotSpead[WeaponNum] + WeaponLevel[WeaponNum]), ForceMode2D.Impulse);

                    FireAngle -= Mathf.PI / 3;
                    FireDirection = new Vector3(Mathf.Cos(FireAngle), Mathf.Sin(FireAngle), 0);
                    GameObject bullet2 = MakeBullet(FireDirection, WeaponNum, transform.position);
                    Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
                    rigid2.AddForce(FireDirection * (WeaponShotSpead[WeaponNum] + WeaponLevel[WeaponNum]), ForceMode2D.Impulse);
                    break;
            }
        }
    }

    float DirectionToAngle(Vector3 Direction)
    {
        float Angle;
        if (Direction.y >= 0)
        {
            Angle = Mathf.Acos(Direction.x);
        }
        else
        {
            Angle = -Mathf.Acos(Direction.x);
        }
        return Angle;
    }

    GameObject MakeBullet(Vector3 FireDirection, int WeaponNum, Vector3 BulletPos)
    {
        GameObject bullet = GameManager.instance.pool.Get(WeaponNum + 4);
        bullet.transform.position = BulletPos;
        bullet.transform.localEulerAngles = new Vector3(0, 0, (180 * DirectionToAngle(FireDirection) / Mathf.PI) - 90);
        return bullet;
    }

    void OnHit(float dmg)
    {
        UpdateHpBar(DAMAGE, dmg);
        if (curHp <= 0)
        {
            anim.SetBool("Dead", true);
            speed = 0;
            Invoke("Dead", 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            OnHit(enemy.attackDamage);
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
        GetComponent<PlayerGameOver>().GameOver();
    }
}
