using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trek_net_test.Models;

namespace trek_net_test.Services
{
    public interface ITrekApiService
    {
        Task<List<Bikes>> GetBikes();
    }
}
