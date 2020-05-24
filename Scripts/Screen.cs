using UnityEngine;
using DG.Tweening;

namespace Toolkit.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        public virtual bool IsEnabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public abstract void Show();

        public abstract void Hide();
        
        public virtual Tween Push()
        {
            return ScreenStack.Push(this);
        }

        public virtual Tween Pop()
        {
            return ScreenStack.Pop(this);
        }
        
        public void PushImmediately()
        {
            ScreenStack.PushImmediately(this);
        }
        
        public void PopImmediately()
        {
            ScreenStack.PopImmediately(this);
        }
    }
}