using AccountingDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDB.Services
{
    public class MetaHandling
    {
        private Dictionary<string?, int> _metaDictionary;
        private AccountingContext _adb;
        public MetaHandling() { }
        public MetaHandling(AccountingContext adb) {
            _adb = adb;
            _metaDictionary = _adb.Metas.ToDictionary(x => x.Content, x => x.Id, StringComparer.OrdinalIgnoreCase);
        }

        public int GetMetaId(string? metaContent)
        {
            var maxLength = 100;
            if (!string.IsNullOrEmpty(metaContent))
            {
                metaContent = metaContent.Length <= maxLength ? metaContent : metaContent.Substring(0, maxLength);
            }
            
            if (_metaDictionary.TryGetValue(metaContent, out var metaId))
            {
                return metaId;
            }
            else
            {
                var meta = new Meta
                {
                    Content = metaContent
                };
                _adb.Metas.Add(meta);
               
                _adb.SaveChanges();
                _metaDictionary.Add(meta.Content, meta.Id);
                return meta.Id;
            }
        }
    }
}
