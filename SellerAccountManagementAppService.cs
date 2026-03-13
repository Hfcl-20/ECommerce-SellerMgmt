using System;
using System.Collections.Generic;
using System.Linq;

namespace SellerManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            SellerAppService sellerService = new SellerAppService();

            Console.WriteLine("--- Seller Account Management System Test ---\n");

          
            SellerAccount mySeller = new SellerAccount
            {
                Username = "vintage_collector",
                Password = "securePassword123",
                Email = "alex@retro.com",
                StoreName = "The Retro Vault",
                StoreDescription = "Rare items from the 80s and 90s."
            };

            bool isRegistered = sellerService.RegisterSeller(mySeller);
            Console.WriteLine($"[1] Registration Successful: {isRegistered}");

            
            bool loginStatus = sellerService.Login("vintage_collector", "securePassword123");
            Console.WriteLine($"[2] Login Attempt: {(loginStatus ? "Success" : "Failed")}");

            
            Console.WriteLine("[3] Admin verifying the seller...");
            sellerService.VerifySeller(mySeller.Id);

            
            bool updateStatus = sellerService.UpdateStoreInfo(mySeller.Id, "The Retro & Antique Vault", "Now including 70s items!");
            Console.WriteLine($"[4] Store Update: {(updateStatus ? "Updated" : "Failed")}");

            
            var verifiedSellers = sellerService.GetVerifiedSellers();
            Console.WriteLine("\n--- ACTIVE SELLERS DASHBOARD ---");
            foreach (var s in verifiedSellers)
            {
                Console.WriteLine($"Shop: {s.StoreName}");
                Console.WriteLine($"Owner: {s.Username}");
                Console.WriteLine($"Joined: {s.DateJoined.ToShortDateString()}");
                Console.WriteLine($"Verified: {s.IsVerified}");
                Console.WriteLine("--------------------------------");
            }

            Console.WriteLine("\nTest Complete. Press any key to exit.");
            Console.ReadKey();
        }
    }

    
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class SellerAccount : Account
    {
        public string StoreName { get; set; } = string.Empty;
        public string StoreDescription { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public DateTime DateJoined { get; set; } = DateTime.Now;
    }

    
    public class AccountDataService
    {
        private static List<Account> _accounts = new List<Account>();

        public void Add(Account account) => _accounts.Add(account);
        public bool UsernameExists(string user) => _accounts.Any(a => a.Username == user);
        public Account? GetById(Guid id) => _accounts.FirstOrDefault(a => a.Id == id);
        public Account? GetByUsername(string user) => _accounts.FirstOrDefault(a => a.Username == user);
        public List<Account> GetAll() => _accounts;
        public void Update(Account updated)
        {
            var index = _accounts.FindIndex(a => a.Id == updated.Id);
            if (index != -1) _accounts[index] = updated;
        }
    }

    
    public class SellerAppService
    {
        private readonly AccountDataService _dataService = new AccountDataService();

        public bool RegisterSeller(SellerAccount newSeller)
        {
            if (_dataService.UsernameExists(newSeller.Username)) return false;
            _dataService.Add(newSeller);
            return true;
        }

        public bool Login(string username, string password)
        {
            var account = _dataService.GetByUsername(username);
            return account != null && account.Password == password;
        }

        public bool UpdateStoreInfo(Guid id, string name, string desc)
        {
            if (_dataService.GetById(id) is SellerAccount seller)
            {
                seller.StoreName = name;
                seller.StoreDescription = desc;
                _dataService.Update(seller);
                return true;
            }
            return false;
        }

        public void VerifySeller(Guid id)
        {
            if (_dataService.GetById(id) is SellerAccount seller)
            {
                seller.IsVerified = true;
                _dataService.Update(seller);
            }
        }

        public List<SellerAccount> GetVerifiedSellers()
        {
            return _dataService.GetAll().OfType<SellerAccount>().Where(s => s.IsVerified).ToList();
        }
    }
}
