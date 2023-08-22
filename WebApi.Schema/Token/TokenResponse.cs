﻿using WebApi.Base;

namespace WebApi.Schema;

public class TokenResponse
{
    public DateTime ExpireTime { get; set; }
    public string AccessToken { get; set; }
    public string UserName { get; set; }
}
