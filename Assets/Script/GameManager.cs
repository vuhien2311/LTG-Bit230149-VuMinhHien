using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Dùng cho Image và Button cơ bản

public class ColorMatchGame : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image targetColorDisplay; // Cái ô hiển thị màu đề bài
    [SerializeField] private Button btnRed;
    [SerializeField] private Button btnGreen;
    [SerializeField] private Button btnBlue;

    [Header("Game Settings")]
    [SerializeField] private float timeLimit = 3.0f; // Thời gian tối đa để chọn

    // Định nghĩa enum cho dễ quản lý
    private enum GameColor { Red, Green, Blue }
    private GameColor currentTargetColor;
    private float timer;
    private bool isGameActive = true;

    private void Start()
    {
        // Gán sự kiện cho các nút bấm
        btnRed.onClick.AddListener(() => OnPlayerInput(GameColor.Red));
        btnGreen.onClick.AddListener(() => OnPlayerInput(GameColor.Green));
        btnBlue.onClick.AddListener(() => OnPlayerInput(GameColor.Blue));

        // Nhuộm màu cho các nút (để người chơi nhận biết)
        btnRed.GetComponent<Image>().color = Color.red;
        btnGreen.GetComponent<Image>().color = Color.green;
        btnBlue.GetComponent<Image>().color = Color.blue;

        NextRound();
    }

    private void Update()
    {
        if (!isGameActive) return;

        // Đếm ngược thời gian
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Hết giờ -> Sinh ra Warning Log
            Debug.LogWarning($"[TIMEOUT] Bạn đã quá chậm! Màu cần chọn là: {currentTargetColor}");
            NextRound();
        }
    }

    // Hàm chuyển sang lượt mới
    private void NextRound()
    {
        timer = timeLimit;
        
        // Random màu (0 = Red, 1 = Green, 2 = Blue)
        int randomColorIndex = Random.Range(0, 3);
        currentTargetColor = (GameColor)randomColorIndex;

        // Cập nhật hiển thị màu sắc cho ô đề bài
        switch (currentTargetColor)
        {
            case GameColor.Red:
                targetColorDisplay.color = Color.red;
                break;
            case GameColor.Green:
                targetColorDisplay.color = Color.green;
                break;
            case GameColor.Blue:
                targetColorDisplay.color = Color.blue;
                break;
        }

        // Log ra console để báo lượt mới (Log thường)
        Debug.Log($"--- Bắt đầu lượt mới. Hãy chọn màu: {currentTargetColor} ---");
    }

    // Hàm xử lý khi người chơi bấm nút
    private void OnPlayerInput(GameColor inputColor)
    {
        if (inputColor == currentTargetColor)
        {
            // ĐÚNG -> Log thường (Info)
            Debug.Log($"[SUCCESS] Chính xác! Bạn đã chọn đúng màu {inputColor}.");
        }
        else
        {
            // SAI -> Log Lỗi (Error)
            // Cái này sẽ hiện màu ĐỎ trên Console của bạn
            Debug.LogError($"[FAIL] Sai rồi! Đề là {currentTargetColor} nhưng bạn chọn {inputColor}.");
        }

        // Chuyển ngay sang lượt sau
        NextRound();
    }
}