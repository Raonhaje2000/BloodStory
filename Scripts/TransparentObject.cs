// https://daekyoulibrary.tistory.com/entry/Charon-8-%ED%94%8C%EB%A0%88%EC%9D%B4%EC%96%B4%EC%99%80-%EC%B9%B4%EB%A9%94%EB%9D%BC-%EC%82%AC%EC%9D%B4%EC%9D%98-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%ED%88%AC%EB%AA%85%ED%99%94%ED%95%98%EA%B8%B0
// 위 블로그 코드 사용
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    public bool IsTransparent { get; private set; } = false;

    private MeshRenderer[] renderers;
    private WaitForSeconds delay = new WaitForSeconds(0.001f);
    private WaitForSeconds resetDelay = new WaitForSeconds(0.005f);
    private const float THRESHOLD_ALPHA = 0.25f;
    private const float THRESHOLD_MAX_TIMER = 0.5f;

    private bool isReseting = false;
    private float timer = 0f;
    private Coroutine timeCheckCoroutine;
    private Coroutine resetCoroutine;
    private Coroutine becomeTransparentCoroutine;


    void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void BecomeTransparent()
    {
        if (IsTransparent)
        {
            timer = 0f;
            return;
        }

        if (resetCoroutine != null && isReseting)
        {
            isReseting = false;
            IsTransparent = false;
            StopCoroutine(resetCoroutine);
        }

        SetMaterialTransparent();
        IsTransparent = true;
        becomeTransparentCoroutine = StartCoroutine(BecomeTransparentCoroutine());
    }


    #region #Run-time 중에 RenderingMode 바꾸는 메소드들
    /// Runtime 중에 RenderingMode를 바꾸는 방법을 찾아보니, 다음과 같은 코드를 사용한다고 함. <summary>
    // 0 = Opaque, 1 = Cutout, 2 = Fade, 3 = Transparent
    private void SetMaterialRenderingMode(Material material, float mode, int renderQueue)
    {
        material.SetFloat("_Mode", mode);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = renderQueue;
    }

    private void SetMaterialTransparent()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                SetMaterialRenderingMode(material, 3f, 3000);
            }
        }
    }

    private void SetMaterialOpaque()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                SetMaterialRenderingMode(material, 0f, -1);
            }
        }
    }
    #endregion

    public void ResetOriginalTransparent()
    {
        SetMaterialOpaque();
        resetCoroutine = StartCoroutine(ResetOriginalTransparentCoroutine());
    }

    private IEnumerator BecomeTransparentCoroutine()
    {
        while (true)
        {
            bool isComplete = true;

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.color.a > THRESHOLD_ALPHA)
                    isComplete = false;

                Color color = renderers[i].material.color;
                color.a -= Time.deltaTime;
                renderers[i].material.color = color;
            }

            if (isComplete)
            {
                CheckTimer();
                break;
            }

            yield return delay;
        }
    }

    private IEnumerator ResetOriginalTransparentCoroutine()
    {
        IsTransparent = false;

        while (true)
        {
            bool isComplete = true;

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.color.a < 1f)
                    isComplete = false;

                Color color = renderers[i].material.color;
                color.a += Time.deltaTime;
                renderers[i].material.color = color;
            }

            if (isComplete)
            {
                isReseting = false;
                break;
            }

            yield return resetDelay;
        }
    }

    public void CheckTimer()
    {
        if (timeCheckCoroutine != null)
            StopCoroutine(timeCheckCoroutine);
        timeCheckCoroutine = StartCoroutine(CheckTimerCouroutine());
    }

    private IEnumerator CheckTimerCouroutine()
    {
        timer = 0f;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer > THRESHOLD_MAX_TIMER)
            {
                isReseting = true;
                ResetOriginalTransparent();
                break;
            }

            yield return null;
        }
    }
}