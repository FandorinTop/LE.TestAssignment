﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LE.Infrastructure.Services.User;

namespace LE.Infrastructure.Services
{
    public interface IUserService
    {
        public Task<string> CreateAsync(UserDto dto);

        public Task<LoginResponse> Login(LoginRequest dto);
    }
}
