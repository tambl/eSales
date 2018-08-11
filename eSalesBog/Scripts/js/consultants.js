function initDatePicker() {
    $('.datepicker').datepicker({
        format: "mm/dd/yyyy",
        weekStart: 1,
        language: "ka"
    });
}

$(document).ready(function () {
    initDatePicker();

});