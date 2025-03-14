using Lockshot.User.API.Class;
using Lockshot.User.API.Core.DTOs;
using Lockshot.User.API.Core.Interfaces;
using Lockshot.User.API.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lockshot.User.API.Core.Services
{
    public class HitService : IHitService
    {
        private readonly IHitRepository _hitRepository;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public HitService(IHitRepository hitRepository, IConfiguration configuration)
        {
            _hitRepository = hitRepository;
            _secretKey = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];

        }

        public async Task SaveHitAsync(HitDto hitDto)
        {
            var hit = new Hit
            {
                UserId = hitDto.UserId,
                WeaponType = hitDto.WeaponType,
                Score = hitDto.Score,
                Distance = hitDto.Distance,
                Timestamp = DateTime.UtcNow
            };
            
            await _hitRepository.SaveHitAsync(hit);
        }

        public async Task<IEnumerable<HitDto>> GetMostHitsByMetrics(int userId, double Metrics)
        {
            var hits = await _hitRepository.GetHitsByMetrics(userId, Metrics);

            return hits
                .OrderByDescending(hit => hit.Metrics) 
                .Select(hit => new HitDto
                {
                    Id = hit.Id,
                    WeaponType = hit.WeaponType,
                    Score = hit.Score,
                    Timestamp = hit.Timestamp,
                    Distance = hit.Distance,
                    Metrics = hit.Metrics
                });
        }

        public async Task<IEnumerable<HitDto>> GetMostByDistance(int userId, double Distance)
        {
            var hits = await _hitRepository.GetHitsByDistance(userId, Distance);

            return hits
                .OrderByDescending(hit => hit.Score)
                .Select(hit => new HitDto
                {
                    Id = hit.Id,
                    WeaponType = hit.WeaponType,
                    Score = hit.Score,
                    Timestamp = hit.Timestamp,
                    Distance = hit.Distance,
                    Metrics = hit.Metrics
                });
        }
        public async Task<IEnumerable<HitDto>> GetMostHitsByScore(int userId, int Score)
        {
            var hits = await _hitRepository.GetHitsByScore(userId, Score);

            return hits
                .OrderByDescending(hit => hit.Score)
                .Select(hit => new HitDto
                {
                    Id = hit.Id,
                    WeaponType = hit.WeaponType,
                    Score = hit.Score,
                    Timestamp = hit.Timestamp,
                    Distance = hit.Distance,
                    Metrics = hit.Metrics
                });
        }

        public async Task<IEnumerable<HitDto>> GetHitsByWeaponType(int userId, string WeaponType)
        {
            var hits = await _hitRepository.GetHitsByWeaponType(userId, WeaponType);

            return hits
                .OrderByDescending(hit => hit.WeaponType)
                .Select(hit => new HitDto
                {
                    Id = hit.Id,
                    WeaponType = hit.WeaponType,
                    Score = hit.Score,
                    Timestamp = hit.Timestamp,
                    Distance = hit.Distance,
                    Metrics = hit.Metrics
                });
        }

        public async Task<IEnumerable<HitDto>> GetAllHits(int userId)
        {
            var hits = await _hitRepository.GetAllHits(userId);
            return hits.Select(hit => new HitDto
            {
                Id = hit.Id,
                WeaponType = hit.WeaponType,
                Score = hit.Score,
                Timestamp = hit.Timestamp,
                Distance = hit.Distance,
                Metrics = hit.Metrics
            });
        }

        public async Task<object> GetUserHitStatisticsAsync(int userId)
        {
            var hits = (await _hitRepository.GetAllHits(userId))?.ToList();

            if (hits == null || hits.Count == 0)
            {
                Console.WriteLine($"User {userId} has no hits.");
                return new { Message = "No hits found for the user." };
            }

            var totalShots = hits.Count;
            var maxDistance = hits.Max(hit => hit.Distance);
            var averageScore = hits.Average(hit => hit.Score);
            var averageMetrics = hits.Average(hit => (double?)hit.Metrics) ?? 0;
            var mostUsedWeapon = hits
                .GroupBy(hit => hit.WeaponType)
                .OrderByDescending(group => group.Count())
                .FirstOrDefault()?.Key;

            Console.WriteLine($"Statistics for User {userId}: TotalShots={totalShots}, MaxDistance={maxDistance}, AverageScore={averageScore}, MostUsedWeapon={mostUsedWeapon}, AverageMetrics={averageMetrics}");

            return new
            {
                TotalShots = totalShots,
                AverageMetrics = averageMetrics,
                MaxDistance = maxDistance,
                AverageScore = averageScore,
                MostUsedWeapon = mostUsedWeapon
            };
        }


        public async Task<string> ValidateTokenAndGetUserId(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token is null or empty.");
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = true,
                    ValidAudience = _audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;


                if (string.IsNullOrEmpty(userId))
                {
                    Console.WriteLine("User ID claim is missing or empty.");
                }

                return userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }

    }
}
