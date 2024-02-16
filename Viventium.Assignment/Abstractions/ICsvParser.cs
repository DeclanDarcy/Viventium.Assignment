using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Viventium.Assignment.Entities;
using Viventium.Assignment.Models;

namespace Viventium.Assignment.Abstractions;

public interface ICsvParser
{
    Task<IReadOnlyCollection<CompanyRecord>> ParseCompaniesAsync(MemoryStream csvStream);
}