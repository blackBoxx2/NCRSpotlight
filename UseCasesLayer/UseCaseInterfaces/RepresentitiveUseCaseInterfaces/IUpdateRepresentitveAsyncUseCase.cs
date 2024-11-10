using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces
{
    public interface IUpdateRepresentativeAsyncUseCase
    {
        Task Execute(string id, IdentityUser representative);
    }
}
