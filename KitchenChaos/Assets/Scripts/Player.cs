using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    //序列化的私有float变量 移动速度
    [SerializeField]
    private float moveSpeed = 3.5f;
    //序列化的私有float变量 旋转速度
    [SerializeField]
    private float rotateSpeed = 3.5f;
    //序列化的私有GameInput类型变量 获取输入
    [SerializeField] private GameInput gameInput;
    //私有Vector3变量 用于存储角色移动的方向
    private Vector3 direction;


    //声明一个私有布尔类型变量，用于存储角色是否在移动
    private bool isWalking;

    private void Update()
    {
        //调用gameInput.GetDirection()方法
        direction = gameInput.GetDirection();
        //如果方向向量不为0,则将isWalking设置为true
        isWalking = direction != Vector3.zero;
    }
    private void FixedUpdate()
    {
        //调用PositionUpdate()方法
        PositionAndRotationUpdate();
    }




    //PositionAndRotationUpdate() WASD控制角色在地面上进行前后左右移动,WASD是世界坐标绝对方向
    public void PositionAndRotationUpdate()
    {
        if (direction == Vector3.zero)
        {
            return;
        }
        //角色移动
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        //角色旋转
        transform.forward = Vector3.Slerp(transform.forward, direction, rotateSpeed * Time.deltaTime);
    }

    //公有方法，IsWalking() 返回角色是否在移动
    public bool IsWalking()
    {
        return isWalking;
    }

}
