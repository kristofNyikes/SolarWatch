﻿using Microsoft.Identity.Client;

namespace SolarWatch.Services.Authentication;

public record AuthResult(
    bool Success,
    string Email,
    string UserName,
    string Token)
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}