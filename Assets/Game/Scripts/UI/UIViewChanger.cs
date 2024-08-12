using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewChanger : MonoBehaviour
{
    [SerializeField] private View[] _viewsTemp;
    private Dictionary<Type, View> _views = new Dictionary<Type, View>();
    [SerializeField] private View _currentView;

    private void Awake()
    {
        InitViewCollection();
        _currentView.Show();
    }

    private void InitViewCollection()
    {
        foreach (var view in _viewsTemp)
        {
            view.Init(this);
            view.Hide();
            _views.Add(view.GetType(),view);
        }
    } 

    public void ShowView<T>()
    {
        if (_currentView != null)
            _currentView.Hide();
        _currentView = _views[typeof(T)];
        _currentView.Show();
    }
}