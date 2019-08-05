﻿using System;
using System.Collections.Generic;

namespace Loxodon.Framework.Views
{
    public class Bundle : IBundle
    {
        protected IDictionary<string, object> data = new Dictionary<string, object>();

        public Bundle()
        {
        }

        public Bundle(IBundle bundle)
        {
            PutAll(bundle);
        }

        public virtual int Count
        {
            get { return this.data.Count; }
        }

        public virtual IDictionary<string, object> Data
        {
            get { return this.data; }
        }

        public virtual ICollection<string> Keys
        {
            get { return this.data.Keys; }
        }

        public virtual ICollection<object> Values
        {
            get { return this.data.Values; }
        }

        public virtual void Clear()
        {
            this.data.Clear();
        }

        public virtual bool ContainsKey(string key)
        {
            return this.data.ContainsKey(key);
        }

        public virtual bool Remove(string key)
        {
            return this.data.Remove(key);
        }

        public virtual T Get<T>(string key) where T : new()
        {
            return Get(key, default(T));
        }

        public virtual T Get<T>(string key, T defaultValue) where T : new()
        {
            object value;
            if (this.data.TryGetValue(key, out value))
                return (T)value;

            return defaultValue;
        }

        public virtual void Put<T>(string key, T value)
        {
            if (!IsValidType(value))
                throw new ArgumentException("Value must be serializable!");

            this.data[key] = value;
        }

        public virtual void PutAll(IBundle bundle)
        {
            foreach (KeyValuePair<string, object> kv in bundle.Data)
            {
                if (!IsValidType(kv.Value))
                    throw new ArgumentException("Value must be serializable!");

                this.data[kv.Key] = kv.Value;
            }
        }

        protected virtual bool IsValidType(object value)
        {
            return true;
        }
    }
}
