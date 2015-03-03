using System.Collections.Generic;

namespace ScreenPad.WebUI.Hubs
{
    public interface IConnectionMapping<in T>
    {
        int Count { get; }
        void Add(T key, string connectionId);
        IEnumerable<string> GetConnections(T key);
        void Remove(T key, string connectionId);
    }
}
