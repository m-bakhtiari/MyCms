using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;

namespace MyCms.Core.Mapper
{
    public class MapViewModelToEntity
    {
        public static Role ToRole(RolesViewModel rolesViewModel)
        {
            return new Role()
            {
                Title = rolesViewModel.Title,
                Id = rolesViewModel.Id
            };
        }

        public static Category ToCategory(CategoryViewModel categoryViewModel)
        {
            return new Category()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };
        }
    }
}
