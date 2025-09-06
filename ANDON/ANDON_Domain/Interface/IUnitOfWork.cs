using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDON_Domain.Interface
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        Task BeginAsync();
        /// <summary>
        /// Thực hiện giao dịch
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
        /// <summary>
        /// Hoàn tác giao dịch
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();
    }
}
