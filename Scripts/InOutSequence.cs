using DG.Tweening;
using Evolutex.Evolunity.Extensions;
using UnityEngine;

namespace Toolkit.Screens
{
    public class InOutSequence : InOutTweenBehaviour
    {
        [SerializeField]
        private InOutTweenBehaviour[] _tweenBehaviours;

        public override Tween PlayIn()
        {
            Sequence sequence = DOTween.Sequence();

            foreach (InOutTweenBehaviour behaviour in _tweenBehaviours)
                sequence.Append(behaviour.PlayIn());

            return sequence;
        }

        public override Tween PlayOut()
        {
            Sequence sequence = DOTween.Sequence();

            // In reversed order.
            foreach (InOutTweenBehaviour behaviour in _tweenBehaviours.Reverse())
                sequence.Append(behaviour.PlayOut());

            return sequence;
        }
    }
}