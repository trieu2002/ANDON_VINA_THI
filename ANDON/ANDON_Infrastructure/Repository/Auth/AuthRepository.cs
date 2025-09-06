using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Domain.Entities;
using ANDON_Domain.Interface;
using Dapper;

namespace ANDON_Infrastructure.Repository.Auth
{
    public class AuthRepository:IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
         

       
        public async Task<User?> AuthenticationAsync(string username, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pUSERID", username);   
            parameters.Add("@pASSWORDS", password);
            var query = "usp_VN_LoginFinishGood_Andon";
            var user = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<User>(
                 query, parameters, commandType: System.Data.CommandType.StoredProcedure
              );
            return user;
        }
    }
}
