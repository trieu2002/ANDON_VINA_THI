using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ANDON_Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction? _transaction;
        public IDbConnection Connection => _connection;

        public IDbTransaction Transaction => _transaction;
        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task BeginAsync()
        {
            if (_connection.State == ConnectionState.Open)
            {
                await((SqlConnection)_connection).OpenAsync();
            }
            _transaction = _connection.BeginTransaction();
        }

        public Task CommitAsync()
        {
            _transaction?.Commit();
            _transaction = null;
            return Task.CompletedTask;
        }

        public Task RollbackAsync()
        {
            _transaction?.Rollback();
            _transaction = null;
            return Task.CompletedTask;
        }
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await RollbackAsync();
            }
            if (_connection.State != ConnectionState.Closed)
            {
                await ((SqlConnection)_connection).CloseAsync();
            }
            _connection.Dispose();
        }
    }
}
