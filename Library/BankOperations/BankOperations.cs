﻿using Library.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Accounts;

namespace Library.BankOperations
{
    public static class BankOperations
    {
        public static void UpdateMoney(DataBase instance, string numberCard, decimal sum)
        {
            string query = "UPDATE CARD SET balance = balance+@newBalance WHERE card_number= @card;";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@newBalance", sum),
            new SqlParameter("@card", numberCard)
            };
            instance.InsertUpdateDeleteData(query, parameters);
        }
        public static Dictionary<string, decimal> CheckBalanceOnAllCards(DataBase instance, int idUser)
        {
            Dictionary<string, decimal> cardBalance = new Dictionary<string, decimal>();

            string query = "select card_number, balance from CARD c inner join MyUser u on c.id_user=u.id_user where u.id_user = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@id", idUser)
            };

            DataTable result = instance.SelectData(query, parameters);
            if (result.Rows.Count >0)
            {
                foreach (DataRow row in result.Rows)
                {
                    string card = row["card_number"].ToString()!;
                    decimal balance = Convert.ToDecimal(row["balance"]);
                    cardBalance[card] = balance;
                }
                return cardBalance;
            }
            else
            {
                throw new Exception();
            }
        }
        
        public static DataTable ShowHistory(DataBase instance, int userID)
        {
            string query = "select date, description, suma, card_number from History inner join CARD on History.id_card = CARD.id_card where History.id_user = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@id", userID)
            };

            DataTable result = instance.SelectData(query, parameters);
            return result;
        }
    }
}