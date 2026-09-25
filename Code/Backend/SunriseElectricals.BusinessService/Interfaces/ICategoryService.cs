using SunriseElectricals.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunriseElectricals.BusinessService.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
