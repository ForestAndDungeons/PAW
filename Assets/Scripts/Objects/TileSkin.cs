using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSkin : MonoBehaviour
{
    [SerializeField] MeshFilter _meshFilter;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Mesh[] _mesh;
    [SerializeField] Material[] _material;

    [SerializeField] GameObject[] _torch;

    /*void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }*/

    public void ChangeSkin(int n, bool b)
    {
        if (n > 0)
        {
            this.gameObject.SetActive(true);
            _meshFilter.mesh = _mesh[n-1];
            _meshRenderer.material = _material[n-1];

            if (_torch.Length > 0)
                foreach (GameObject torch in _torch)
                    torch.SetActive(b);
        }
        else
            this.gameObject.SetActive(false);
    }
}
