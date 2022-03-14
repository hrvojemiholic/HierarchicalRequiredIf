using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HierarchicalRequiredIf.CustomAdapter {
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider {
        
        IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer) {

            switch (attribute) {
                
                case RequiredIfHierarchicalAttribute requiredIfHierarchicalAttribute:
                    return new RequiredIfHierarchicalAttributeAdapter(requiredIfHierarchicalAttribute, stringLocalizer);
                default:
                    return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
            }
        }
    }
}
