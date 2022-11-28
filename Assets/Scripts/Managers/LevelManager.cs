using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> {

    [SerializeField] Transform levelContainer;
    [Header("Pieces")]
    [SerializeField] GameObject startPiece;
    [SerializeField] GameObject endPiece;
    [SerializeField] List<GameObject> pieces;

    List<GameObject> _spawnedPieces = new List<GameObject>();
    int _currentIndex = 1;

    public void CreateLevel() {
        CleanLevel();
        SpawnPieces();
    }

    public int GetLevel() {
        return _currentIndex;
    }

    public void CleanLevel() {
        foreach (var piece in _spawnedPieces) {
            Destroy(piece);
        }
        _spawnedPieces.Clear();
    }

    void SpawnPieces() {
        SpawnStartPiece();
        for (int i = 0; i < _currentIndex; i++) {
            SpawnMidPiece();
        }
        SpawnEndPiece();
        _currentIndex++;
    }

    void SpawnStartPiece() {
        var spawned = Instantiate(startPiece, levelContainer);
        _spawnedPieces.Add(spawned);
    }

    void SpawnMidPiece() {
        var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
        var spawned = Instantiate(pieces[Random.Range(0, pieces.Count)], levelContainer);
        spawned.transform.position = Vector3.up * (lastPiece.transform.position.y + 10);
        _spawnedPieces.Add(spawned);
    }

    void SpawnEndPiece() {
        var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
        var spawned = Instantiate(endPiece, levelContainer);
        spawned.transform.position = Vector3.up * (lastPiece.transform.position.y + 10);
        _spawnedPieces.Add(spawned);
    }

}
