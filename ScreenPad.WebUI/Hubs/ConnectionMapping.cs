using System.Collections.Generic;
using System.Linq;

namespace ScreenPad.WebUI.Hubs
{
    public class ConnectionMapping<T> : IConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (connections)
            {
                HashSet<string> hashSet;
                if (!connections.TryGetValue(key, out hashSet))
                {
                    hashSet = new HashSet<string>();
                    connections.Add(key, hashSet);
                }

                lock (hashSet)
                {
                    hashSet.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> hashSet;
            if (connections.TryGetValue(key, out hashSet))
            {
                return hashSet;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (connections)
            {
                HashSet<string> hashSet;
                if (!connections.TryGetValue(key, out hashSet))
                {
                    return;
                }

                lock (hashSet)
                {
                    hashSet.Remove(connectionId);

                    if (hashSet.Count == 0)
                    {
                        connections.Remove(key);
                    }
                }
            }
        }
    }
}