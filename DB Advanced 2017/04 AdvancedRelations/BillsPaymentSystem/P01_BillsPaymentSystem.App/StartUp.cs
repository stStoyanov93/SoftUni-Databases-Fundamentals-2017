namespace P01_BillsPaymentSystem.App
{
    using System;
    using System.Linq;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;

    class StartUp
    {
        static void Main(string[] args)
        {

            //Creating the database and adding the check constraint with the migration
            using (var context = new PaymentSystemDBContex())
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }

            //Seeding the database (with 1 user)
            using (var contex = new PaymentSystemDBContex())
            {
                Seed(contex);
            }

            //Getting user's details
            using (var context = new PaymentSystemDBContex())
            {
                GetUserDetails(context);
            }

            //Paying bills
            using (var context = new PaymentSystemDBContex())
            {               
                try
                {
                    Console.Write("Enter userId: ");
                    var userId = int.Parse(Console.ReadLine());
                    Console.Write("Enter the amount you wish to pay: ");
                    var amount = decimal.Parse(Console.ReadLine());

                    PayBills(userId, amount, context);
                    Console.WriteLine("The bills have been successfully paid.");
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void Seed(PaymentSystemDBContex db)
        {
                var user = new User()
                {
                    FirstName = "Guy",
                    LastName = "Gilbert",
                    Email = "gGuilbert@gmail.com",
                    Password = "Asdawe2e"
                };

                var creditCard = new CreditCard()
                {
                    Limit = 800.00M,
                    MoneyOwed = 100.00M,
                    ExpirationDate = DateTime.ParseExact("20.03.2020", "dd.MM.yyyy", null)
                };

                var bankAccounts = new BankAccount[]
                {

                new BankAccount()
                {
                    Balance = 2000M,
                    BankName = "Unicredit Bulbank",
                    SwiftCode = "UNCRBGSF"
                },

                new BankAccount()
                {
                    Balance = 1000M,
                    BankName = "First Investment Bank",
                    SwiftCode = "FINVBGSF"
                }};

                var paymentMethods = new PaymentMethod[]
                {
                    new PaymentMethod()
                    {
                        User = user,
                        CreditCard = creditCard,
                        Type = PaymentMethodType.CreditCard
                    },

                    new PaymentMethod()
                    {
                        User = user,
                        BankAccount = bankAccounts[0],
                        Type = PaymentMethodType.BankAccount
                    },

                    new PaymentMethod()
                    {
                        User = user,
                        BankAccount = bankAccounts[1],
                        Type = PaymentMethodType.BankAccount
                    }};

                db.Users.Add(user);
                db.CreditCards.Add(creditCard);
                db.BankAccounts.AddRange(bankAccounts);
                db.PaymentMethods.AddRange(paymentMethods);

                db.SaveChanges();           
        }
        private static void GetUserDetails(PaymentSystemDBContex db)
        {
            Console.Write("Enter userId: ");
            var userId = int.Parse(Console.ReadLine());

            var user = db.Users
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    FullName = u.FirstName + " " + u.LastName,
                    CreditCards = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.CreditCard)
                        .Select(pm => pm.CreditCard).ToList(),
                    BankAccounts = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.BankAccount)
                        .Select(pm => pm.BankAccount).ToList(),
                })
                .FirstOrDefault();

            Console.WriteLine($"User: {user.FullName}");

            var bankAccounts = user.BankAccounts;

            if (bankAccounts.Any())
            {
                Console.WriteLine("Bank Accounts:");

                foreach (var item in bankAccounts)
                {
                    Console.WriteLine($@"-- ID: {item.BankAccountId}");
                    Console.WriteLine($"-- - Balance: {item.Balance:F2}");
                    Console.WriteLine($"-- - Bank: {item.BankName}");
                    Console.WriteLine($"-- - SWIFT: {item.SwiftCode}");
                }
            }

            var creditCards = user.CreditCards;

            if (creditCards.Any())
            {
                Console.WriteLine("Credit Cards:");

                foreach (var item in creditCards)
                {
                    Console.WriteLine($"-- ID: {item.CreditCardId}");
                    Console.WriteLine($"-- - Limit: {item.Limit:F2}");
                    Console.WriteLine($"-- - Money Owed: {item.MoneyOwed:F2}");
                    Console.WriteLine($"-- - Limit Left: {item.LimitLeft}");
                    Console.WriteLine($"-- - Expiration Date: {item.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                }
            }
        }
        private static void PayBills(int userId, decimal amount, PaymentSystemDBContex context)
        {
            

            var user = context.Users.Find(userId);

            if (user == null)
            {
                Console.WriteLine($"User with id {userId} not found!");
                return;
            }

            decimal usersMoney = 0.0m;

            var bankAccounts = context
                .BankAccounts.Join(context.PaymentMethods,
                    (ba => ba.BankAccountId),
                    (p => p.BankAccountId),
                    (ba, p) => new
                    {
                        UserId = p.UserId,
                        BankAccountId = ba.BankAccountId,
                        Balance = ba.Balance
                    })
                .Where(ba => ba.UserId == userId)
                .ToList();


            var creditCards = context
                .CreditCards.Join(context.PaymentMethods,
                    (c => c.CreditCardId),
                    (p => p.CreditCardId),
                    (c, p) => new
                    {
                        UserId = p.UserId,
                        CreditCardId = c.CreditCardId,
                        LimitLeft = c.LimitLeft
                    })
                .Where(c => c.UserId == userId)
                .ToList();

            usersMoney += bankAccounts.Sum(b => b.Balance);
            usersMoney += creditCards.Sum(c => c.LimitLeft);

            if (usersMoney < amount)
            {
                throw new InvalidOperationException("Insufficient funds!");
            }

            bool isPayBills = false;
            foreach (var bankAccount in bankAccounts.OrderBy(b => b.BankAccountId))
            {
                var currentAccount = context.BankAccounts.Find(bankAccount.BankAccountId);

                if (amount <= currentAccount.Balance)
                {
                    currentAccount.Withdraw(amount);
                    isPayBills = true;
                }
                else
                {
                    amount -= currentAccount.Balance;
                    currentAccount.Withdraw(currentAccount.Balance);
                }

                if (isPayBills)
                {
                    context.SaveChanges();
                    return;
                }
            }

            foreach (var creditCard in creditCards.OrderBy(c => c.CreditCardId))
            {
                var currentCreditCard = context.CreditCards.Find(creditCard.CreditCardId);

                if (amount <= currentCreditCard.LimitLeft)
                {
                    currentCreditCard.Withdraw(amount);
                    isPayBills = true;
                }
                else
                {
                    amount -= currentCreditCard.LimitLeft;
                    currentCreditCard.Withdraw(currentCreditCard.LimitLeft);
                }

                if (isPayBills)
                {
                    context.SaveChanges();
                    return;
                }
            }
        }
    }
}
