using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //将该物体设置为单例

    #region 单例
    public static Player Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are more than one Player in the scene!");
        }
    }
    #endregion

    //声明一个事件，当SelectedCounter改变时触发该事件
    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedArgs : EventArgs
    {
        public BaseCounter _selectedCounter;
    }


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
    //声明一个私有BaseCounter类型变量，用于存储检测到的柜台
    private BaseCounter selectedCounter;
    //手上的物体
    private KitchenObject kitchenObject;
    //手拿东西的位置
    [SerializeField] private Transform playerHandPoint;


    private void Start()
    {
        //为OnInteractAction事件添加监听
        gameInput.OnInteractAction += OnGameInputInteract;
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
            if (hit.collider.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                //如果检测到的物体有BaseCounter组件，则将selectedCounter设置为检测到的柜台
                SetSelectedCounter(baseCounter);
            }
            else
            {
                //如果检测到的物体没有BaseCounter组件，则将selectedCounter设置为null
                SetSelectedCounter(null);
            }
        }
        else
        {
            //如果没有检测到物体
            SetSelectedCounter(null);
        }
    }
    //设置SelectedCounter
    public void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        //当SelectedCounter改变时，触发OnSelectedCounterChanged事件
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs { _selectedCounter = selectedCounter });
    }

    //当OnteractAction事件触发时，调用OnGameInputInteract()方法，完成与柜台的交互
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
            if (hit.collider.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                //如果检测到的物体有ClearCounter组件，则调用Interact()方法
                baseCounter.Interact(this);
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

    //获取柜台上的物体
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    //设置柜台上的物体
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    //是否已经有物体
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    //清除柜台上的物体
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    //获取柜台的counterTopPoint
    public Transform GetThekitchenFollowTransform()
    {
        return playerHandPoint;
    }
}
