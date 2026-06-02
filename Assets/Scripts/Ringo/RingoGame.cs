using TMPro;
using UnityEngine;

public class RingoGame : MonoBehaviour
{
    public static RingoGame Instance { get; private set; }

    private int _points;
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        GameStart();
    }
    public void GameStart()
    {
        _points = 0;
        pointsText.text = "0";
    }

    public void GetPoints(int points)
    {
        _points += points;
        pointsText.text = _points.ToString();
    }
}
