using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    //获取InputActionAsset
    private PlayerInputAction playerInputController;
    //声明一个OnInteract事件，当玩家按下交互键时触发该事件
    public event EventHandler OnInteractAction;
    //声明一个OnInteract事件，当玩家按下第二交互键时触发该事件
    public event EventHandler OnInteractAltmateAction;

    //Awake时实例化InputActionAsset
    private void Awake()
    {
        //获取InputActionAsset
        playerInputController = new PlayerInputAction();

        //为playerInputAction的Interact事件添加监听
        playerInputController.Player.Interact.performed += context =>
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        };
        playerInputController.Player.InteractAltmate.performed += context =>
        {
            OnInteractAltmateAction?.Invoke(this, EventArgs.Empty);
        };
    }
    //Enable时启用InputActionAsset
    private void OnEnable()
    {
        playerInputController.Enable();
    }
    //Disable时禁用InputActionAsset
    private void OnDisable()
    {
        playerInputController.Disable();
    }

    //GetDirection() 获取输入的方向
    public Vector3 GetDirection()
    {
        //私有Vector3变量 用于存储角色移动的方向
        Vector3 direction;
        //获取输入的方向
        direction.x = playerInputController.Player.Move.ReadValue<Vector2>().x;
        direction.z = playerInputController.Player.Move.ReadValue<Vector2>().y;
        direction.y = 0;
        //对方向向量进行归一化
        direction = direction.normalized;
        //返回方向向量
        return direction;
    }
}
