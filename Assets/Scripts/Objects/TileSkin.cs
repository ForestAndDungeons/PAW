using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSkin : MonoBehaviour
{
    [SerializeField] MeshFilter _meshFilter;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Mesh[] _mesh;
    public Mesh[] mesh { get { return _mesh; } }

    [SerializeField] Material[] _material;

    public void ChangeSkin(int n)
    {
        if (n > 0)
        {
            this.gameObject.SetActive(true);
            _meshFilter.mesh = _mesh[n-1];
            _meshRenderer.material = _material[n-1];
        }
        else
            this.gameObject.SetActive(false);
    }
}
