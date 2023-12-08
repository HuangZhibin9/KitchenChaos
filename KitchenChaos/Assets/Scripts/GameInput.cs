using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    //GetDirection() 获取输入的方向
    public Vector3 GetDirection()
    {
        //私有Vector3变量 用于存储角色移动的方向
        Vector3 direction;
        //获取水平轴输入
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //获取垂直轴输入
        float verticalInput = Input.GetAxisRaw("Vertical");
        //创建一个Vector3变量，用于存储角色移动的方向
        direction = new Vector3(horizontalInput, 0, verticalInput);
        //对方向向量进行归一化
        direction = direction.normalized;
        //返回方向向量
        return direction;
    }
}
