using Cronica.UI;
using TMPro;
using UnityEngine;

public class UIPanelTest : UIBase
{
    //组件
    public TextMeshProUGUI txtFrame;
    
    //参数
    public float updateInterval = 0.5f; // 更新频率（秒）
    
    //运行数据
    private float accum = 0f; // FPS累计值
    private int frames = 0; // 帧数累计
    private float timeLeft; // 下次更新的剩余时间
    private float fps = 0f; // 当前FPS值

    private void Start()
    {
        timeLeft = updateInterval;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;
        
        // 间隔时间到，计算FPS
        if (timeLeft <= 0f)
        {
            fps = accum / frames;
            timeLeft = updateInterval;
            accum = 0f;
            frames = 0;
            
            txtFrame.text = "FPS: " + Mathf.Round(fps);
                
            // 可选：根据FPS值改变颜色
            if (fps < 30)
                txtFrame.color = Color.yellow;
            else if (fps < 10)
                txtFrame.color = Color.red;
            else
                txtFrame.color = Color.green;
        }
    }
}
