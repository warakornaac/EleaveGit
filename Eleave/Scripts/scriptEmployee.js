function getDetailEmployee(empID) {
    $.ajax({
        url: getDetailEmployeeURL,
        data: {
            EMPID: empID
        },
        type: 'post',
        dataType: 'html',
        cache: false,
        Async: true,
        beforeSend: function () {
            LoadingShow();
            $('#modalEmployeeDetail').modal('show')
                .find('.modal-body')
                .html('<i class="fa fa-spinner fa-pulse fa-1x fa-fw"></i><span class="">กรุณารอสักครู่...</span>');
        },
        success: function (data) {
            $('#modalEmployeeDetail .modal-body').html(data);
            LoadingHide();
        }

    })
}