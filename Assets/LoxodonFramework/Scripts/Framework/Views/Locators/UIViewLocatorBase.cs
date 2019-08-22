﻿/*
 * MIT License
 *
 * Copyright (c) 2018 Clark Yang
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in 
 * the Software without restriction, including without limitation the rights to 
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
 * of the Software, and to permit persons to whom the Software is furnished to do so, 
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
 * SOFTWARE.
 */

using Loxodon.Framework.Asynchronous;

namespace Loxodon.Framework.Views
{
    public abstract class UIViewLocatorBase : IUIViewLocator
    {
        public virtual IView LoadView(string name)
        {
            return LoadView<IView>(name);
        }

        public abstract T LoadView<T>(string name) where T : IView;

        public virtual Window LoadWindow(string name)
        {
            return LoadWindow<Window>(name);
        }

        public abstract T LoadWindow<T>(string name) where T : Window;

        public virtual Window LoadWindow(IWindowManager windowManager, string name)
        {
            return LoadWindow<Window>(windowManager, name);
        }

        public abstract T LoadWindow<T>(IWindowManager windowManager, string name) where T : Window;


        public virtual IProgressTask<float, IView> LoadViewAsync(string name)
        {
            return LoadViewAsync<IView>(name);
        }

        public abstract IProgressTask<float, T> LoadViewAsync<T>(string name) where T : IView;

        public virtual IProgressTask<float, Window> LoadWindowAsync(string name)
        {
            return LoadWindowAsync<Window>(name);
        }

        public abstract IProgressTask<float, T> LoadWindowAsync<T>(string name) where T : Window;

        public virtual IProgressTask<float, Window> LoadWindowAsync(IWindowManager windowManager, string name)
        {
            return LoadWindowAsync<Window>(windowManager, name);
        }

        public abstract IProgressTask<float, T> LoadWindowAsync<T>(IWindowManager windowManager, string name) where T : Window;
    }
}
