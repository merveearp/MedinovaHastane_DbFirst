using Medinova.DTOs.BlogDtos;
using Medinova.Models;
using Medinova.Repositories.BlogRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.BlogService
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        public BlogService()
        {
            _blogRepository = new BlogRepository(new MedinovaContext());
        }
        public async Task CreateAsync(CreateBlogDto dto)
        {
            var blog = new Blog
            {
                BlogTitle = dto.BlogTitle,
                BlogContent = dto.BlogContent,
                BlogSubtitle = dto.BlogSubtitle,
                BlogWriter = dto.BlogWriter,
                WriterProfile = dto.WriterProfile,
                Image1 = dto.Image1,
                Image2 = dto.Image2

            };

            await _blogRepository.CreateAsync(blog);
        }

        public async Task DeleteAsync(int id)
        {
           await _blogRepository.DeleteAsync(id);
        }

        public async Task<List<ResultBlogDto>> GetAllAsync()
        {
           var values = await _blogRepository.GetAllAsync();
            return values.Select(x => new ResultBlogDto
            {
                BlogId = x.BlogId,
                BlogTitle = x.BlogTitle,
                BlogContent = x.BlogContent,
                BlogSubtitle = x.BlogSubtitle,
                BlogWriter = x.BlogWriter,
                WriterProfile = x.WriterProfile,
                Image1 = x.Image1,
                Image2 = x.Image2

            }).ToList();
        }

        public async Task<UpdateBlogDto> GetByIdAsync(int id)
        {
           var value = await _blogRepository.GetByIdAsync(id);
            return new UpdateBlogDto
            {
                BlogId= value.BlogId,
                BlogTitle= value.BlogTitle,
                BlogContent = value.BlogContent,
                BlogSubtitle = value.BlogSubtitle,
                BlogWriter = value.BlogWriter,
                WriterProfile = value.WriterProfile,
                Image1 = value.Image1,
                Image2 = value.Image2

            };
        }

        public async Task UpdateAsync(UpdateBlogDto dto)
        {
            var value = await _blogRepository.GetByIdAsync(dto.BlogId);

            value.BlogTitle = dto.BlogTitle;
            value.BlogContent = dto.BlogContent;
            value.BlogSubtitle = dto.BlogSubtitle;
            value.BlogWriter = dto.BlogWriter;
            value.WriterProfile = dto.WriterProfile;
            value.Image1 = dto.Image1;
            value.Image2 = dto.Image2;

            await _blogRepository.UpdateAsync(value);

        }
    }
}