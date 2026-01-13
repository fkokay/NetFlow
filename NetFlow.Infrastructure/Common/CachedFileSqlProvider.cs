using NetFlow.Domain.Common;
using NetFlow.Domain.Firms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Common
{
    public sealed class CachedFileSqlProvider : ISqlProvider, IDisposable
    {
        private readonly string _sqlPath;
        private readonly ICurrentFirmProvider _firm;
        private readonly ConcurrentDictionary<string, string> _cache = new();
        private readonly FileSystemWatcher _watcher;

        public CachedFileSqlProvider(string sqlPath, ICurrentFirmProvider firm)
        {
            _sqlPath = sqlPath;
            _firm = firm;
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
            var firm = _firm.GetFirmCode();
            var key = $"{firm}:{name}";
            return _cache.GetOrAdd(key, _ =>
            {
                var firmFile = Path.Combine(_sqlPath, InsertFirm(name, firm));
                if (File.Exists(firmFile))
                    return File.ReadAllText(firmFile);

                var defaultFile = Path.Combine(_sqlPath, name);
                if (!File.Exists(defaultFile))
                    throw new Exception($"SQL not found: {name}");

                return File.ReadAllText(defaultFile);
            });
        }

        private static string InsertFirm(string name, string firm)
        {
            var ext = Path.GetExtension(name);               // .sql
            var baseName = Path.GetFileNameWithoutExtension(name);
            return $"{baseName}.Firm{firm}{ext}";
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}
