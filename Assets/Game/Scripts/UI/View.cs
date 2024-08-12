using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected UIViewChanger _viewChanger;

    public void Init(UIViewChanger viewChanger)
    {
        _viewChanger = viewChanger;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}