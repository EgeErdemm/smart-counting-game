using UnityEngine;

public class PlayerCreate3D : IPlayerCreate3D
{
    private readonly GameObject _playerPrefab;
    private readonly ILevelDataProvider _levelDataProvider;
    private GameObject _currentPlayer;
    private float playerHeight = 0.0f;

    public PlayerCreate3D(GameObject playerPrefab, ILevelDataProvider levelDataProvider)
    {
        _playerPrefab = playerPrefab;
        _levelDataProvider = levelDataProvider;
    }

    public GameObject CreatePlayer()
    {
        // Mevcut varsa önce yok et
        DestroyPlayer();

        Vector3 pos = new Vector3(_levelDataProvider.levelData.startX, playerHeight, _levelDataProvider.levelData.startY);
        _currentPlayer = GameObject.Instantiate(_playerPrefab, pos, Quaternion.identity);
        return _currentPlayer;
    }

    public void DestroyPlayer()
    {
        if (_currentPlayer != null)
        {
            GameObject.Destroy(_currentPlayer);
            _currentPlayer = null;
        }
    }
}
