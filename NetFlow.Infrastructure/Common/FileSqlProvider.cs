using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Common
{
    public sealed class FileSqlProvider : ISqlProvider
    {
        private readonly string _basePath;

        public FileSqlProvider(string basePath)
        {
            _basePath = basePath;
        }

        public string Get(string name)
        {
            var path = Path.Combine(_basePath, "Sql", name);

            if (!File.Exists(path))
                throw new Exception($"SQL not found: {path}");

            return File.ReadAllText(path);
        }
    }
}
