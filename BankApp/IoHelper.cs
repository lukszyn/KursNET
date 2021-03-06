﻿using BankApp.BusinessLayer.Models;
using BankApp.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    public class IoHelper
    {
        public string GetTextFromUser(string message)
        {
            Console.Write($"{message}: ");
            return Console.ReadLine();
        }

        public int GetIntFromUser(string message)
        {
            int number;
            while (!int.TryParse(GetTextFromUser(message), out number))
            {
                Console.WriteLine("Not na integer - try again.\n");
            }

            return number;
        }

        public uint GetUintFromUser(string message)
        {
            uint number;

            while (!uint.TryParse(GetTextFromUser(message), out number))
            {
                Console.WriteLine("Not a positive integer - try again.\n");
            }

            return number;
        }

        public decimal GetDecimalFromUser(string message)
        {
            decimal number;

            while (!decimal.TryParse(GetTextFromUser(message), out number))
            {
                Console.WriteLine("Not a floating point number - try again.\n");
            }

            return number;
        }

        public bool ValidateEmail(string email)
        {
            return email.Contains("@");
        }

        public bool ValidatePassword(string password)
        {
            return password.Length >= 6;
        }

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            int number;
            return phoneNumber.Length == 9 && int.TryParse(phoneNumber, out number);
        }

        public bool CheckIfNegative(decimal amount)
        {
            return amount <= 0;
        }

        public void PrintAccountName(Account account)
        {
            Console.WriteLine($"\n{"Account name: ",-10} {account.Name,-20}");
        }

        public void PrintAccountsBalance(ICollection<Account> accounts)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\n{"Account Name",-30}{"Account Number",-40}{"Balance",-20}\n");

            foreach (var account in accounts)
            {
                sb.Append(BuildAccountString(account));
            }

            Console.WriteLine(sb.ToString());
        }

        public void PrintTransfers(ICollection<Transfer> transfers)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{"Transfer Date",-19}" +
                      $" {"Receiver Account Number",-36}" +
                      $" {"Sender Account Number",-36}" +
                      $" {"Transfer Name",-25}" +
                      $" {"Transfer Amount",-20}" +
                      $" {"Transfer Type",-20}\n");

            foreach (var transfer in transfers)
            {
                sb.Append(BuildTransferString(transfer));
            }

            Console.WriteLine(sb.ToString());
        }

        public string BuildAccountString(Account account)
        {
            return $"{account.Name,-30}{account.Number,-40}{account.Balance + "$",-20:N2}\n";
        }

        public string BuildTransferString(Transfer transfer)
        {
            return $"{transfer.Date,-0:dd/MM/yyyy HH:mm:ss}" +
                $" {transfer.Receiver.Number, -36}" +
                $" {transfer.Account.Number,-36}" +
                $" {transfer.Name,-25}" +
                $" {transfer.Amount + "$",-20:N2}" +
                $" {transfer.Type,-20}\n";
        }

        public void PrintStatement(AccountStatement accountStatement)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("----------------------------------------\n");
            sb.Append("Outgoing Transfers Summary\n\n");
            sb.Append($"{"Receiver Account Number",-36}" +
                      $"{"Transfers Sum", 20}\n");

            foreach (var transfer in accountStatement.OutgoingTransfersStatement)
            {
                sb.Append($"{transfer.Key, -36}{transfer.Value, 20:N2}$\n");
            }

            sb.Append($"Overall: {accountStatement.OutgoingTransfersSum, 5:N2}$\n\n");

            sb.Append("Incoming Transfers Summary\n\n");
            sb.Append($"{"Sender Account Number",-36}" +
                $" {"Transfers Sum", 20}\n");

            foreach (var transfer in accountStatement.IncomingTransfersStatement)
            {
                sb.Append($"{transfer.Key,-36}{transfer.Value, 20:N2}$\n");
            }

            sb.Append($"Overall: {accountStatement.IncomingTransfersSum, 5:N2}$\n\n");

            sb.Append($"Maximum Transfer Amount: {accountStatement.MaxTransfer, 10:N2}$\n");
            sb.Append($"Minimum Transfer Amount: {accountStatement.MinTransfer, 10:N2}$\n");
            sb.Append($"Average Transfer Amount: {accountStatement.AverageTransfer, 10:N2}$\n");

            Console.WriteLine(sb.ToString());
        }
    }
}
