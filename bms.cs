using System;
using System.Collections.Generic;
using System.IO;

namespace BankManagementSystem
{
    class BankAccount
    {
        public string AccountNumber;
        public string AccountHolderName;
        public double Balance;
        public List<string> Transactions;

        public BankAccount()
        {
            Transactions = new List<string>();
        }
    }

    class Admin
    {
        private List<BankAccount> accounts;
        private string accountsFilePath = "accounts.txt";

        public Admin()
        {
            accounts = new List<BankAccount>();
            LoadAccountsFromFile();
        }

        private void LoadAccountsFromFile()
        {
            if (File.Exists(accountsFilePath))
            {
                string[] lines = File.ReadAllLines(accountsFilePath);
                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    BankAccount account = new BankAccount
                    {
                        AccountNumber = data[0],
                        AccountHolderName = data[1],
                        Balance = double.Parse(data[2])
                    };
                    accounts.Add(account);
                }
            }
        }

        private void SaveAccountsToFile()
        {
            List<string> lines = new List<string>();
            foreach (var account in accounts)
            {
                lines.Add($"{account.AccountNumber},{account.AccountHolderName},{account.Balance}");
            }
            File.WriteAllLines(accountsFilePath, lines);
        }

        public void CreateAccount()
        {
            Console.WriteLine("Enter Account Number:");
            string accountNumber = Console.ReadLine();
            Console.WriteLine("Enter Account Holder Name:");
            string accountHolderName = Console.ReadLine();
            Console.WriteLine("Enter Initial Balance:");
            double balance = double.Parse(Console.ReadLine());

            BankAccount account = new BankAccount
            {
                AccountNumber = accountNumber,
                AccountHolderName = accountHolderName,
                Balance = balance
            };
            accounts.Add(account);
            SaveAccountsToFile();
            Console.WriteLine("Account created successfully!");
        }

        public void DeleteAccount()
        {
            Console.WriteLine("Enter Account Number to Delete:");
            string accountNumber = Console.ReadLine();
            BankAccount account = accounts.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                accounts.Remove(account);
                SaveAccountsToFile();
                Console.WriteLine("Account deleted successfully!");
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }

        public void UpdateAccount()
        {
            Console.WriteLine("Enter Account Number to Update:");
            string accountNumber = Console.ReadLine();
            BankAccount account = accounts.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                Console.WriteLine("Enter New Account Holder Name:");
                account.AccountHolderName = Console.ReadLine();
                Console.WriteLine("Enter New Balance:");
                account.Balance = double.Parse(Console.ReadLine());
                SaveAccountsToFile();
                Console.WriteLine("Account updated successfully!");
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }

        public void ViewAllAccounts()
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts found!");
                return;
            }

            Console.WriteLine("All Accounts:");
            foreach (var account in accounts)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}, Holder: {account.AccountHolderName}, Balance: {account.Balance}");
            }
        }

        public void ViewTransactionHistory()
        {
            Console.WriteLine("Enter Account Number to View Transactions:");
            string accountNumber = Console.ReadLine();
            BankAccount account = accounts.Find(a => a.AccountNumber == accountNumber);
            if (account != null)
            {
                if (account.Transactions.Count == 0)
                {
                    Console.WriteLine("No transactions found!");
                    return;
                }

                Console.WriteLine($"Transaction History for Account: {account.AccountNumber}");
                foreach (var transaction in account.Transactions)
                {
                    Console.WriteLine(transaction);
                }
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            int choice;

            do
            {
                Console.WriteLine("\nBank Management System - Admin Menu");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Delete Account");
                Console.WriteLine("3. Update Account");
                Console.WriteLine("4. View All Accounts");
                Console.WriteLine("5. View Transaction History");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    admin.CreateAccount();
                }
                else if (choice == 2)
                {
                    admin.DeleteAccount();
                }
                else if (choice == 3)
                {
                    admin.UpdateAccount();
                }
                else if (choice == 4)
                {
                    admin.ViewAllAccounts();
                }
                else if (choice == 5)
                {
                    admin.ViewTransactionHistory();
                }
                else if (choice == 6)
                {
                    Console.WriteLine("Exiting...");
                }
                else
                {
                    Console.WriteLine("Invalid choice! Please try again.");
                }
            } while (choice != 6);
        }
    }
}
