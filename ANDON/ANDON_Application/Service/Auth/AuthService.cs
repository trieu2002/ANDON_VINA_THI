using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANDON_Application.Auth;
using ANDON_Application.Interface.Auth;
using ANDON_Domain.Exceptions;
using ANDON_Domain.Interface;
using AutoMapper;

namespace ANDON_Application.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtRepository _jwtRepository;
        private readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository, IJwtRepository jwtRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtRepository = jwtRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO?> AuthenticationAsync(string username, string password)
        {
            // kiểm tra dũ liệu xem có hợp lệ hay không
            var user=await _userRepository.AuthenticationAsync(username, password);
            if (user == null)
            {
                throw new UnauthoriedException(401, "Đăng nhập thất bại do tài khoản hoặc mật khẩu không đúng", "Đăng nhập thất bại do tài khoản hoặc mật khẩu không đúng");

            }
            // nếu pass tất cả thì tạo token cho hệ thống đăng nhập
            string token=_jwtRepository.GenerateToken(user.UserName,user.Password,user.GroupId);
            // map dữ liệu sang entities
            var userDTO=_mapper.Map<UserDTO>(user);  
            userDTO.Token = token;
            // trả về dữ liệu
            return userDTO;
        }
    }
}
