using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NodeCommandCenter.Caching
{
	public class Cache<K, V>
	{
		private readonly int _capacity;
		private readonly ConcurrentDictionary<K, V> _map = new ConcurrentDictionary<K, V>();

		public Cache(int capacity)
		{
			if (capacity < 1)
			{
				throw new ArgumentException("Capacity must be greater than zero.");
			}
			_capacity = capacity;
		}

		public V Get(K key)
		{
			V node;
			if (_map.TryGetValue(key, out node))
			{
				return node;
			}
			return default(V);
		}

		public void Set(K key, V value)
		{
			// Look for cache key.
			V node;
			if (!_map.TryGetValue(key, out node))
			{
				_map.TryAdd(key, value);
			}
		}

		public List<V> GetAllValues()
		{
			List<V> response = new List<V>();
            foreach (var key in _map.Keys)
            {
				V node;
				if (_map.TryGetValue(key, out node))
				{
					response.Add(node);
				}
			}

			return response;
		}
	}
}
