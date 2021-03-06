using Microsoft.EntityFrameworkCore;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context) : base(context) { }
        /// <summary>
        /// Only Validation by User not password(it's hashed)
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.User == login.User);
        }
    }
}
