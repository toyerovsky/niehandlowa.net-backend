/* Copyright (C) Przemysław Postrach - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by V Role Play team <contact@v-rp.pl> December 2017
 */

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.Data.MySqlClient;

namespace niehandlowa.net.Dal.Context
{
    public class NonTradeContextFactory : IDesignTimeDbContextFactory<NonTradeContext>
    {
        public NonTradeContext Create() => this.CreateDbContext(new[] { "" });

        private readonly string _connectionString;

        // add this to auto generate migrations
        public NonTradeContextFactory() : this("")
        {

        }

        public NonTradeContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private bool _firstAttempt = true;
        public NonTradeContext CreateDbContext(string[] args)
        {
            if (_firstAttempt)
            {
                using (MySqlConnection testConnection = new MySqlConnection(_connectionString))
                {
                    testConnection.Open();
                    string query = "select 1";

                    using (MySqlCommand command = new MySqlCommand(query, testConnection))
                    {
                        try
                        {
                            command.ExecuteScalar();
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
                _firstAttempt = false;
            }

            DbContextOptionsBuilder<NonTradeContext> options = new DbContextOptionsBuilder<NonTradeContext>();
            options.UseMySql(_connectionString);

            return new NonTradeContext(options.Options);
        }
    }
}