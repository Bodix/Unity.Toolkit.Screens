// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Toolkit.Screens.Animations
{
    public class CompositeAnimation : InOutTweenBehaviour
    {
        [SerializeField]
        private CompositeAnimationPart[] _tweenBehaviours;

        public override Tween PlayIn()
        {
            Sequence sequence = DOTween.Sequence();

            foreach (CompositeAnimationPart part in _tweenBehaviours)
            {
                Tween tween = part.TweenBehaviour.PlayIn();

                // To prewarm tween initial state.
                tween.ManualUpdate(float.MinValue, float.MinValue);

                sequence.Insert(part.Position, tween);
            }

            return sequence;
        }

        public override Tween PlayOut()
        {
            Sequence sequence = DOTween.Sequence();

            // In reversed order.
            float maxPosition = _tweenBehaviours.Select(x => x.Position).Max();
            foreach (CompositeAnimationPart part in _tweenBehaviours)
            {
                Tween tween = part.TweenBehaviour.PlayOut();

                // To prewarm tween initial state.
                tween.ManualUpdate(float.MinValue, float.MinValue);

                sequence.Insert(maxPosition - part.Position, tween);
            }

            return sequence;
        }
    }
}