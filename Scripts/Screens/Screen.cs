using DG.Tweening;
using Toolkit.Tweens.Extensions;
using UnityEngine;

namespace Toolkit.Tweens.Screens
{
    public class Screen : AbstractAnimatedScreen
    {
        [SerializeField]
        private InOutTweenBehaviour _tweenBehaviour;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override Tween ShowTween => _tweenBehaviour.PlayIn().AddOnStart(Show);
        public override Tween HideTween => _tweenBehaviour.PlayOut().AddOnComplete(Hide);
    }
}