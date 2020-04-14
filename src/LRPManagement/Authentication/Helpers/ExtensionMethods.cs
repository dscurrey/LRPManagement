﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Models;

namespace Authentication.Helpers
{
    public static class ExtensionMethods
    {
        public static List<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(u => u.WithoutPassword()).ToList();
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}
