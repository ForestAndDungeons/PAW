using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    //[SerializeField] Material _matOriginal;
    //[SerializeField] Material _matTransparency;
    [SerializeField] float _fadeSpeed, _fadeAmount;
    float _originalOpacity;
    Renderer _renderer;
    Material _mat;
    [SerializeField] bool _doFade;
    public bool doFade { set { _doFade = value; } }

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _mat = _renderer.material;
        _originalOpacity = _mat.color.a;
    }

    void Update()
    {
        if(_doFade)
        {
            StartCoroutine(ChangeToTransparent());
            
        }
        else
        {
            ResetFade();
            StartCoroutine(ChangeToOpaque());
        }
    }

    void FadeNow()
    {
        Color currentColor = _mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, _fadeAmount, _fadeSpeed * Time.deltaTime));
        _mat.color = smoothColor;
    }

    void ResetFade()
    {
        Color currentColor = _mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, _originalOpacity, _fadeSpeed * Time.deltaTime));
        _mat.color = smoothColor;
    }

    public IEnumerator ChangeToTransparent()
    {
        _renderer.material.SetFloat("_Mode", 3);
        _renderer.material.SetOverrideTag("RenderType", "Transparent");
        _renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _renderer.material.SetInt("_ZWrite", 0);
        _renderer.material.DisableKeyword("_ALPHATEST_ON");
        _renderer.material.EnableKeyword("_ALPHABLEND_ON");
        _renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _renderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        yield return new WaitForSeconds(0.1f);
        FadeNow();
    }

    public IEnumerator ChangeToOpaque()
    {
        yield return new WaitForSeconds(0.1f);
        _renderer.material.SetFloat("_Mode", 0);
        _renderer.material.SetOverrideTag("RenderType", "");
        _renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        _renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        _renderer.material.SetInt("_ZWrite", 1);
        _renderer.material.DisableKeyword("_ALPHATEST_ON");
        _renderer.material.DisableKeyword("_ALPHABLEND_ON");
        _renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _renderer.material.renderQueue = -1;
    }
}
