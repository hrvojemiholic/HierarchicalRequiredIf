(function ($) {

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


    $.validator.unobtrusive.adapters.addSingleVal("requiredifhierarchical", "otherproperty");

}(jQuery));
