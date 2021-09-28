using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadWindowView : AssetBundleViewBase
{
    [SerializeField] private Button _loadAssetButton;
    [SerializeField] private Button _spawnAssetButton;
    [SerializeField] private RectTransform _mountRootTransform;
    [SerializeField] private AssetReference _loadPrefab;

    private List<AsyncOperationHandle<GameObject>> _addresablePrefab = new List<AsyncOperationHandle<GameObject>>();

    private void Start()
    {
        _loadAssetButton.onClick.AddListener(LoadAssets);
        _spawnAssetButton.onClick.AddListener(CreatePrefab);
    }

    private void CreatePrefab()
    {
        var addresablePrefab = Addressables.InstantiateAsync(_loadPrefab, _mountRootTransform);
        _addresablePrefab.Add(addresablePrefab);
    }

    private void OnDestroy()
    {
        _loadAssetButton.onClick.RemoveAllListeners();
        _spawnAssetButton.onClick.RemoveAllListeners();

        foreach(var addresablePrefab in _addresablePrefab)
        {
            Addressables.ReleaseInstance(addresablePrefab);
        }

        _addresablePrefab.Clear();
    }

    private void LoadAssets()
    {
        _loadAssetButton.interactable = false;
        StartCoroutine(DownloadAndSetAssetBundle());
    }
}
