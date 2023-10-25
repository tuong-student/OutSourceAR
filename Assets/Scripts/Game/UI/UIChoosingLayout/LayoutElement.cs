using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.UI;
using NOOD.UI;

public class LayoutElement : MonoBehaviour, IPointerClickHandler
{
    private UIChoseLayout _uIChoseLayout;
    private RectTransform _rect;
    [SerializeField] Canvas _canvas;
    [SerializeField] RectTransform _canvasTransform;
    public Vector2 LocalPosition
    {
        get
        {
            return this.transform.localPosition;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _uIChoseLayout.SetChosenLayoutElement(this);
    }

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _uIChoseLayout = UILoader.GetUI<UIChoseLayout>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
