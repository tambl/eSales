
function initDatePicker() {
    $('.datepicker').datepicker({
        format: "dd/MM/yyyy",
        weekStart: 1,
        language: "ka",
        //todayHighlight: true,
        //autoclose: true
    });
}

$(document).ready(function () {
    initDatePicker();

});