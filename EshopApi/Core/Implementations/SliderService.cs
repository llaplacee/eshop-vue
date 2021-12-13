using System.Collections.Generic;
using System.Linq;
using Core.Contracts;
using DataLayer.ApplicationContext;
using DataLayer.Entities;

namespace Core.Implementations
{
    public class SliderService : ISliderService
    {

        private EshopContext context;

        public SliderService(EshopContext context)
        {
            this.context = context;
        }

        public List<Slider> GetAllSliders()
        {
            return context.Sliders.OrderByDescending(s => s.SliderId).ToList();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
