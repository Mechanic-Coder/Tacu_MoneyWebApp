using AccountingDB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountingDB.Services
{
    public  class MerchantHandling
    {
        private AccountingContext _adb;
        private readonly Dictionary<string?, int> _merchantDictionary;
        private readonly Dictionary<string?, int> _locationDictionary;
        private readonly Dictionary<string?, int> _stateDictionary;
        private readonly Dictionary<int, int > _transactionMerchantDictionary;
        public MerchantHandling(){}
        public MerchantHandling(AccountingContext adb)
        {
            _adb = adb;
            _merchantDictionary = _adb.Merchants.ToDictionary(x => x.Name, x => x.Id, StringComparer.OrdinalIgnoreCase);

            _locationDictionary = _adb.Locations.ToDictionary(x => x.Name, x => x.Id, StringComparer.OrdinalIgnoreCase);
            
            _stateDictionary =  _adb.States.ToDictionary(x => x.ShortName, x => x.Id, StringComparer.OrdinalIgnoreCase);
            
            _transactionMerchantDictionary = _adb.TransactionMerchants.ToList().ToDictionary(x => HashCode.Combine(x.MerchantId, x.LocationId, x.StateId), x => x.Id);
        }

        private int TransactionMerchantHash(int? merchantId, int? locationId, int? stateId)
        {
            return HashCode.Combine(merchantId, locationId, stateId);
        }

        public int GetTransactionMerchantId(string? merchantName, string? locationName, string? stateName)
        {
            var locationId = GetLocationId(locationName ?? "");
            var merchantId = GetMerchantId(merchantName);
            var stateId = GetStateByShortNameId(stateName);
            var merhcLocHash = TransactionMerchantHash(merchantId, locationId, stateId);

            if(_transactionMerchantDictionary.TryGetValue(merhcLocHash, out var merchLocId))
            {
                return merchLocId;
            }
            var transactionMerchant = new TransactionMerchant
            {
                LocationId = locationId,
                MerchantId = merchantId,
                StateId = stateId
            };

            _adb.TransactionMerchants.Add(transactionMerchant);
            _adb.SaveChanges();
            _transactionMerchantDictionary.Add(TransactionMerchantHash(merchantId, locationId, stateId), transactionMerchant.Id);
            return transactionMerchant.Id;

        }
        
        public int GetMerchantId(string? merchantName)
        {
            if (_merchantDictionary.TryGetValue(merchantName, out var merchantId))
            {
                return merchantId;
            }
            else
            {
                var merchant = new Merchant
                {
                    Name = merchantName
                };
                _adb.Merchants.Add(merchant);
                _adb.SaveChanges();
                _merchantDictionary.Add(merchant.Name, merchant.Id);
                return merchant.Id;
            }
        }

        public int GetLocationId(string locationName)
        {
            if (_locationDictionary.TryGetValue(locationName, out var locationId))
            {
                return locationId;
            }

            var location = new Location
            {
                Name = locationName,
            };

            _adb.Locations.Add(location);
            _adb.SaveChanges();
            _locationDictionary.Add(locationName, location.Id);

            return location.Id;
        }

        public int GetStateByShortNameId(string? stateName)
        {
            if (_stateDictionary.TryGetValue(stateName, out var stateId))
            {
                return stateId;
            }
            
            var state = new State
            {
                ShortName = stateName
            };

            _adb.States.Add(state);
            _adb.SaveChanges();
            _stateDictionary.Add(state.ShortName, state.Id);
            return state.Id;
        }
    }
}
