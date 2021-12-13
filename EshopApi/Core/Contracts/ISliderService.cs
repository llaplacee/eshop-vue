using System;
using System.Collections.Generic;
using DataLayer.Entities;

namespace Core.Contracts
{
    public interface ISliderService : IDisposable
    {
        List<Slider> GetAllSliders();
    }
}