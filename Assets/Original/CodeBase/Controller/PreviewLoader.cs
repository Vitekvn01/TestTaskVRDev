using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using TMPro;

public class PreviewLoader : MonoBehaviour
{
    [SerializeField] private VideoPlayerController _playerController;
    [SerializeField] private GameObject _previewButtonPrefab;
    [SerializeField] private Transform _previewContentPanel;

    private static Dictionary<string, Texture2D> _textureCache = new Dictionary<string, Texture2D>(); 

    private void Start()
    {
        foreach (var video in VideoDatabase.Videos)
        {
            StartCoroutine(LoadPreview(video));
        }
    }

    private IEnumerator LoadPreview(VideoData video)
    {
        GameObject buttonPreview = Instantiate(_previewButtonPrefab, _previewContentPanel);
        Button button = buttonPreview.GetComponent<Button>();
        Image previewImage = buttonPreview.GetComponent<Image>();
        TextMeshProUGUI titleText = buttonPreview.GetComponentInChildren<TextMeshProUGUI>();
        LoadingCircle loadingCircle = buttonPreview.GetComponentInChildren<LoadingCircle>();

        titleText.text = video.title;

        if (_textureCache.ContainsKey(video.previewUrl))
        {
            Texture2D cachedTexture = _textureCache[video.previewUrl];
            previewImage.sprite = Sprite.Create(cachedTexture, new Rect(0, 0, cachedTexture.width, cachedTexture.height), Vector2.zero);
        }
        else
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(video.previewUrl);
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);

                    _textureCache[video.previewUrl] = texture;

                    previewImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                    loadingCircle.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogError($"Failed load {video.title} error:{request.error}");
                }
            }
        }

        button.onClick.AddListener(() =>  _playerController.SetVideo(video.videoUrl, video.title));
  
    }
}
