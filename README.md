# HierarchicalRequiredIf

This example shows how to create RequiredIf attribute of
some ViewModel property, based on property of the parent.

Solution applies to .NET Core Razor Pages project, but it can be easily 
addapted to other types of projects.

Main part of the solution is to connect Parent and Child model.



1. Child model would have ParentModel property

 ** public ContainerViewModel ParentModel { get; set; 
 
2.This property is set on Parent's getter of child items:
 
 ```
 
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
        
        
  ```
  
 3.  After this, attribute is applied on child ViewModel property:
  
    ```
  
   [RequiredIfHierarchical(parentProperty: nameof(ContainerViewModel.ItemsMustHaveValues), ignoreValues:new string [] { "False", "" }, ErrorMessage = "Required based on value of parent (ItemsMustHaveValues)")]
        public int? Value { get; set; }
        
     ```
  
 4. Code for server side validation is **IsValid** method of custom attribute **RequiredIfHierarchical**:
  
  
    ```
  
  
  protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            if (value == null) {

                var targetObject = validationContext.ObjectInstance;


                var parentProperty = targetObject.GetType().GetProperty("ParentModel");
                var parentModel = parentProperty.GetValue(targetObject, null);


                var otherProperty = parentModel.GetType().GetProperty(OtherProperty);
                var otherPropertyValue = otherProperty.GetValue(parentModel, null)?.ToString();

                if (IgnoreValues.Any(x => x == otherPropertyValue)) return ValidationResult.Success;

                if (!string.IsNullOrWhiteSpace(otherPropertyValue)) {
                    
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;

        }
        
        
          ```
  
  5. Client side validation can be applied with jquery unobtrusive validation method:
  
  
    ```
  
   $.validator.addMethod("requiredifhierarchical", function (value, element, params) {

        var modelData = element.name.split('.');
        modelData = modelData.reverse();

        var parentModelName = modelData[2];

        var elementOfParentModel = $('#' + parentModelName + "_" + params);

        var ignoreElements;
        var ignoreEl = $(element).data("val-requiredifhierarchical-ignorevalues");        

        if (ignoreEl.indexOf(',') !== -1) {
            ignoreElements = ignoreEl.split(',');
        } else {
            ignoreElements = [ignoreEl];
        }

        var parentValue = elementOfParentModel.val();
        if (elementOfParentModel.attr('type') == 'checkbox') {
            parentValue = elementOfParentModel.is(":checked");
        }

        for (i = 0; i < ignoreElements.length; i++) {
            if (parentValue == ignoreElements[i]) return true;
        }

        if (parentValue != '') {
            return value != '';
        }

        return true;
    });
    
      ```
  
                                              
                                              
  
  
  
  
