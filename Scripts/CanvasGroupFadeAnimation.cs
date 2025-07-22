using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Toolkit.Screens
{
    public class CanvasGroupFadeAnimation : InOutTweenBehaviour
    {
        public float Alpha = 1f;
        public float Duration = 0.5f;
        public Ease Ease = Ease.Linear;

        [SerializeField, HideIf(nameof(SameGameObjectWithTarget))]
        private CanvasGroup _canvasGroup;

        public CanvasGroup CanvasGroup => _canvasGroup;
        private bool SameGameObjectWithTarget => _canvasGroup && _canvasGroup.gameObject == gameObject;

        private void Awake()
        {
            InitializeIfRequired();
        }

        public override Tween PlayIn()
        {
            InitializeIfRequired();

            return CanvasGroup.DOFade(Alpha, Duration)
                .From(0)
                .SetEase(Ease);
        }

        public override Tween PlayOut()
        {
            InitializeIfRequired();

            return CanvasGroup.DOFade(0, Duration)
                .From(Alpha)
                .SetEase(Ease);
        }

        private void InitializeIfRequired()
        {
            if (!CanvasGroup)
                _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}