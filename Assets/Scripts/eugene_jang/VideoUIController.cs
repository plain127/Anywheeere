using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoUIController : MonoBehaviour
{
    public RawImage rawImage;  // Raw Image를 통한 비디오 출력
    public VideoPlayer videoPlayer;  // Video Player 컴포넌트

    void Start()
    {
        // 비디오 플레이어 준비 완료 시 이벤트 등록
        videoPlayer.prepareCompleted += OnVideoPrepared;

        // 비디오 준비 및 재생
        PrepareVideo();
    }

    void PrepareVideo()
    {
        // 비디오 플레이어 준비 시작
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // 비디오 플레이어가 준비되었을 때 비디오를 재생하고 Raw Image에 텍스처 설정
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    void Update()
    {
        // 비디오가 재생 중이고, 텍스처가 설정되지 않았다면 설정
        if (videoPlayer.isPrepared && rawImage.texture == null)
        {
            rawImage.texture = videoPlayer.texture;
        }
    }
}
