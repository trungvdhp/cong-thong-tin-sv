// check checkboxes by selected value array
function checkCells(selectedVals, master, childCol) {
    var ok = true;
    $(childCol).each(function () {
        var value = $(this).val()

        if (value != 'on') {
            var index = inArray(value, selectedVals);
            if (index >= 0) {
                $(this).attr('checked', true);
            }
            else {
                ok = false;
            }
        }
    });

    $(master).attr('checked', ok);
}
// return index of value in array
// buils in function $.inArray not working in function check(value, state, array)
function inArray(value, arr) {
    var len = arr.length;

    for (i = 0; i < len; i++) {
        if (value == arr[i])
            return i;
    }

    if (i == len) {
        return -1;
    }
}
// update value to array
function checkVal(value, state, array) {
    if (value != 'on') {
        var index = inArray(value, array);

        if (!state && index != -1) {
            // remove id from the list
            array.splice(index, 1);
        } else if (index == -1) {
            // add id to list
            array.push(value);
        }
    }
}
//check all child checkboxes by master checkbox state
function checkAll(master, childCol) {
    var state = $(master).is(':checked');

    $(childCol).each(function () {
        checkVal($(this).val(), state, selectedVals);
        $(this).attr('checked', state);
    });
}
//return true if all child checkboxes is checked
function isCheckedAll(childCol) {
    var ok = true;

    $(childCol).each(function () {
        if ($(this).val() != 'on' && !$(this).is(':checked')) {
            ok = false;
        }
    });
    return ok;
}

// processing action with message and complete function ajax
function processing(action, mess, func) {
    $.blockUI({
        message: mess,
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });

    $.ajax({
        url: action,
        //success: ,
        complete: function (e) {
            func();
            $.unblockUI();
        },
        cache: false
    });
}