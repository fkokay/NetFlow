using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetFlow.Domain.Common;
using NetFlow.Domain.Firms;
using NetFlow.Domain.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Common
{
    public sealed class CachedFileSqlProvider : ISqlProvider, IDisposable
    {
        private readonly string _sqlPath;
        private readonly ConcurrentDictionary<string, string> _cache = new();
        private readonly FileSystemWatcher _watcher;

        public CachedFileSqlProvider(string sqlPath)
        {
            _sqlPath = sqlPath;
            _watcher = new FileSystemWatcher(sqlPath, "*.sql");
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _watcher.Changed += Invalidate;
            _watcher.Created += Invalidate;
            _watcher.Deleted += Invalidate;
            _watcher.Renamed += Invalidate;
            _watcher.EnableRaisingEvents = true;
        }

        private void Invalidate(object? sender, FileSystemEventArgs e)
        {
            _cache.Clear();
        }

        public string Get(string name)
        {
            return _cache.GetOrAdd(name, _ =>
            {
                var defaultFile = Path.Combine(_sqlPath, name);
                if (!File.Exists(defaultFile))
                    throw new Exception($"SQL not found: {name}");
                return File.ReadAllText(defaultFile);
            });
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}
