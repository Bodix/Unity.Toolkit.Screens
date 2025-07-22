// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using DG.Tweening;
using Evolutex.Evolunity.Extensions;
using Toolkit.Screens.Extensions;

namespace Toolkit.Screens.Screens
{
    public static class ScreenStack
    {
        private static readonly Stack<Screen> stack = new Stack<Screen>(3);
        private static Tween transition;

        private static Screen CurrentScreen => stack.IsEmpty() ? null : stack.Peek();

        public static Tween Push(Screen screen)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.PrependCallback(() => ValidatePushTween(sequence, screen));
            foreach (Screen otherScreen in stack)
            {
                if (otherScreen.IsEnabled)
                    if (otherScreen is AnimatedScreen otherAnimatedScreen)
                        sequence.Append(otherAnimatedScreen.HideTween);
                    else sequence.AppendCallback(otherScreen.Hide);
            }
            sequence.OnComplete(() =>
            {
                screen.Show();
                stack.Push(screen);
            });

            return sequence;
        }

        public static Tween Push(AnimatedScreen screen)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.PrependCallback(() => ValidatePushTween(sequence, screen));
            foreach (Screen otherScreen in stack)
            {
                if (otherScreen.IsEnabled)
                    if (otherScreen is AnimatedScreen otherAnimatedScreen)
                        sequence.Append(otherAnimatedScreen.HideTween);
                    else sequence.AppendCallback(otherScreen.Hide);
            }
            sequence.Append(screen.ShowTween
                .AddOnComplete(() => stack.Push(screen)));

            return sequence;
        }

        public static Tween Pop(Screen screen)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.PrependCallback(() => ValidatePopTween(sequence, screen));
            sequence.AppendCallback(() =>
            {
                screen.Hide();
                stack.Pop();
            });
            Screen nextScreen = GetNextScreen();
            if (nextScreen != null)
                if (nextScreen is AnimatedScreen nextAnimatedScreen)
                    sequence.Append(nextAnimatedScreen.ShowTween);
                else sequence.AppendCallback(nextScreen.Show);

            return sequence;
        }

        public static Tween Pop(AnimatedScreen screen)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.PrependCallback(() => ValidatePopTween(sequence, screen));
            sequence.Append(screen.HideTween
                .AddOnComplete(() => stack.Pop()));
            Screen nextScreen = GetNextScreen();
            if (nextScreen != null)
                if (nextScreen is AnimatedScreen nextAnimatedScreen)
                    sequence.Append(nextAnimatedScreen.ShowTween);
                else sequence.AppendCallback(nextScreen.Show);

            return sequence;
        }

        public static void PushImmediately(Screen screen)
        {
            CheckPushForExceptions(screen);

            foreach (Screen otherScreen in stack)
                if (otherScreen.IsEnabled)
                    otherScreen.Hide();

            screen.Show();
            stack.Push(screen);
        }

        public static Tween PushImmediately(AnimatedScreen screen)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.PrependCallback(() => ValidatePushTween(sequence, screen));
            sequence.Append(screen.ShowTween
                .AddOnStart(() =>
                {
                    foreach (Screen otherScreen in stack)
                        if (otherScreen.IsEnabled)
                            otherScreen.Hide();
                })
                .AddOnComplete(() => stack.Push(screen)));

            return sequence;
        }

        public static void PopImmediately(Screen screen)
        {
            CheckPopForExceptions(screen);

            screen.Hide();
            stack.Pop();

            CurrentScreen?.Show();
        }

        public static Tween PopImmediately(AnimatedScreen screen)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.PrependCallback(() => ValidatePopTween(sequence, screen));
            sequence.Append(screen.HideTween
                .AddOnComplete(() =>
                {
                    stack.Pop();

                    CurrentScreen?.Show();
                }));

            return sequence;
        }

        public static void Clear()
        {
            stack.Clear();
        }

        private static void CheckPushForExceptions(Screen screen)
        {
            if (stack.Contains(screen))
                if (CurrentScreen == screen)
                    throw new InvalidOperationException("Failed to show the screen that is already shown");
                else
                    throw new InvalidOperationException(
                        "Failed to show the screen that is already shown under the current screen");
        }

        private static void ValidatePushTween(Tween tween, Screen screen)
        {
            try
            {
                if (transition == null || !transition.IsActive() || transition.IsComplete())
                    transition = tween;
                if (transition != tween)
                    throw new InvalidOperationException("Failed to show the screen during the transition");

                CheckPushForExceptions(screen);
            }
            catch (InvalidOperationException)
            {
                tween.Kill();

                throw;
            }
        }

        private static void CheckPopForExceptions(Screen screen)
        {
            if (screen != CurrentScreen)
                if (stack.Contains(screen))
                    throw new InvalidOperationException(
                        "Failed to hide the screen that is under the current screen");
                else
                    throw new InvalidOperationException(
                        "Failed to hide the screen that is not managed by screen stack");
        }

        private static void ValidatePopTween(Tween tween, Screen screen)
        {
            try
            {
                if (transition == null || !transition.IsActive() || transition.IsComplete())
                    transition = tween;
                if (transition != tween)
                    throw new InvalidOperationException("Failed to hide the screen during the transition");

                CheckPopForExceptions(screen);
            }
            catch (InvalidOperationException)
            {
                tween.Kill();

                throw;
            }
        }

        private static Screen GetNextScreen()
        {
            Screen screen = CurrentScreen;
            if (CurrentScreen == null)
                return null;
            
            stack.Pop();
            Screen nextScreen = CurrentScreen;
            stack.Push(screen);

            return nextScreen;
        }
    }
}