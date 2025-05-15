[System.Serializable]
public class LevelData
{
    public int level;
    public int optimalPath;
    public int targetScore;
    public int startX;
    public int startY;
    public int[] grid;
    public int gridWidth;
    public int gridHeight;

    public string comment; // Yorum için ekledik
    public bool isBlind;
    public int totalTime;
}
