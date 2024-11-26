using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDataBinding : MonoBehaviour
{
    public Animator animator;

    public float Speed
    {
        set
        {
            animator.SetFloat(anim_key_speed, value);
        }
    }
    public bool Attack
    {
        set
        {
            if (value)
            {
                animator.SetTrigger(anim_key_attack);
            }
        }
    }
    public bool Dead
    {
        set
        {
            if (value)
            {
                puppetMaster.mode = PuppetMaster.Mode.Active;
                puppetMaster.Kill(stateSettings);
            }
        }
    }
    private int anim_key_speed;
    private int anim_key_attack;
    public PuppetMaster puppetMaster;
    public PuppetMaster.StateSettings stateSettings = PuppetMaster.StateSettings.Default;
    // Start is called before the first frame update
    void Awake()
    {
        anim_key_attack = Animator.StringToHash("Attack");
        anim_key_speed = Animator.StringToHash("Speed");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
