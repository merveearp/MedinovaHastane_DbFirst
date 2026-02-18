using Medinova.DTOs.AIDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medinova.Services.AIService
{
    public interface IAIService
    {
        Task<AIResponseDto> AskAIAsync(string question);
    }
}
