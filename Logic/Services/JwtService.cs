﻿using ApplicationSettings.Settings;
using Logic.Dto;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Logic.Services
{
    /// <summary>
    /// Сервис для создания и валидации jwt токенов
    /// </summary>
    public class JwtService
    {
        private readonly JwtSettings _settings;
        public JwtService(JwtSettings settings)
        {
            _settings = settings;
        }

        public JwtTokenDto CreateToken(CreateTokenDto credentials)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var handler = new JwtSecurityTokenHandler();
            var token = new JwtSecurityToken(
                _settings.Issuer,
                _settings.Audience,
                new[] { new Claim("user", credentials.Id.ToString()) },
                expires: GetExpireDate(_settings.TimeToLiveInHours),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );
            var jwtToken = handler.WriteToken(token);

            return new JwtTokenDto(DateTime.Now, GetExpireDate(_settings.TimeToLiveInHours), jwtToken);
        }

        public bool ValidateToken(ValidateTokenDto tokenDto, out int? userId)
        {
            userId = null;
            var handler = new JwtSecurityTokenHandler();

            var claimsUser = handler.ValidateToken(tokenDto.Token,
                new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _settings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = _settings.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey))
                }, out var _);

            if (claimsUser == null || !claimsUser.Claims.Any())
                return false;

            var userIdToString = claimsUser.Claims.FirstOrDefault(x => x.Type == "user")?.Value;

            if(int.TryParse(userIdToString, out var result))
            {
                userId = result;
                return true;
            }

            return false;
        }

        private DateTime GetExpireDate(int timeToLive)
        {
            return DateTime.Now.AddHours(timeToLive);
        }
    }
}
