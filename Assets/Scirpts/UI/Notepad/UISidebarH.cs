using UnityEngine;
using DG.Tweening;  // 别忘了导入 DOTween 命名空间

public class UISidebarH : MonoBehaviour
{
    [Header("折叠参数")]
    [SerializeField] private float expandedHeight = 400f;   // 展开宽度
    [SerializeField] private float collapsedHeight = 60f;   // 折叠后的宽度（只留图标/小条）
    [SerializeField] private float animationDuration = 0.4f;

    private RectTransform rectTransform;
    private bool isExpanded = false;  // 当前是否展开

    // 按钮上的图片（可选：用于显示箭头方向变化）
    [SerializeField] private RectTransform buttonIcon;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // 确保初始宽度正确
        // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, expandedWidth);
    }

    // 给按钮的 OnClick 绑定的公共方法
    public void ToggleFold()
    {
        isExpanded = !isExpanded;
        float targetHeight = isExpanded ? expandedHeight : collapsedHeight;

        // 停止任何正在进行的动画，防止冲突
        rectTransform.DOKill();

        // 宽度动画 + 弹性缓动
        rectTransform
            .DOSizeDelta(new Vector2(rectTransform.sizeDelta.x, targetHeight), animationDuration)
            .SetEase(Ease.OutBack);  // OutBack 会带一点"过冲"弹性效果

        // 可选：更新按钮文字方向
        if (buttonIcon != null)
        {
            //buttonIcon.rotation.z = isExpanded ? 0 : 90;
            if (isExpanded)
            {
                buttonIcon.Rotate(0f, 0f, 180f);
            }
            else
            {
                buttonIcon.Rotate(0f, 0f, -180f);
            }
            
        }
    }
}
