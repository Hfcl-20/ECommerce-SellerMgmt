using System;
using System.Collections.Generic;
using System.Linq;

namespace SellerSystem
{
    
    public class Account { public Guid Id { get; set; } = Guid.NewGuid(); public string User { get; set; } = ""; public string Pass { get; set; } = ""; }
    
    public class Seller : Account 
    { 
        public string Store { get; set; } = ""; 
        public bool IsVerified { get; set; } 
        public List<string> Products { get; set; } = new(); 
    }

    
    public class SellerManager
    {
        private List<Account> _db = new();

        public void Register(Seller s) => _db.Add(s);
        
        public bool Login(string u, string p) => _db.Any(a => a.User == u && a.Pass == p);

        public void AddProduct(Guid id, string product)
        {
            if (_db.FirstOrDefault(a => a.Id == id) is Seller s) s.Products.Add(product);
        }

        public List<Seller> GetActiveShops() => _db.OfType<Seller>().Where(s => s.IsVerified).ToList();
    }

    
    class Program { static void Main() => Console.WriteLine("Seller Management System Ready."); }
}
