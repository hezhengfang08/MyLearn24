using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Forum.Domain.Shared
{
    /// <summary>
    /// **获取用户** 任职用户
    /// </summary>
    public static class CoreMvcAuthorizationUtil
    {
        public static string? GetUserId(HttpContext context)
        {
            var userIdentifie = context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            if (userIdentifie != null)
            {
                return userIdentifie.Value;
            }

            return null;
        }
    }
}
