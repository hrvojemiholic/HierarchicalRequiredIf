using System.ComponentModel.DataAnnotations;

namespace HierarchicalRequiredIf.ViewModels {
    public class ContainerViewModel {

  
        public int ContainerID { get; set; }

  
        public string Name { get; set; }        

        public bool ItemsMustHaveValues { get; set; }


        private IList<ItemViewModel> items;
        public IList<ItemViewModel> Items {
            get {
                if (items != null) {
                    foreach (var child in this.items) {
                        if (child.ParentModel == null) {
                            child.ParentModel = this;
                        }
                    }
                }

                return this.items;
            }
            set {
                this.items = value;
            }
        }

        public ContainerViewModel() {
            this.Items=new List<ItemViewModel>();
        }

    }
}
