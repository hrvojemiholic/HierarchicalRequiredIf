using HierarchicalRequiredIf.CustomAdapter;

namespace HierarchicalRequiredIf.ViewModels {
    public class ItemViewModel {

     
        public int ItemID { get; set; }
   
        public string Name { get; set; }        
        
        [RequiredIfHierarchical(parentProperty: nameof(ContainerViewModel.ItemsMustHaveValues), ignoreValues:new string [] { "False", "" }, ErrorMessage = "Required based on value of parent (ItemsMustHaveValues)")]
        public int? Value { get; set; }


        public ContainerViewModel ParentModel { get; set; }

    }
}
