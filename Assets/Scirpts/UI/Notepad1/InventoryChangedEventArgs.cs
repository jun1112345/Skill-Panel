using System;

// 1. 定义操作类型的枚举
public enum InventoryChangeType
{
    Add,
    Remove
}

// 2. 定义自定义的事件参数类
public class InventoryChangedEventArgs : EventArgs
{
    public InventoryChangeType ChangeType { get; }

    // 构造函数，方便创建时赋值
    public InventoryChangedEventArgs(InventoryChangeType type)
    {
        ChangeType = type;
    }
}