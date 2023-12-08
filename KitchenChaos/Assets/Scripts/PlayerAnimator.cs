using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //该脚本用于控制角色动画

    //声明一个Animator类型的变量
    private Animator animator;

    //声明一个String常量，用于存储动画参数IsWalking
    private const string IS_WALKING = "IsWalking";

    //声明一个序列化的私有Player类型变量
    [SerializeField] private Player player;
    private void Awake()
    {
        //获取角色身上的Animator组件
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //设置角色移动动画
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
