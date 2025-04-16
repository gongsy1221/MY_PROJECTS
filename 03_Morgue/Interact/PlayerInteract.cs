using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private Transform _cam;
    private PlayerUI _playerUI;
    private int _interactMask;

    [SerializeField] private float _distance = 3;

    private Material outline;

    private MeshRenderer renderers;
    private List<Material> materialList = new List<Material>();

    private void Awake()
    {
        _cam = Camera.main.transform;
        _interactMask = LayerMask.NameToLayer("Interact");
        _playerUI = GetComponent<PlayerUI>();
        //outline = new Material(Shader.Find("Draw/OutlineShader"));
    }

    void Update()
    {
        _playerUI.UpdateText(string.Empty);
        Ray _ray = new Ray(_cam.transform.position, _cam.transform.forward);
        RaycastHit _hitInfo;
        if (Physics.Raycast(_ray, out _hitInfo, _distance, 1 << _interactMask))
        {
            if (_hitInfo.collider.GetComponent<Interactable>() != null)
            {
                //renderers = _hitInfo.collider.GetComponent<MeshRenderer>();

                //materialList.Clear();
                //materialList.AddRange(renderers.sharedMaterials);
                //materialList.Add(outline);

                //renderers.materials = materialList.ToArray();

                Interactable _interactable = _hitInfo.collider.GetComponent<Interactable>();
                _playerUI.UpdateText(_interactable.promptMessage);
                if(Input.GetKeyDown(KeyCode.E))
                    _interactable.BassInteract();
            }
            else
            {
                //MeshRenderer renderer = _hitInfo.collider.GetComponent<MeshRenderer>();

                //materialList.Clear();
                //materialList.AddRange(renderer.sharedMaterials);
                //materialList.Remove(outline);

                //renderer.materials = materialList.ToArray();
            }
        }
    }
}
