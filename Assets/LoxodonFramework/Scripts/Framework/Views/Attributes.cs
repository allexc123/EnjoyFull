﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Loxodon.Framework.Views
{
    public interface IAttributes
    {
        object Get(Type type);

        T Get<T>();

        void Add(Type type, object target);

        void Add<T>(T target);

        object Remove(Type type);

        T Remove<T>();

        IEnumerator GetEnumerator();

    }

    public class Attributes : IAttributes
    {
        private Dictionary<Type, object> attributes = null;

        public virtual void Add(Type type, object target)
        {
            if (this.attributes == null)
                this.attributes = new Dictionary<Type, object>();

            if (type == null || target == null)
                return;

            this.attributes[type] = target;
        }

        public virtual void Add<T>(T target)
        {
            this.Add(typeof(T), target);
        }

        public virtual object Get(Type type)
        {
            if (type == null || this.attributes == null || !this.attributes.ContainsKey(type))
                return null;

            return this.attributes[type];
        }

        public virtual T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public virtual object Remove(Type type)
        {
            if (type == null || this.attributes == null || !this.attributes.ContainsKey(type))
                return null;

            object target = this.attributes[type];
            this.attributes.Remove(type);
            return target;
        }

        public virtual T Remove<T>()
        {
            return (T)this.Remove(typeof(T));
        }

        public virtual IEnumerator GetEnumerator()
        {
            if (this.attributes == null)
                return new EmptyEnumerator();

            return this.attributes.GetEnumerator();
        }

        class EmptyEnumerator : IEnumerator
        {
            public object Current
            {
                get { return null; }
            }

            public bool MoveNext()
            {
                return false;
            }

            public void Reset()
            {
            }
        }
    }
}
