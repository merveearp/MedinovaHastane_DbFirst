using Medinova.DTOs.TestimonialDtos;
using Medinova.Models;
using Medinova.Repositories.TestimonialRepository;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medinova.Services.TestimonialService
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;
        public TestimonialService()
        {
            _testimonialRepository= new TestimonialRepository(new MedinovaContext());
        }
        public async Task CreateAsync(CreateTestimonialDto dto)
        {
            var testimonial = new Testimonial
            {
                Comment = dto.Comment,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            await _testimonialRepository.CreateAsync(testimonial);
        }

        public async Task DeleteAsync(int id)
        {
            await _testimonialRepository.DeleteAsync(id);
        }

        public async Task<List<ResultTestimonialDto>> GetAllAsync()
        {
            var values = await _testimonialRepository.GetAllAsync();

            return values.Select(x => new ResultTestimonialDto
            {
                TestimonialId = x.TestimonialId,
                FirstName =x.FirstName,
                LastName =x.LastName,
                Comment = x.Comment

            }).ToList();

        }

        public async Task<UpdateTestimonialDto> GetByIdAsync(int id)
        {
           var value = await _testimonialRepository.GetByIdAsync(id);
            return new UpdateTestimonialDto
            {
                TestimonialId=value.TestimonialId,
                FirstName=value.FirstName,
                LastName=value.LastName,
                Comment = value.Comment
            };
        }

        public async Task UpdateAsync(UpdateTestimonialDto dto)
        {
            var value = await _testimonialRepository.GetByIdAsync(dto.TestimonialId);

            value.FirstName = dto.FirstName;
            value.LastName = dto.LastName;
            value.Comment = dto.Comment;

            await _testimonialRepository.UpdateAsync(value);
        }
    }
}