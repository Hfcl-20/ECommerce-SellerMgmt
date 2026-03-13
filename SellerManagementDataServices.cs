using System;
using System.Collections.Generic;
using System.Linq;
using AccountManagementModels;

namespace AccountManagementDataService
{
    public class SellerDataService
    {
        
        public List<Account> dummyAccounts = new List<Account>();

        public SellerDataService()
        {
           
            var techStore = new SellerAccount 
            { 
                AccountId = Guid.NewGuid(), 
                Username = "tech_giant", 
                Password = "password123",
                StoreName = "Tech Giant Official",
                IsVerified = true 
            };

            var bookStore = new SellerAccount 
            { 
                AccountId = Guid.NewGuid(), 
                Username = "book_worm", 
                Password = "password123",
                StoreName = "The Reading Nook",
                IsVerified = false 
            };

            dummyAccounts.Add(techStore);
            dummyAccounts.Add(bookStore);
        }

        public void Add(Account account)
        {
            dummyAccounts.Add(account);
        }

        public Account? GetById(Guid id)
        {
            return dummyAccounts.FirstOrDefault(a => a.AccountId == id);
        }

        public Account? GetByUsername(string username)
        {
            return dummyAccounts.FirstOrDefault(a => a.Username == username);
        }

        public bool UsernameExists(string username)
        {
            return dummyAccounts.Any(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        
        public void Update(Account account)
        {
            var existing = GetById(account.AccountId);
            if (existing != null)
            {
               
                existing.Username = account.Username;
                existing.Password = account.Password;

                
                if (existing is SellerAccount existingSeller && account is SellerAccount incomingSeller)
                {
                    existingSeller.StoreName = incomingSeller.StoreName;
                    existingSeller.StoreDescription = incomingSeller.StoreDescription;
                    existingSeller.IsVerified = incomingSeller.IsVerified;
                }
            }
        }

      
        public List<Account> GetAccounts()
        {
            return dummyAccounts;
        }

       
        public List<SellerAccount> GetAllSellers()
        {
            return dummyAccounts.OfType<SellerAccount>().ToList();
        }
    }
}
