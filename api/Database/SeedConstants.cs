using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Database
{
    public static class SeedConstants
    {
        //Categories
        public static readonly Guid ElectronicsCategoryId = Guid.Parse("5e0045b8-03fc-4dbb-95fb-4c676e5533a1");
        public static readonly Guid FoodCategoryId = Guid.Parse("90dd49d9-47f5-486f-b1b2-d1a0e8fceffd");
        public static readonly Guid FurnitureCategoryId = Guid.Parse("4f9d04d9-f0b4-4c46-b04c-e689876274bb");
        public static readonly Guid ClothingCategoryId = Guid.Parse("4d97305a-73bd-468b-9383-4c08fc1ae98e");

        //Roles
        public static readonly Guid UserRoleId = Guid.Parse("73de8fa4-119e-4e36-81bc-17ff5762ac44");
        public static readonly Guid AdminRoleId = Guid.Parse("79471720-7377-4526-8d05-32163c09fd82");

        //user
        public static readonly Guid SystemUserId = Guid.Parse("888a7a52-03ac-4cfc-a4fa-00b3e225c144");

    }
}