using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaces;
using System;

namespace SocialMedia.Infraestructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUri)
        {
            string baseUri = $"{_baseUri}{actionUri}";
            return new Uri(baseUri);
        }
    }
}
