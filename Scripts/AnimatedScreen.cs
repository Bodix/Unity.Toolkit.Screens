﻿using DG.Tweening;

namespace Toolkit.Screens
{
    public abstract class AnimatedScreen : Screen
    {
        public abstract Tween ShowTween { get; }

        public abstract Tween HideTween { get; }
        
        public override Tween Push()
        {
            return ScreenStack.Push(this);
        }

        public override Tween Pop()
        {
            return ScreenStack.Pop(this);
        }
        
        public new Tween PushImmediately()
        {
            return ScreenStack.PushImmediately(this);
        }
        
        public new Tween PopImmediately()
        {
            return ScreenStack.PopImmediately(this);
        }
    }
}