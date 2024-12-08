using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerSlider : MonoBehaviour
{
    public Slider timerSlider; // 연결할 Slider
    public float duration = 60f; // 타이머 시간 (1분)
    private float elapsedTime = 0f;

    void Start()
    {
        // Slider 초기화
        timerSlider.maxValue = duration;
        timerSlider.value = duration;

        // 3초 후에 타이머 시작
        StartCoroutine(StartTimerAfterDelay(5f));
    }

    IEnumerator StartTimerAfterDelay(float delay)
    {
        // 3초 대기
        yield return new WaitForSeconds(delay);

        // 타이머 시작
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // 경과 시간 계산
            timerSlider.value = duration - elapsedTime; // 남은 시간 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        TimerEnd(); // 타이머 종료 처리
    }

    void TimerEnd()
    {
        Debug.Log("타이머 종료!");
    }
}
