using DG.Tweening;
using UnityEngine;

namespace Toolkit.Screens
{
    public abstract class InOutTweenBehaviour : MonoBehaviour, IInOutTween
    {
        public abstract Tween PlayIn();

        public abstract Tween PlayOut();
    }
}