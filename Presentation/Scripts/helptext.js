 $(function () {
//var rootUrl = "/dims";        // Client Server Application
 var rootUrl = "";         // Local Server Application
   // var rootUrl = "";             //Locally Source Code

    //$('#tbLabRadDetails').on('click', ".LabRadInvestigationdelete", function () {
    //    var Con = confirm("Are you want to delete Investigation?");
    //    if (Con == true) {
    //        var id = $(this).data('id');
    //        var row = $(this).closest('tr');
            
    //        if (id == 0) {
    //            row.remove();
    //        }
    //        else {
    //             var url = rootUrl + "/Investigation/DeleteBillingQueueDetails";
    //            $.post(url, { BillQueueId: id }, function (response) {
    //                if (response) {
    //                    row.remove(); // OK, so remove the row
    //                } else {
    //                    // Oops - display and error message?
    //                }
    //            });
    //        }
    //        LabRadInvestigationTotalCalculation();
    //    }
    //});



    $('#tbLabRadDetails').on('click', ".LabRadInvestigationdelete", function () {
        var Con = confirm("Are you want to delete Investigation?");
        if (Con == true) {
            var id = $(this).data('id');
            var row = $(this).closest('tr');
            debugger;
            if (id == 0) {
                row.remove();
            }
            else {
                var url = rootUrl + "/Investigation/DeleteBillingQueueDetails";
                $.post(url, { BillQueueId: id }, function (response) {
                    if (response) {
                        row.remove(); // OK, so remove the row
                        LabRadInvestigationTotalCalculation();
                    } else {
                        alert("You can't delete an approved investigation!");
                        location.href = location.href;
                    }
                });
            }
            LabRadInvestigationTotalCalculation();
        }
    });



    //debugger
  
    $('#tbInvestigationDetails').on('click', ".Investigationdelete", function () {
        var Con = confirm("Are you want to delete ?");
        if (Con == true) {
            var id = $(this).data('id');
            var row = $(this).closest('tr');
            
            if (id == 0) {
                row.remove();
            }
            else {
                var url = rootUrl + "/Investigation/DeleteBillingQueueDetails";
                $.post(url, { BillQueueId: id }, function (response) {
                    if (response) {
                        row.remove(); // OK, so remove the row
                        InvestigationTotalCalculation();
                    } else {
                        alert("You can't delete an approved treatment!");
                        location.href = location.href;
                    }
                });
            }
            InvestigationTotalCalculation();
        }
    });

  
    $('#tbPrescriptionsDetails').on('click', ".Prescriptionsdelete", function () {
        var Con = confirm("Are you want to delete ?");
        if (Con == true) {
            var id = $(this).data('id');
            var row = $(this).closest('tr');
            
            if (id == 0) {
                row.remove();
            }
            else {
                var url = rootUrl + "/Prescriptions/DeletePrescriptionDetails";
                $.post(url, { PrescriptionId: id }, function (response) {
                    if (response) {
                        row.remove(); // OK, so remove the row
                    } else {
                        // Oops - display and error message?
                    }
                });
            }
            InvestigationTotalCalculation();
        }
    });



    $("#ServiceDeptName").change(function () {
        var $ServiceDeptName = $("#ServiceDeptName option:selected");
        if ($ServiceDeptName.text() == 'Radiology') {
            $("#LabRadTeethNo").prop("readonly", false);
        }
        else {
            $("#LabRadTeethNo").prop("readonly", true);
        }
        $('#LabGroupId').empty();
        $('#LabGroupId').append('<option value="0">Select</option>');
        $('#LabRadServiceName').empty();
        $('#LabRadServiceName').append('<option value="0">Select</option>');
        $.ajax({
            type: 'POST',
            url: rootUrl + "/Investigation/GetLabRadGroup",
            dataType: 'json',
            data: { Id: $("#ServiceDeptName").val() },
            success: function (Details) {
                $.each(Details, function (i, detail) {
                    $('#LabGroupId').append('<option value="' + detail.Value + '">' +
                     detail.Text + '</option>');
                });
            }
        });
    });

    var GetLabRadService = function () {

        $('#LabRadServiceName').empty();
        $('#LabRadServiceName').append('<option value="0">Select</option>');
        $.ajax({
            type: 'POST',
            url: rootUrl + "/Investigation/GetLabRadServices",
            dataType: 'json',
            data: { Id: $("#LabGroupId").val() },
            success: function (Details) {
                $.each(Details, function (i, detail) {
                    $('#LabRadServiceName').append('<option value="' + detail.Value + '">' +
                     detail.Text + '</option>');
                });
            }
        });
        return false;
    }

    $("#LabGroupId").change(function () {
        GetLabRadService();

    });

    $("#LabRadServiceName").change(function () {
        var dropdowntext = $("#LabRadServiceName option:selected").text();
        if (dropdowntext != 'Select') {
            $.ajax({
                type: "GET",
                url: rootUrl + "/Investigation/GetBillingServices?Id=" + $("#LabRadServiceName").val(),

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var items = '';
                    $.each(data, function (i, item) {
                        $("#LabRadTeethNo").val("")
                        $("#LabRadRate").val(item.Rate)
                        $("#LabRadQty").val(item.Qty)
                        $("#LabRadAmount").val(item.Amount)
                        $("#LabRadNetAmount").val(item.Amount)
                    });
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        }
        else {
            $("#LabRadDiscountPer").val(0);
            $("#LabRadDiscountRs").val(0);
            $("#LabRadQty").val(1);
        }
    });


    $("select#ServiceName").change(function () {
        //var url = '@Url.Action("GetBillingServices", "Investigation")' + '?Id=' + $("#ServiceName").val();

        var dropdowntext = $("#ServiceName option:selected").text();
        if (dropdowntext != 'Select') {
            $.ajax({
                type: "GET",
                url: rootUrl + "/Investigation/GetBillingServices?Id=" + $("#ServiceName").val(),
                // url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var items = '';
                    $.each(data, function (i, item) {
                        $("#TeethNo").val("")
                        $("#Rate").val(item.Rate)
                        $("#Qty").val(item.Qty)
                        $("#Amount").val(item.Amount)
                        $("#NetAmount").val(item.Amount)
                    });
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        }
        else {
            $("#DiscountPer").val(0);
            $("#DiscountAmt").val(0);
            $("#Qty").val(1);
        }


    });





    //$('#btnSearchAllot').on('click', function (e) {
    //    e.preventDefault();
    //    SearchAllotvalue();
    //});


    //$('#ReferralSearchTable').hide();
    //$('#AllotSearchTable').hide();









    var LabRadInvestigationTotalCalculation = function () {
       // debugger;
        var sumQty = 0;
        var sumAmount = 0;
        var sumNetAmount = 0;
        $('.js_LRsumQty').each(function () {
            var Qty = $(this);
            sumQty += parseInt(Qty.closest('tr').find('.js_LRsumQty').val());
            sumAmount += parseFloat(Qty.closest('tr').find('.js_LRsumAmount').val());
            sumNetAmount += parseFloat(Qty.closest('tr').find('.js_LRsumNetAmount').val());
        });
        $('#LabRadTotalQty').val(sumQty);
        $('#LabRadTotalAmount').val(parseFloat(sumAmount).toFixed(2))
        $('#LabRadTotalNetAmount').val(parseFloat(sumNetAmount).toFixed(2))

    };

    var InvestigationTotalCalculation = function () {
        //debugger
        var sumQty = 0;
        var sumAmount = 0;
        var sumNetAmount = 0;
        $('.js_sumQty').each(function () {
            var Qty = $(this);
            sumQty += parseInt(Qty.closest('tr').find('.js_sumQty').val());
            sumAmount += parseFloat(Qty.closest('tr').find('.js_sumAmount').val());
            sumNetAmount += parseFloat(Qty.closest('tr').find('.js_sumNetAmount').val());
        });
        $('#TotalQty').val(sumQty);
        $('#TotalAmount').val(parseFloat(sumAmount).toFixed(2))
        $('#TotalNetAmount').val(parseFloat(sumNetAmount).toFixed(2))
    };


    $('#btnApproval').on('click', function () {
        
        var $ApprovalTypeId = $('#approvalViewModal_ApprovalTypeId').val();
        var $CaserecordId = $('#approvalViewModal_CaserecordId').val();
        var $DeptId = $('#approvalViewModal_DeptId').val();
        var $DoctorId = $('#approvalViewModal_DoctorId').val();
        var $PatientId = $('#approvalViewModal_PatientId').val();
        var $ReferredTreatmentId = $('#approvalViewModal_ReferredTreatmentId').val();

        if (($ApprovalTypeId != 0) && ($CaserecordId != 0) && ($DeptId != 0) && ($DoctorId != 0) && ($PatientId != 0)) {
            var url = rootUrl + "/Approval/CaseSheetApproval?ApprovalTypeId=" + $ApprovalTypeId + "&CaserecordId=" + $CaserecordId + "&DeptId=" + $DeptId + "&DoctorId=" + $DoctorId + "&PatientId=" + $PatientId + "&ReferredTreatmentId=" + $ReferredTreatmentId + "";
            $("#myModelApprovalBodyDiv").load(url, function () {
                $("#myApprovalModel").modal("show");
            });
        }
    });
     
    //$("#tbInvestigationDetails").find("input[type='text']").change(function () {
    //    
    //    var $this = $(this);
    //    var $PaidAmount = $this.closest(".js_sumPaidAmount");        
    //    alert($PaidAmount.val());
    //})

    $(window).scroll(function () {
        var height = $(window).scrollTop();
        if (height > 100) {
            $('#back2Top').fadeIn();
        } else {
            $('#back2Top').fadeOut();
        }
    });
    $(document).ready(function () {
        $("#back2Top").click(function (event) {
            event.preventDefault();
            $("html, body").animate({ scrollTop: 0 }, "slow");
            return false;
        });

    });


});


