using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBinding : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public float Speed
    {
        set
        {
            //animator.SetFloat(key_anim_speed, value);
        }
    }

    public Vector3 MoveDir
    {
        set
        {
            animator.SetFloat(key_anim_X, value.x);

            animator.SetFloat(key_anim_Y, value.z);
        }
    }

    private int key_anim_X;
    private int key_anim_Y;
    // Start is called before the first frame update
    void Start()
    {
        key_anim_X = Animator.StringToHash("X");
        key_anim_Y = Animator.StringToHash("Y");
        WeaponControl.instance.OnChangeWeapon.AddListener(OnChangeWeapon);
    }
    public void PlayShowWP()
    {
        animator.Play("Draw", 0, 0);
    }
    public void PlayFireWP()
    {
        animator.Play("Fire", 1, 0);
    }
    private void OnChangeWeapon(WeaponBehaviour wp)
    {
        animator.runtimeAnimatorController = wp.overrideController;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
