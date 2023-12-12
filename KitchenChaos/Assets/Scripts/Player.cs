using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [Header("玩家移动参数")]
    [SerializeField] private float playerHeight;
    [SerializeField] private float playerRedius;
    [Header("玩家移动参数")]
    //序列化的私有GameInput类型变量 获取输入
    [SerializeField] private GameInput gameInput;
    //序列化的私有float变量 移动速度
    [SerializeField] private float moveSpeed = 3.5f;
    //序列化的私有float变量 旋转速度
    [SerializeField] private float rotateSpeed = 3.5f;
    //声明一个私有布尔类型变量，用于存储角色是否在移动
    private bool isWalking;
    //声明一个私有Vector3类型变量，用于存储角色上一次交互的方向，这样即使停下也能检测前面的柜台
    private Vector3 lastDirection;
    //声明一个用于射线交互的LayerMask
    [SerializeField] private LayerMask layerMask;


    private void Start()
    {
        //为OnInteractAction事件添加监听
        gameInput.OnInteractAction += OnGameInputInteract;
    }

    private void OnGameInputInteract(object sender, EventArgs e)
    {
        //获取角色前进的方向
        Vector3 direction = gameInput.GetDirection();
        //可交互的最大距离
        float maxDistance = 1.5f;
        //如果玩家在移动，即direction不为0
        if (direction != Vector3.zero)
        {
            //将lastDirection设置为direction
            lastDirection = direction;
        }

        //射线检测前方是否有物体
        if (Physics.Raycast(transform.position, lastDirection, out RaycastHit hit, maxDistance, layerMask))
        {
            if (hit.collider.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
            {
                //如果检测到的物体有ClearCounter组件，则调用Interact()方法
                clearCounter.Interact();
            }
        }
    }

    private void Update()
    {
        //调用HandleInteraction()方法
        HandleInteraction();

    }
    private void FixedUpdate()
    {
        PositionAndRotationUpdate();
    }

    //HandleInteraction() 解决与柜台的交互
    public void HandleInteraction()
    {
        //获取角色前进的方向
        Vector3 direction = gameInput.GetDirection();
        //可交互的最大距离
        float maxDistance = 1.5f;
        //如果玩家在移动，即direction不为0
        if (direction != Vector3.zero)
        {
            //将lastDirection设置为direction
            lastDirection = direction;
        }

        //射线检测前方是否有物体
        if (Physics.Raycast(transform.position, lastDirection, out RaycastHit hit, maxDistance, layerMask))
        {
            if (hit.collider.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
            {
                //如果检测到的物体有ClearCounter组件，则调用Interact()方法
                //clearCounter.Interact();
            }
        }

    }

    //PositionAndRotationUpdate() WASD控制角色在地面上进行前后左右移动,WASD是世界坐标绝对方向
    public void PositionAndRotationUpdate()
    {
        //私有Vector3变量 用于存储角色移动的方向
        Vector3 direction;
        //调用gameInput.GetDirection()方法
        direction = gameInput.GetDirection();
        //如果方向向量不为0,则将isWalking设置为true
        isWalking = direction != Vector3.zero;
        //射线的最大距离
        float maxDistance = 0.2f;

        if (direction == Vector3.zero)
        {
            return;
        }
        //碰撞检测
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRedius, direction, maxDistance);
        if (!canMove)
        {
            //检测x轴分量是否会发生碰撞
            Vector3 xDir = new Vector3(direction.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRedius, xDir, maxDistance);
            if (canMove)
            {
                direction = xDir;
            }
            else
            {
                //检测z轴分量是否会发生碰撞
                Vector3 zDir = new Vector3(0, 0, direction.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRedius, zDir, maxDistance);
                if (canMove)
                {
                    direction = zDir;
                }
            }
        }
        if (canMove)
        {
            //角色移动
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            //角色旋转
            transform.forward = Vector3.Slerp(transform.forward, gameInput.GetDirection(), rotateSpeed * Time.deltaTime);
        }
    }

    //公有方法，IsWalking() 返回角色是否在移动
    public bool IsWalking()
    {
        return isWalking;
    }

}
