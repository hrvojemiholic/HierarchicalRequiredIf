using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace HierarchicalRequiredIf.CustomAdapter {
    public class RequiredIfHierarchicalAttributeAdapter : AttributeAdapterBase<RequiredIfHierarchicalAttribute> {

        private readonly RequiredIfHierarchicalAttribute _attribute;
        private readonly IStringLocalizer _stringLocalizer;

        public RequiredIfHierarchicalAttributeAdapter(RequiredIfHierarchicalAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer) {
            _attribute = attribute;
            _stringLocalizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context) {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-requiredIfhierarchical", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-requiredIfhierarchical-otherproperty", _attribute.OtherProperty);
            MergeAttribute(context.Attributes, "data-val-requiredIfhierarchical-ignorevalues", string.Join(',', _attribute.IgnoreValues));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) {
            return Attribute.ErrorMessage;
        }
    }
}
