using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Data;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext context) : base(context) { }

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
