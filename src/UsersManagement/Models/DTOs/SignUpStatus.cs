﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Models.DTOs;

public enum SignUpStatus : int
{
    DublicateUsername = 1,
    CreateUserSuccess = 2,

}
