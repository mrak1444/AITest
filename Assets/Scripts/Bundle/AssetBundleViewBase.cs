using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleViewBase : MonoBehaviour
{
    private const string UrlAssetBundleAudio = "https://drive.google.com/uc?export=download&id=19hkheKPEyEGoNav3nCxGucMPdlj9NC4C";
    private const string UrlAssetBundleSprite = "https://drive.google.com/uc?export=download&id=1fiaBpJLVSyPTxCf7ZXsBki7L3gIKNLzz";

    [SerializeField] private DataSpriteBundle[] _dataSpriteBundle;
    [SerializeField] private DataAudioBundle[] _dataAudioBundle;

    private AssetBundle _spriteAssetBundle;
    private AssetBundle _audioAssetBundle;

    protected IEnumerator DownloadAndSetAssetBundle()
    {
        yield return GetSpriteAssetBundle();
        yield return GetAudioAssetBundle();

        if(_spriteAssetBundle == null || _audioAssetBundle == null)
        {
            Debug.LogError("Error");
            yield break;
        }

        SetDownloadAsset();
        yield return null;
    }

    private void SetDownloadAsset()
    {
        foreach(var data in _dataSpriteBundle)
            data.Image.sprite = _spriteAssetBundle.LoadAsset<Sprite>(data.NameAssetBundle);

        foreach (var data in _dataAudioBundle)
        {
            data.AudioSource.clip = _audioAssetBundle.LoadAsset<AudioClip>(data.NameAssetBundle);
            data.AudioSource.Play();
        }
    }

    private IEnumerator GetAudioAssetBundle()
    {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleAudio);
        yield return request.SendWebRequest();
        while (!request.isDone)
            yield return null;

        StateRequest(request, ref _audioAssetBundle);
    }

    private IEnumerator GetSpriteAssetBundle()
    {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleSprite);
        yield return request.SendWebRequest();
        while (!request.isDone)
            yield return null;

        StateRequest(request, ref _spriteAssetBundle);
    }

    private void StateRequest(UnityWebRequest request, ref AssetBundle assetBundle)
    {
        if(request.error == null)
        {
            assetBundle = DownloadHandlerAssetBundle.GetContent(request);
            Debug.Log("COMPLETE");
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
}
