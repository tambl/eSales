$(document).on('click', '#add-product', function () {
    addNewTrToTable('product');
    reorderingTable('product');
});

function addNewTrToTable(tableName) {
    var newTr = $('.hidden-' + tableName + '-tr').first().clone();
    newTr.removeClass('hidden-' + tableName + '-tr').show();
    $('table#' + tableName + '-table tbody').append(newTr);

};

$(document).on('click', 'table#product-table .remove-product', function () {
    var thisEl = $(this);

    if ($("input", thisEl.closest("tr"))[0].getAttribute("name").split('[')[1]) {
        var idOfDeletedTr = $("input", thisEl.closest("tr"))[0].getAttribute("name").split('[')[1].split(']')[0];

        document.getElementsByName("Products[" + idOfDeletedTr + "].IsDeleted")[0].setAttribute("value", "true");

        thisEl.closest('tr').hide();

        //thisEl.closest('tr').remove();        
    }
    else {
        thisEl.closest('tr').remove();

    }

});

function reorderingTable(tableName) {
    $('table#' + tableName + '-table tbody tr').each(function (trKey, trElement) {
        $("input, select", $(trElement)).each(function (inputKey, inputElement) {
            var name = $(inputElement).attr("name");
            var newName = name;
            if (name.indexOf('.[') !== -1) {
                newName = name.split('.[')[0] + '[' + trKey + '].' + name.split(']')[1];
            }
            else if (name.indexOf('.') !== -1 && name.indexOf(']') === -1 && name.indexOf('[') === -1) {
                newName = name.split('.')[0] + '[' + trKey + '].' + name.split('.')[1];
            }
            else if (name.indexOf('[') !== -1 && name.indexOf(']') !== -1 && name.indexOf('.') === -1) {
                newName = name.split('[')[0] + '[' + trKey + '].' + name.split(']')[1];
            }
            else if (name.indexOf('[') !== -1 && name.indexOf('].') !== -1) {
                newName = name.split('[')[0] + '[' + trKey + '].' + name.split('].')[1];
            }
            else if (name.indexOf('.') === -1 && name.indexOf(']') === -1 && name.indexOf('[') === -1) {
                var refProp = $(inputElement).closest("[data-ref-prop]").data("ref-prop");
                newName = refProp + '[' + trKey + '].' + name;
            }
            $(inputElement).attr("name", newName);
            $(inputElement).attr("id", newName);
        });
    });
};