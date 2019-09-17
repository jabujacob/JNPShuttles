$('.timePicker').datetimepicker({
    changeMonth: true,
    changeYear: true,
    yearRange: "-100:+0",
    dateFormat: 'dd/M/yy',
    controlType: 'select',
    timeFormat: 'hh:mm TT',
    useCurrent: false
});

function formatDateToString(date, time) {
    // 01, 02, 03, ... 29, 30, 31
    var dd = (date.getDate() < 10 ? '0' : '') + date.getDate();
    // 01, 02, 03, ... 10, 11, 12
    var MM = ((date.getMonth() + 1) < 10 ? '0' : '') + (date.getMonth() + 1);
    // 1970, 1971, ... 2015, 2016, ...
    var yyyy = date.getFullYear();
    // 01, 02, 03, ... 11, 12
    var HH = (date.getHours() < 10 ? '0' : '') + date.getHours();
    // 01, 02, 03, ... 58, 59
    var mm = (date.getMinutes() < 10 ? '0' : '') + date.getMinutes();

    if (time == 1) {
        return (yyyy + "-" + MM + "-" + dd + " " + HH + ":" + mm);
    }
    else {
        return (yyyy + "-" + MM + "-" + dd);
    }

}

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-uk-pre": function (a) {
        if (a == null || a == "") {
            return 0;
        }
        var ukDatea = a.split('/');
        return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
    },

    "date-uk-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-uk-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});