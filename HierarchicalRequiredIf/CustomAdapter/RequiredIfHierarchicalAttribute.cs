using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace HierarchicalRequiredIf.CustomAdapter {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class RequiredIfHierarchicalAttribute : ValidationAttribute {

        public string OtherProperty { get; private set; }
        public string[] IgnoreValues { get; private set; } 

        public RequiredIfHierarchicalAttribute(string parentProperty, string[] ignoreValues) {
            if (parentProperty == null) {
                throw new ArgumentNullException(nameof(parentProperty));
            }

            if (ignoreValues == null) {
                throw new ArgumentNullException(nameof(ignoreValues));
            }

            this.OtherProperty = parentProperty;
            this.IgnoreValues = ignoreValues;
        }

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
    }
}
