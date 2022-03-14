using HierarchicalRequiredIf.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HierarchicalRequiredIf {

    public class ApiContext : DbContext {

        public ContainerViewModel? Container { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options) {

            this.Container = new ContainerViewModel { ContainerID = 1, Name = "Container1" };

            ItemViewModel item1 = new ItemViewModel { ItemID = 1, Name = "Item1" };
            ItemViewModel item2 = new ItemViewModel { ItemID = 1, Name = "Item2" };

            this.Container.Items.Add(item1);
            this.Container.Items.Add(item2);

            this.SaveChanges();

        }

    }


}
