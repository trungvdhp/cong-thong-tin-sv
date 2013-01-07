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

//page back
function pageback() {
    parent.history.back();
    return false;
}

//Get data fields and data titles
function getFieldAndTitles(gridHeader, dataFields, dataTitles) {
    var temp1 = [];
    var temp2 = [];
    var unchecked = [];
    var selectedArray = [];
    var header = $(gridHeader);
    var data_field;
    var data_title;
    var state;

    //search all header
    header.each(function (index) {
        data_field = $(this).attr("data-field");

        if (data_field != null) {
            checkVal(data_field, true, temp1);
            data_title = $(this).attr("data-title");

            if (data_title != null) {
                checkVal(data_title, true, temp2);
            }
            else {
                checkVal(data_field, true, temp2);
            }
        }
    });

    //search all checkbox have attr data-field, data-title
    header = $('input:checkbox');

    header.each(function (index) {
        data_field = $(this).attr("data-field");
        state = $(this).is(":checked");

        if (data_field != null) {
            if (!state) {
                //save unchecked data-fields 
                checkVal(data_field, true, unchecked);
            }
            //save all selected data-fields
            checkVal(data_field, true, selectedArray);
        }
    });

    //filter data-fields not checked
    var len = unchecked.length;
    for (var i = 0; i < len; i++) {
        index = inArray(unchecked[i], selectedArray);
        if (index != -1) {
            selectedArray.splice(index, 1);
        }
    }

    //filter data-fields and data-titles exist in selectedArray
    var len = temp1.length;

    for (var i = 0; i < len; i++) {
        var index = inArray(temp1[i], selectedArray);

        if (index != -1) {
            dataFields.push(temp1[i]);
            dataTitles.push(temp2[i]);
        }
    }
}