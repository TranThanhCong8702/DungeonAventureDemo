using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerMovementType2 : MonoBehaviour
{
    Vector2 MoveInput;
    Vector2 MoveInput1;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float NormalmoveSpeed = 1f;
    [SerializeField] float ShootingmoveSpeed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float CountDownToStart = 2f;

    [SerializeField] TextMeshProUGUI Shooting;
    [SerializeField] TextMeshProUGUI SuperSpeed;
    [SerializeField] TextMeshProUGUI Slow;

    Rigidbody2D playerRb;
    bool isAlive;
    Animator animator;
    ParticleSystemOlay particle;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;
    EdgeCollider2D edgeCollider;
    AudioSource audioPlayer;
    GameControlType2 gameControl;
    Shooter shoot;
    EvilWizard ev;
    //Meteor mete;
    int pressCount = 0;
    private void Awake()
    {
        gameControl = FindObjectOfType<GameControlType2>();
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        particle = FindObjectOfType<ParticleSystemOlay>();
        shoot = GetComponent<Shooter>();
        ev = FindObjectOfType<EvilWizard>();
    }
    public CapsuleCollider2D GetCapsuleCollider()
    {
        return capsuleCollider;
    }
    void Start()
    {
        Starting();
        moveSpeed = NormalmoveSpeed;
    }

    void PlayerState()
    {
        if(!shoot.IsFiring && moveSpeed == ShootingmoveSpeed)
        {
            Slow.gameObject.SetActive(true);
            Shooting.gameObject.SetActive(false);
            SuperSpeed.gameObject.SetActive(false);
        }
        else if(!shoot.IsFiring && moveSpeed == NormalmoveSpeed)
        {
            Slow.gameObject.SetActive(false);
            Shooting.gameObject.SetActive(false);
            SuperSpeed.gameObject.SetActive(true);
        }
        else
        {
            Slow.gameObject.SetActive(false);
            Shooting.gameObject.SetActive(true);
            SuperSpeed.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        PlayerState();
        if (!ev.BossAlive) return;
        if (!isAlive) { shoot.IsFiring = false; return; }
        SetAlive();
        InstantDeath();
    }
    void FixedUpdate()
    {
        if (!isAlive) { return; }
        Run();
        Jumping();
    }
    public void Die()
    {
        audioPlayer.Play();
        isAlive = false;
        animator.SetBool("IsAlive", isAlive);
    }
    public void Starting()
    {
        isAlive = true;
        animator.SetBool("IsAlive", isAlive);
    }
    public bool GetPlayerStatus()
    {
        return isAlive;
    }
    private void SetAlive()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            particle.ParticlePlay();
            FindObjectOfType<GameControlType2>().PlayerProcess();
        }
    }
    private void InstantDeath()
    {
        if (edgeCollider.IsTouchingLayers(LayerMask.GetMask("Camera")))
        {
            //particle.ParticlePlay();
            Debug.Log("Camera");
            gameControl.InstantDeath();
            FindObjectOfType<GameControlType2>().PlayerProcess();
        }
    }
    void Run()
    {
        Running();
    }
    void Running()
    {
        Vector2 move = new Vector2(MoveInput1.x * moveSpeed, playerRb.velocity.y);
        playerRb.velocity = move;
        bool isRunning;
        if (isAlive)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
            animator.SetBool("IsAlive", isAlive);
        }
        animator.SetBool("IsRunning", isRunning);
    }
    void OnJump(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
    void OnMove(InputValue val)
    {
        MoveInput1 = val.Get<Vector2>();
        Debug.Log(MoveInput1);
    }
    public void Shock()
    {
        shoot.IsFiring = false;
        moveSpeed = NormalmoveSpeed;
        pressCount = 0;
    }
    void OnFire(InputValue val)
    {
        if (val.isPressed)
        {
            if(pressCount == 0)
            {
                shoot.IsFiring = true;
                moveSpeed = ShootingmoveSpeed;
                pressCount++;
            }
            else
            {
                shoot.IsFiring = false;
                moveSpeed = NormalmoveSpeed;
                pressCount = 0;
            }
        }
    }
    private void Jumping()
    {
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))) return;
        if (!isAlive) { return; }
        if (MoveInput.y > 0)
        {
            playerRb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    public float GetCountDown()
    {
        return CountDownToStart;
    }
}
