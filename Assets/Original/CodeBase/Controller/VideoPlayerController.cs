using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;
using TMPro;

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer;    
    [SerializeField] private TextMeshProUGUI _videoTitle;   
    [SerializeField] private Button _playButton;          
    [SerializeField] private Button _stopButton;          

    private string _currentVideoUrl;

    private void Start()
    {
        // Назначение функций кнопкам
        _playButton.onClick.AddListener(PlayVideo);
        _stopButton.onClick.AddListener(StopVideo);
        _stopButton.gameObject.SetActive(false);

        SetVideo(VideoDatabase.Videos[0].videoUrl, VideoDatabase.Videos[0].title);
    }

    public void PlayVideo()
    {
        if (!_mediaPlayer.Control.IsPlaying())
        {
            _mediaPlayer.Play();
            _playButton.gameObject.SetActive(false);
            _stopButton.gameObject.SetActive(true);
        }
    }

    public void StopVideo()
    {
        if (_mediaPlayer.Control.IsPlaying())
        {
            _mediaPlayer.Pause();
            _stopButton.gameObject.SetActive(false);
            _playButton.gameObject.SetActive(true);
        }
    }

    public void SetVideo(string url, string title)
    {
        if (_currentVideoUrl == url) return;

        _currentVideoUrl = url;
        _videoTitle.text = title;

        _mediaPlayer.OpenMedia(MediaPathType.AbsolutePathOrURL, url, false);
        _mediaPlayer.Pause();
        _stopButton.gameObject.SetActive(false);
        _playButton.gameObject.SetActive(true);
    }
}
