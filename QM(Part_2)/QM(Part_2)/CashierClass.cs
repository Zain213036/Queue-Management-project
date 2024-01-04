using System;

namespace QM_Part_2_
{
    public class Cashier
    {
        [Id]
        public int Id { get; set; }

        [Number]
        public string Number { get; set; }

        internal string CashierGeneratedNumber(string v)
        {
            if (string.IsNullOrEmpty(v))
            {
                throw new ArgumentNullException(nameof(v), "Input string cannot be null or empty");
            }

            // Generate a hash code based on the input string
            int hashCode = v.GetHashCode();

            // Convert the hash code to a string and return
            return hashCode.ToString();
        }

    }

    public class CashierContext : DbContext
    {
        public DbSet<Cashier> Cashiers { get; set; }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }

    public class CashierService
    {
        private readonly CashierContext _context;

        public CashierService()
        {
            _context = new CashierContext();
        }

        public string CashierGeneratedNumber()
        {
            // Create a new Cashier entity and save it to the database
            var cashier = new Cashier { Number = GenerateNumber() };
            _context.Cashiers.Add(cashier);
            _context.SaveChanges();

            return cashier.Number;
        }

        private string GenerateNumber()
        {
            // Generate your cashier number logic here
            // For example, you can combine a prefix and a unique number
            int latestId = _context.Cashiers.Count() + 1;
            return "CASH-" + latestId.ToString().PadLeft(5, '0');
        }
    }
}
