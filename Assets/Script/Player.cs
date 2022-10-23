using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text stepTexts;
    [SerializeField] ParticleSystem dieParticles;
    [SerializeField] public AudioSource jumpSound;
    [SerializeField] public AudioSource hitSound;
    [SerializeField] public AudioSource deadSound;
    [SerializeField,Range(0.01f,1f)] float movedur=0.2f;
    [SerializeField,Range(0.01f,1f)] float jumpheight=0.5f;
    [SerializeField] bool gamemodestomp;
   
    private float backDistance;
    private float extent;
    private float backBoundary;
    private float leftBoundary;
    private float rightBoundary;
    [SerializeField] private int maxTravel;
    public int MaxTravel {get => maxTravel;}
     [SerializeField] private int currentTravel;
    public int CurrentTravel { get => currentTravel;}
    public bool IsDie {get => this.enabled == false;}
    

    // void Start()
    // {
    //     if (gamemodestomp)
    //         scalebeast();
            
    // }

    public void SetUp(int backDistance, int extent)
    {
        backBoundary = backDistance;
        leftBoundary = -(extent+1);
        rightBoundary = (extent+1);
    }

    void Update()
    {
        var MoveDir = Vector3.zero;
        if(Input.GetKey(KeyCode.UpArrow)||Input.GetKey("w"))
        {
            
            MoveDir += new Vector3(0,0,1);
        }
        if(Input.GetKey(KeyCode.DownArrow)||Input.GetKey("s"))
        {
            MoveDir += new Vector3(0,0,-1);
        }
         if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey("d"))
        {
            MoveDir += new Vector3(1,0,0);
        }
         if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey("a"))
        {
            MoveDir += new Vector3(-1,0,0);
        }
        if (MoveDir == Vector3.zero)
            return;
            
        if(IsJumping() == false)
        {
            MoveJump(MoveDir);
            jumpSound.Play();
        }
        
    }
    private void UpdateTravel()
    {
        currentTravel = (int) this.transform.position.z;
        if (currentTravel > maxTravel)
            maxTravel = currentTravel;
        stepTexts.text = "Langkah: " + maxTravel.ToString();
    }
    private void MoveJump(Vector3 targetDirection)
    {
        var TargetPosition = transform.position + targetDirection;
        transform.LookAt(TargetPosition);

        var MoveSeq = DOTween.Sequence(transform);
        MoveSeq.Append(transform.DOMoveY(jumpheight,movedur/2));
        MoveSeq.Append(transform.DOMoveY(0,movedur/2));
        if(Tree.AllPositions.Contains(TargetPosition))
            return;
        if(TargetPosition.z < backBoundary || TargetPosition.x < leftBoundary||TargetPosition.x > rightBoundary)
            return;
        transform.DOMoveX(TargetPosition.x,movedur);
        transform.DOMoveZ(TargetPosition.z,movedur).OnComplete(UpdateTravel);
        
    }
    private void Jumping()
    {

    }
    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        var boat = other.GetComponent<Boat>();
        if (boat!=null)
            boat.Stop();
        else
        {
            AnimateKelelep();
            hitSound.Play();
        }
        if (gamemodestomp)
        {
            other.GetComponent<Car>().Stop();
        }
        if(!gamemodestomp)
        {
            var car = other.GetComponent<Car>();
            if(car != null)
            {
                AnimateKeLindes(car);
                deadSound.Play();
            }
        }
        
    }
    private void AnimateKelelep()
    {
        transform.DORotate(new Vector3(0,180f,0),10.0f);
        transform.DOMoveY(-0.45f,0.01f);

    }
    private void AnimateKeLindes(Car car)
    {
        transform.DOMoveY(0.45f,0.01f);
        transform.DOScaleX(3.0f,0.01f);
        transform.DOScaleY(0.15f,0.01f);
        transform.DOScaleZ(2.0f,0.01f);
        this.enabled = false;
    }
    private void scalebeast()
    {
        transform.DOScaleX(10.0f,0.01f);
        transform.DOScaleY(10.0f,0.01f);
        transform.DOScaleZ(10.0f,0.01f);
        transform.DOMoveY(-1.5f,0.01f);
    }
    
 
}
