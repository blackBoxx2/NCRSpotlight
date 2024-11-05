using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;

namespace UseCasesLayer.UseCaseInterfaces.QualityPortionUseCaseInterfaces
{
    public interface IGetQualityPortionsAsyncUseCase
    {

        Task<IEnumerable<QualityPortion>> Execute();

    }
}
